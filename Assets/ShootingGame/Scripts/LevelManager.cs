using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

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

        private int bulletBuffCounts = 2;   // ��� ������ ���� ����
        public int BULLET_BUFF_COUNTS => bulletBuffCounts;

        public TextMeshProUGUI BombCountTx;

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
                diff = GetDifficulty.GetEasy();
            else if (difficulty == Difficulty.Normal)
                diff = GetDifficulty.GetNormal();
            else if (difficulty == Difficulty.Hard)
                diff = GetDifficulty.GetHard();
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
        /// <summary>
        /// ������ ȹ������ ��ȭ �Ѿ� �߰�
        /// </summary>
        public void AddPowerBullet()
        {
            bulletBuffCounts++;
            // TODO ��ȭ�Ѿ� UI, ����
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
            BombCount();
        }
        void ToyCollidesListener()
        {
            m_gameOverCountdown = 5f;
        }
        void BombCount()
        {
            BombCountTx.text =  bulletCounts.ToString();
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// ���� ���� ���� ���� �������� (Easy)
        /// </summary>
        /// <returns></returns>
        IEnumerator SpawnToysCo()
        {
            yield return null;
            // TODO �Ѿ� UI
            bulletCounts = diff.BULLET_COUNT;

            for (int i = 0; i < spawnLists.Count; i++)
            {
                for (int j = 0; j < spawnLists[i].Count; j++)
                {
                    int rnd = Random.Range(0, diff.BOUND);
                    GameObject tgt = prefabs[rnd];

                    Vector3 rot = Camera.main.transform.position - spawnLists[i][j].position;
                    rot.y = 0;
                    GameObject go = Instantiate(tgt, spawnLists[i][j].position, 
                        Quaternion.LookRotation(rot), toyParent);
                    //go.transform.localScale *= diff.TOY_SIZE;
                    yield return new WaitForSeconds(0.1f);
                }
            }
            GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>().SetTimer(diff.ITEM_WAITTIME);
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
            Slider slider = GameObject.Find("Slider_BombTimer").GetComponent<Slider>();
            m_fireCountdown = diff.FIRE_COUNTDOWN;
            while (true)
            {
                yield return null;
                m_fireCountdown -= Time.deltaTime;
                //Debug.Log(diff.FIRE_COUNTDOWN);
                slider.value = (float)(m_fireCountdown / diff.FIRE_COUNTDOWN);
                if (m_fireCountdown < 0)
                {
                    // �Ѿ� �ϳ� ���ҷ� �ٲٱ�
                    PlayerFiresListener();
                    m_fireCountdown = diff.FIRE_COUNTDOWN;
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
