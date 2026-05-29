from stable_baselines3 import PPO
from enemy_env import EnemyEnv

env = EnemyEnv()

# carrega modelo treinado
model = PPO.load("enemy_ai")

# reinicia ambiente
obs, _ = env.reset()

while True:

    # IA escolhe ação
    action, _ = model.predict(obs)

    # executa ação
    obs, reward, done, _, _ = env.step(action)

    print("Ação escolhida:", action)

    if done:
        obs, _ = env.reset()

