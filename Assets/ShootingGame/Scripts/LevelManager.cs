using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using System.Text;

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
        private int toyCounts = 0;


        // variants
        [SerializeField] private Transform[] serial_spawns = new Transform[5];  // �ν����Ϳ�
        List<List<Transform>> spawnLists;   // ������� ��������Ʈ

        public List<GameObject> prefabs = new List<GameObject>();   // ������ ���� �����յ�
        Transform toyParent;    // ������ ������ transfrom
        private TextMeshProUGUI BombCountTx;    // �Ѿ� ���� �ؽ�Ʈ
        private Canvas canvas;
        public List<BlockImage> pooImagePrefabs = new List<BlockImage>(); // ȭ�� ������ ���� �̹���


        private void Awake()
        {
            // singleton
            if (instance == null)
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

            // �Ѿ� ���� ī��Ʈ
            BombCountTx = GameObject.Find("TX_BombCount").GetComponent<TextMeshProUGUI>();

            // ĵ���� ã��
            canvas = GetComponentInChildren<Canvas>();
        }

        private void Start()
        {
            playerFiresEvent.AddListener(PlayerFiresListener);
            toyCollidesEvent.AddListener(ToyCollidesListener);


            // TODO ����׿� ���߿� ����
            StartGame(Difficulty.Normal);
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
            if (BULLET_BUFF_COUNTS > 0)
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
            BombCount();
        }
        public void AtePoo()
        {
            // TODO �̹��� ĵ������ ����
            int rnd = Random.Range(0, pooImagePrefabs.Count);
            Debug.Log(pooImagePrefabs.Count);
            Debug.Log(rnd);
            BlockImage img = Instantiate(pooImagePrefabs[rnd], canvas.transform);
            RectTransform rect = img.GetComponent<RectTransform>();
            //float rndX = Random.Range(640, 1280);
            //float rndY = Random.Range(360, 720);
            rect.position = new Vector3(960, 540, 0);
            Debug.Log("Ate Poo");
        }
        public void AddToy()
        {
            if (++toyCounts >= 18)
            {
                // TODO ���� �¸�
                Debug.Log("GAME WIN!!!!!");
                GameObject.Find("Round2-LiveMenu");
            }
            Debug.Log("cur toy: " + toyCounts);
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
            if (bulletCounts == 0)
                return;
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
            m_gameOverCountdown = GAME_OVER_COUNTDOWN;
        }
        void BombCount()
        {
            StringBuilder sb = new StringBuilder();
            if (BULLET_BUFF_COUNTS > 0)
                sb.Append("<color=#FF0000>");
            sb.Append(bulletCounts.ToString());
            if (BULLET_BUFF_COUNTS > 0)
                sb.Append("</color>");
            BombCountTx.text = sb.ToString();
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
            bulletCounts = diff.BULLET_COUNT;
            BombCount();

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
            for (int i = 0; i < toyParent.childCount; i++)
            {
                MovableToy mov = toyParent.GetChild(i).GetComponent<MovableToy>();
                mov.SetSpeed(diff.TOY_SPEED);
                mov.StartMove();
            }
        }

        float GAME_OVER_COUNTDOWN = 10f;
        float m_gameOverCountdown = 5f;
        IEnumerator CheckGameOver()
        {
            m_gameOverCountdown = GAME_OVER_COUNTDOWN;
            while (true)
            {
                yield return null;
                m_gameOverCountdown -= Time.deltaTime;
                if (m_gameOverCountdown < 0)
                    break;
            }
            if (IS_GAMEOVER)
                GameOver();
        }
        #endregion

    }
}
