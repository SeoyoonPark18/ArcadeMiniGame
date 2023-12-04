using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ShootingGame
{
    public class LevelManager : MonoBehaviour
    {
        // events
        [HideInInspector] public UnityEvent playerFiresEvent; // �߻� ī��Ʈ�ٿ� �ʱ�ȭ, �Ѿ˰��� -1

        // status
        private bool isGameover = false;
        public bool IS_GAMEOVER => isGameover;

        // variants
        [SerializeField] private Transform[] serial_spawns = new Transform[5];
        List<List<Transform>> spawnLists;
        public List<GameObject> prefabs = new List<GameObject>();
        Transform toyParent;

        private void Awake()
        {
            // ���� ���� �ʱ�ȭ    
            spawnLists = new List<List<Transform>>();
            for (int i = 0; i < serial_spawns.Length; i++)
            {
                spawnLists.Add(new List<Transform>());
                for (int j = 0; j < serial_spawns[i].childCount; j++)
                    spawnLists[i].Add(serial_spawns[i].GetChild(j));
            }

            // ���� �θ� ����
            toyParent = new GameObject("ToyParent").transform;
        }

        private void Start()
        {
            playerFiresEvent.AddListener(PlayerFiresListener);

            // TODO ����׿� ���߿� ����
            StartGame(Difficulty.Easy);
        }


        /// <summary>
        /// ���� ���� ����!!!
        /// </summary>
        /// <param name="difficulty">���̵�</param>
        LevelDifficulty diff;
        public void StartGame(Difficulty difficulty)
        {
            if (difficulty == Difficulty.Easy)
                diff = new Easy();
            else if (difficulty == Difficulty.Normal)
                diff = new Normal();
            else if (difficulty == Difficulty.Hard)
                diff = new Hard();
            else
                return;

            // ���� ���� �ڷ�ƾ ����
            StartCoroutine(SpawnToysCo());
        }

        /// <summary>
        /// ���� ���� ���� ���� �������� (Easy)
        /// </summary>
        /// <returns></returns>
        IEnumerator SpawnToysCo()
        {
            // TODO �Ѿ� ����

            for (int i = 0; i < spawnLists.Count; i++)
            {
                for (int j = 0; j < spawnLists[i].Count; j++)
                {
                    // TODO ���� ��ȯ (ũ�� ����)
                    GameObject tgt = prefabs[Random.Range(0, prefabs.Count)];
                    Vector3 rot = Camera.main.transform.position - spawnLists[i][j].position;
                    rot.y = 0;
                    GameObject go = Instantiate(tgt, spawnLists[i][j].position, 
                        Quaternion.LookRotation(rot), toyParent);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            // ��¥ ���� ����
            yield return null;

            // �߻� ī��Ʈ�ٿ� ����
            StartCoroutine(FireCountdownCo());
            // ���� ������ ����
            StartCoroutine(ToyMoveCo());
        }

        /// <summary>
        /// fireCountdown �ð� �ȿ� �Ѿ��� ���� ������ GameOver
        /// </summary>
        /// <param name="fireCountdown"></param>
        /// <returns></returns>
        private float m_fireCountdown;
        IEnumerator FireCountdownCo()
        {
            // TODO �߻� ī��Ʈ�ٿ� ����
            m_fireCountdown = diff.FIRE_COUNTDOWN;
            while (true)
            {
                yield return null;
                m_fireCountdown -= Time.deltaTime;
                if (m_fireCountdown < 0)
                {
                    break;
                }
            }
            // ���� ���� ���μ��� (�������� ��Ȳ�� �ƴϸ�)
            if(IS_GAMEOVER)
            {
                GameOver();
            }
        }
        void PlayerFiresListener()
        {
            m_fireCountdown = diff.FIRE_COUNTDOWN;
        }

        IEnumerator ToyMoveCo()
        {
            // TODO ������ ������ ����
            yield return null;
        }

        public void GameOver()
        {
            Debug.Log("Game Over!!!");
            isGameover = true;
            // TODO �������� UI �˾�
        }
    }
}
