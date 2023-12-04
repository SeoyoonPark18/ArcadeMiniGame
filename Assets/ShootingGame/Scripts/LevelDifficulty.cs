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
    public class LevelDifficulty
    {
        protected Difficulty difficulty;
        protected float fireCountdown;
        protected int bulletCount;
        protected float toySpeed;
        protected float toySize;

        public Difficulty DIFFICULTY => difficulty;
        public float FIRE_COUNTDOWN => fireCountdown;
        public int BULLET_COUNT => bulletCount;
        public float TOY_SPEED => toySpeed;
        public float TOY_SIZE => toySize;
    }
    public class Easy : LevelDifficulty
    {
        Difficulty difficulty = Difficulty.Easy;
        float fireCountdown = 15f;
        int bulletCount = 30;
        float toySpeed = 0f;
        float toySize = 1f;
    }
    public class Normal : LevelDifficulty
    {
        Difficulty difficulty = Difficulty.Normal;
        float fireCountdown = 10f;
        int bulletCount = 30;
        float toySpeed = 0.5f;
        float toySize = 1f;
    }
    public class Hard : LevelDifficulty
    {
        Difficulty difficulty = Difficulty.Hard;
        float fireCountdown = 5f;
        int bulletCount = 30;
        float toySpeed = 1f;
        float toySize = 0.5f;
    }
}