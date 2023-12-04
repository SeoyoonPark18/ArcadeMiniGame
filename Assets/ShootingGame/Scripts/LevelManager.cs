using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ShootingGame
{
    public class LevelManager : MonoBehaviour
    {
        // events
        [HideInInspector] public UnityEvent playerFiresEvent; // 발사 카운트다운 초기화, 총알개수 -1

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
        }

        private void Start()
        {
            playerFiresEvent.AddListener(PlayerFiresListener);

            // TODO 디버그용 나중에 삭제
            StartGame(Difficulty.Easy);
        }


        /// <summary>
        /// 슈팅 게임 시작!!!
        /// </summary>
        /// <param name="difficulty">난이도</param>
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

            // 인형 스폰 코루틴 시작
            StartCoroutine(SpawnToysCo());
        }

        /// <summary>
        /// 게임 시작 전에 인형 스폰해줌 (Easy)
        /// </summary>
        /// <returns></returns>
        IEnumerator SpawnToysCo()
        {
            // TODO 총알 충전

            for (int i = 0; i < spawnLists.Count; i++)
            {
                for (int j = 0; j < spawnLists[i].Count; j++)
                {
                    // TODO 인형 소환 (크기 설정)
                    GameObject tgt = prefabs[Random.Range(0, prefabs.Count)];
                    Vector3 rot = Camera.main.transform.position - spawnLists[i][j].position;
                    rot.y = 0;
                    GameObject go = Instantiate(tgt, spawnLists[i][j].position, 
                        Quaternion.LookRotation(rot), toyParent);
                    yield return new WaitForSeconds(0.1f);
                }
            }
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
            // TODO 발사 카운트다운 시작
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
            // 게임 종료 프로세스 (게임종료 상황이 아니면)
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
            // TODO 인형들 움직임 시작
            yield return null;
        }

        public void GameOver()
        {
            Debug.Log("Game Over!!!");
            isGameover = true;
            // TODO 게임종료 UI 팝업
        }
    }
}
