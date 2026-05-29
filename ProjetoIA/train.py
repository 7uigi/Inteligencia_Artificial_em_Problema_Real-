from stable_baselines3 import PPO
from enemy_env import EnemyEnv

env = EnemyEnv()

model = PPO(
    "MlpPolicy",
    env,
    verbose=1,
    learning_rate=3e-4,
    n_steps=2048,
    batch_size=64
)

model.learn(total_timesteps=500000)

model.save("enemy_ai")