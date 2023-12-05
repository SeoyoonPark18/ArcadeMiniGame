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
        [HideInInspector] public UnityEvent playerFiresEvent; // 발사 카운트다운 초기화, 총알개수 -1
        [HideInInspector] public UnityEvent toyCollidesEvent; // 게임 종료 확인용 이벤트

        // status
        private bool isGameover = false;    // 게임 종료되었는가
        public bool IS_GAMEOVER => isGameover;

        private int bulletCounts;   // 현재 남은 총알 개수
        public int BULLET_COUNTS => bulletCounts;

        private int bulletBuffCounts = 2;   // 사용 가능한 버프 개수
        public int BULLET_BUFF_COUNTS => bulletBuffCounts;
        private int toyCounts = 0;


        // variants
        [SerializeField] private Transform[] serial_spawns = new Transform[5];  // 인스펙터용
        List<List<Transform>> spawnLists;   // 이지모드 스폰포인트

        public List<GameObject> prefabs = new List<GameObject>();   // 생성할 인형 프리팹들
        Transform toyParent;    // 인형이 생성될 transfrom
        private TextMeshProUGUI BombCountTx;    // 총알 개수 텍스트
        private Canvas canvas;
        public List<BlockImage> pooImagePrefabs = new List<BlockImage>(); // 화면 가리는 방해 이미지


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

            // 스폰 지점 초기화    
            spawnLists = new List<List<Transform>>();
            for (int i = 0; i < serial_spawns.Length; i++)
            {
                spawnLists.Add(new List<Transform>());
                for (int j = 0; j < serial_spawns[i].childCount; j++)
                    spawnLists[i].Add(serial_spawns[i].GetChild(j));
            }

            // 인형 부모 생성
            toyParent = new GameObject("ToyParent").transform;

            // 총알 개수 카운트
            BombCountTx = GameObject.Find("TX_BombCount").GetComponent<TextMeshProUGUI>();

            // 캔버스 찾기
            canvas = GetComponentInChildren<Canvas>();
        }

        private void Start()
        {
            playerFiresEvent.AddListener(PlayerFiresListener);
            toyCollidesEvent.AddListener(ToyCollidesListener);


            // TODO 디버그용 나중에 삭제
            StartGame(Difficulty.Normal);
        }

        #region Functions
        /// <summary>
        /// 슈팅 게임 시작!!!
        /// </summary>
        /// <param name="difficulty">난이도</param>
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

            // 인형 스폰 코루틴 시작
            StartCoroutine(SpawnToysCo());
        }
        /// <summary>
        /// 강화된 총알 사용 가능 시 true 리턴,
        /// 사용 불가능하면 false 리턴
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
        /// 아이템 획득으로 강화 총알 추가
        /// </summary>
        public void AddPowerBullet()
        {
            bulletBuffCounts++;
            BombCount();
        }
        public void AtePoo()
        {
            // TODO 이미지 캔버스에 생성
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
                // TODO 게임 승리
                Debug.Log("GAME WIN!!!!!");
                GameObject.Find("Round2-LiveMenu");
            }
            Debug.Log("cur toy: " + toyCounts);
        }
        public void GameOver()
        {
            Debug.Log("Game Over!!!");
            isGameover = true;
            // TODO 게임종료 UI 팝업
        }
        #endregion

        #region Listeners
        /// <summary>
        /// 플레이어가 총알 발사 시 작동할 이벤트
        /// </summary>
        void PlayerFiresListener()
        {
            if (bulletCounts == 0)
                return;
            m_fireCountdown = diff.FIRE_COUNTDOWN;  // 총알 제한 발사 시간 초기화
            if (--bulletCounts == 0)
            {
                // 총알 모두 소모, 게임 끝
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
        /// 게임 시작 전에 인형 스폰해줌 (Easy)
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
            // 진짜 게임 시작
            yield return null;

            // 발사 카운트다운 시작
            StartCoroutine(FireCountdownCo());
            // 인형 움직임 시작
            StartCoroutine(ToyMoveCo());
        }

        /// <summary>
        /// fireCountdown 시간 안에 총알을 쏘지 않으면 GameOver
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
                    // 총알 하나 감소로 바꾸기
                    PlayerFiresListener();
                    m_fireCountdown = diff.FIRE_COUNTDOWN;
                    //break;
                }
            }
            // 게임 종료 프로세스 (게임종료 상황이 아니면)
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
