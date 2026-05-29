import torch
from stable_baselines3 import PPO
from enemy_env import EnemyEnv

env = EnemyEnv()

model = PPO.load(
    r"C:\Users\Gustavo\Downloads\ProjetoIA\ProjetoIA\enemy_ai.zip",
    env=env
)

policy = model.policy

obs_size = env.observation_space.shape[0]

dummy_input = torch.randn(1, obs_size)

# Wrapper correto
class SB3OnnxablePolicy(torch.nn.Module):
    def __init__(self, policy):
        super().__init__()
        self.policy = policy

    def forward(self, observation):
        features = self.policy.extract_features(observation)

        if self.policy.share_features_extractor:
            latent_pi, _ = self.policy.mlp_extractor(features)
        else:
            latent_pi = self.policy.mlp_extractor.forward_actor(features)

        action_logits = self.policy.action_net(latent_pi)

        return action_logits

onnxable_model = SB3OnnxablePolicy(policy)

torch.onnx.export(
    onnxable_model,
    dummy_input,
    "enemy_ai.onnx",
    opset_version=11,
    input_names=["input"],
    output_names=["output"]
)

print("ONNX exportado com sucesso!")