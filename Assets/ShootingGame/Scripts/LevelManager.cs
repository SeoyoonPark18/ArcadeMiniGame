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

    public class LevelManager : MonoBehaviour
    {
        public List<List<Transform>> spawnPoints = new List<List<Transform>>(5);

        /// <summary>
        /// ���� ���� ����!!!
        /// </summary>
        /// <param name="difficulty">���̵�</param>
        public void StartGame(Difficulty difficulty)
        {

        }

        
    }
}
