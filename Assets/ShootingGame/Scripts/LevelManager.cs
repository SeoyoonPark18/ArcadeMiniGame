using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ShootingGame
{
    public class LevelManager : MonoBehaviour
    {
        // singleton
        private static LevelManager instance;
        public static LevelManager Instance => instance;

        // events
        [HideInInspector] public UnityEvent playerFiresEvent; // �߻� ī��Ʈ�ٿ� �ʱ�ȭ, �Ѿ˰��� -1
        [HideInInspector] public UnityEvent toyCollidesEvent; // ���� ���� Ȯ�ο� �̺�Ʈ

        // status
        private bool isGameover = false;    // ���� ����Ǿ��°�
        public bool IS_GAMEOVER => isGameover;

        private int bulletCounts;   // ���� ���� �Ѿ� ����
        public int BULLET_COUNTS => bulletCounts;

        private int bulletBuffCounts = 0;   // ��� ������ ���� ����
        public int BULLET_BUFF_COUNTS => bulletBuffCounts;

        // variants
        [SerializeField] private Transform[] serial_spawns = new Transform[5];  // �ν����Ϳ�
        List<List<Transform>> spawnLists;   // ������� ��������Ʈ

        public List<GameObject> prefabs = new List<GameObject>();   // ������ ���� �����յ�
        Transform toyParent;    // ������ ������ transfrom

        private void Awake()
        {
            // singleton
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

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
            toyCollidesEvent.AddListener(ToyCollidesListener);

            // TODO ����׿� ���߿� ����
            StartGame(Difficulty.Easy);
        }

        #region Functions
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
        /// ��ȭ�� �Ѿ� ��� ���� �� true ����,
        /// ��� �Ұ����ϸ� false ����
        /// </summary>
        /// <returns></returns>
        public bool UsePowerBullet()
        {
            if (bulletBuffCounts > 0)
            {
                bulletBuffCounts--;
                return true;
            }
            return false;
        }
        public void GameOver()
        {
            Debug.Log("Game Over!!!");
            isGameover = true;
            // TODO �������� UI �˾�
        }
        #endregion

        #region Listeners
        /// <summary>
        /// �÷��̾ �Ѿ� �߻� �� �۵��� �̺�Ʈ
        /// </summary>
        void PlayerFiresListener()
        {
            m_fireCountdown = diff.FIRE_COUNTDOWN;  // �Ѿ� ���� �߻� �ð� �ʱ�ȭ
            if (--bulletCounts == 0)
            {
                // �Ѿ� ��� �Ҹ�, ���� ��
                if (IS_GAMEOVER == false)
                {
                    StartCoroutine(CheckGameOver());
                }
            }
        }
        void ToyCollidesListener()
        {
            m_gameOverCountdown = 5f;
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// ���� ���� ���� ���� �������� (Easy)
        /// </summary>
        /// <returns></returns>
        IEnumerator SpawnToysCo()
        {
            // TODO �Ѿ� UI
            bulletCounts = diff.BULLET_COUNT;

            for (int i = 0; i < spawnLists.Count; i++)
            {
                for (int j = 0; j < spawnLists[i].Count; j++)
                {
                    GameObject tgt = prefabs[Random.Range(0, prefabs.Count)];
                    Vector3 rot = Camera.main.transform.position - spawnLists[i][j].position;
                    rot.y = 0;
                    GameObject go = Instantiate(tgt, spawnLists[i][j].position, 
                        Quaternion.LookRotation(rot), toyParent);
                    //go.transform.localScale *= diff.TOY_SIZE;
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
            m_fireCountdown = diff.FIRE_COUNTDOWN;
            while (true)
            {
                yield return null;
                m_fireCountdown -= Time.deltaTime;
                // TODO �߻����� UI ǥ��
                if (m_fireCountdown < 0)
                {
                    // TODO �Ѿ� �ϳ� ���ҷ� �ٲٱ�
                    //break;
                }
            }
            // ���� ���� ���μ��� (�������� ��Ȳ�� �ƴϸ�)
            /*if(IS_GAMEOVER)
            {
                GameOver();
            }*/
        }

        IEnumerator ToyMoveCo()
        {
            yield return null;
            for(int i = 0; i < toyParent.childCount; i++)
            {
                MovableToy mov = toyParent.GetChild(i).GetComponent<MovableToy>();
                mov.SetSpeed(diff.TOY_SPEED);
                mov.StartMove();
            }
        }

        float m_gameOverCountdown = 5f;
        IEnumerator CheckGameOver()
        {
            while(true)
            {
                yield return null;
                m_gameOverCountdown -= Time.deltaTime;
                if (m_gameOverCountdown < 0)
                    break;
            }
            if(IS_GAMEOVER)
                GameOver();
        }
        #endregion

    }
}
