using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LiveUI : MonoBehaviour
{
    public Image[] lifeImages; // 목숨을 표시할 이미지 배열
    public float gameTime = 90f; 
    private int currentLives; 

    void Start()
    {
        currentLives = lifeImages.Length; 
        UpdateLivesUI();
        StartCoroutine(Countdown());
    }

    
    IEnumerator Countdown()   // 30초마다 나타나는 코루틴
    {
        while (gameTime > 30f)
        {
            yield return new WaitForSeconds(30f);
            currentLives--;
            UpdateLivesUI();
        }

        // 여기서 게임 오버 처리 또는 다른 작업을 수행합니다.
        Debug.Log("Game Over!");
    }

    // UI를 업데이트하여 목숨을 표시합니다.
    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i < currentLives)
            {
                lifeImages[i].enabled = true; // 목숨 수보다 작은 인덱스의 이미지는 활성화
            }
            else
            {
                lifeImages[i].enabled = false; // 나머지 이미지는 비활성화
            }
        }
    }
}
    