using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }
    public struct LevelDifficulty
    {
        public Difficulty difficulty;
        public float fireCountdown;
        public int bulletCount;
        public float toySpeed;
        public float toySize;
        public int bound;

        public Difficulty DIFFICULTY => difficulty;
        public float FIRE_COUNTDOWN => fireCountdown;
        public int BULLET_COUNT => bulletCount;
        public float TOY_SPEED => toySpeed;
        public float TOY_SIZE => toySize;
        public int BOUND => bound;
    }

    public class GetDifficulty
    {
        public static LevelDifficulty GetEasy()
        {
            return new LevelDifficulty
            {
                difficulty = Difficulty.Easy,
                fireCountdown = 15f,
                bulletCount = 30,
                toySpeed = 0f,
                toySize = 1f,
                bound = 3
            };

        }
        public static LevelDifficulty GetNormal()
        {
            return new LevelDifficulty
            {
                difficulty = Difficulty.Normal,
                fireCountdown = 10f,
                bulletCount = 30,
                toySpeed = 0.5f,
                toySize = 1f,
                bound = 5
            };
        }
        public static LevelDifficulty GetHard()
        {
            return new LevelDifficulty
            {
                difficulty = Difficulty.Hard,
                fireCountdown = 5f,
                bulletCount = 30,
                toySpeed = 1f,
                toySize = 0.5f,
                bound = 8
            };
        }
    }
}