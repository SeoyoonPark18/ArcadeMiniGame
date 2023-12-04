using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LiveUI : MonoBehaviour
{
    public Image[] lifeImages; // ����� ǥ���� �̹��� �迭
    public float gameTime = 90f; 
    private int currentLives; 

    void Start()
    {
        currentLives = lifeImages.Length; 
        UpdateLivesUI();
        StartCoroutine(Countdown());
    }

    
    IEnumerator Countdown()   // 30�ʸ��� ��Ÿ���� �ڷ�ƾ
    {
        while (gameTime > 30f)
        {
            yield return new WaitForSeconds(30f);
            currentLives--;
            UpdateLivesUI();
        }

        // ���⼭ ���� ���� ó�� �Ǵ� �ٸ� �۾��� �����մϴ�.
        Debug.Log("Game Over!");
    }

    // UI�� ������Ʈ�Ͽ� ����� ǥ���մϴ�.
    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i < currentLives)
            {
                lifeImages[i].enabled = true; // ��� ������ ���� �ε����� �̹����� Ȱ��ȭ
            }
            else
            {
                lifeImages[i].enabled = false; // ������ �̹����� ��Ȱ��ȭ
            }
        }
    }
}
    