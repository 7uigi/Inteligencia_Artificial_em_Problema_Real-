import gymnasium as gym
from gymnasium import spaces
import numpy as np
import random

class EnemyEnv(gym.Env):

    def __init__(self):
        super(EnemyEnv, self).__init__()

        # 4 ações possíveis
        self.action_space = spaces.Discrete(4)

        # observações:
        # distância player
        # hp inimigo
        # hp player
        # cooldown fire
        # cooldown ice
        self.observation_space = spaces.Box(
            low=0,
            high=100,
            shape=(6,),
            dtype=np.float32
        )

        self.reset()

    def reset(self, seed=None, options=None):

        self.enemy_hp = 100
        self.player_hp = 100

        self.distance = random.uniform(1, 20)

        self.fire_cd = 0
        self.ice_cd = 0

        self.missChance = 0

        obs = self._get_obs()

        return obs, {}

    def _get_obs(self):
        return np.array([
            self.distance,
            self.enemy_hp,
            self.player_hp,
            self.fire_cd,
            self.ice_cd,
            self.missChance
        ], dtype=np.float32)

    def step(self, action):

        reward = 0
        done = False

        # FIRE
        if action == 0:
            self.missChance = random.randint(0, 10)
            if self.fire_cd <= 0:
                if self.missChance >= 5: 
                    damage = random.randint(8, 15)
                    self.player_hp -= damage 
                    reward += 15
                    self.fire_cd = 6
                else:
                    reward -= 1
                
            else:
                reward -= 10

        # ICE
        elif action == 1:
            self.missChance = random.randint(0, 10)
            if self.ice_cd <= 0:
                if self.missChance >= 3: 
                    damage = random.randint(5, 10) 
                    self.player_hp -= damage 
                    reward += 12
                    self.ice_cd = 3
                else:
                    reward -= 2
            else:
                reward -= 20

        # MELEE
        elif action == 2:

            if self.distance < 10:
                self.missChance = random.randint(2, 10)
                reward += 18
                self.distance -= 2
            else:
                self.missChance = random.randint(0, 10)
                reward -= 5

        # DODGE
        elif action == 3:
            self.missChance = random.randint(0, 10)
            reward += 2

            # aumenta distância
            self.distance += 2

            if self.distance > 20: 
                reward -= 5

        # player contra-ataca
        if self.distance < 5:
            self.enemy_hp -= random.randint(5, 15)

        if self.missChance > 8:
            self.enemy_hp -= 20

        if self.distance > 20:
            self.enemy_hp -= random.randint(20,30)

        # cooldowns
        self.fire_cd = max(0, self.fire_cd - 1)
        self.ice_cd = max(0, self.ice_cd - 1)

        # movimentação aleatória
        self.distance += random.uniform(-1, 1)
        self.distance = np.clip(self.distance, 1, 20)


        # terminou?
        if self.player_hp <= 0:
            reward += 50
            done = True

        if self.enemy_hp <= 0:
            reward -= 50
            done = True

        obs = self._get_obs()

        return obs, reward, done, False, {}
