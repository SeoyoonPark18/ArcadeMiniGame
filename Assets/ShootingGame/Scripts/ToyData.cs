using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{
    public class ToyData : MonoBehaviour
    {
        // set
        [SerializeField] private int id;
        [SerializeField] private int score;

        // get
        public int ID => id;
        public int SCORE => score;
    }
}