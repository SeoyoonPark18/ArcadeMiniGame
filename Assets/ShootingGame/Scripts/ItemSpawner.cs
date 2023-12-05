using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingGame
{

    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] GameObject buffItemFactory;
        [SerializeField] GameObject pooItemFactory;

        private float waitTime = 5f;
        private float shootPower = 10f;

        public void SetTimer(float v_timer)
        {
            waitTime = v_timer;
            StartCoroutine(StartTimer());
        }

        IEnumerator StartTimer()
        {
            while (true)
            {
                int itemSelect = Random.Range(0, 2);
                int itemPosSelect = Random.Range(0, transform.childCount);
                if(itemSelect == 0) // 버프아이템 생성
                {
                    GameObject go = Instantiate(buffItemFactory, transform.GetChild(itemPosSelect).transform.position,
                        transform.GetChild(itemPosSelect).transform.rotation);
                    go.GetComponent<Rigidbody>().AddForce(go.transform.forward * shootPower, ForceMode.Impulse);
                }
                else if(itemSelect == 1) // 방해아이템 생성
                {
                    GameObject go = Instantiate(pooItemFactory, transform.GetChild(itemPosSelect).transform.position,
                        transform.GetChild(itemPosSelect).transform.rotation);
                    go.GetComponent<Rigidbody>().AddForce(go.transform.forward * shootPower, ForceMode.Impulse);
                }
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

}