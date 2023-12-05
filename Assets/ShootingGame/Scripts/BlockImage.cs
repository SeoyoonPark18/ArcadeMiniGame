using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlockImage : MonoBehaviour
{
    public float duration = 2.5f;
    private float elapsed;
    private Image img;

    private void Start()
    {
        elapsed = duration;
        img = GetComponent<Image>();

        StartCoroutine(LifeCycle());
    }
    IEnumerator LifeCycle()
    {
        while(true)
        {
            elapsed -= Time.deltaTime;
            img.color = new Vector4(img.color.r, img.color.g, img.color.b, 
                elapsed / duration);    // ≈ı∏Ì»≠


            yield return null;
            if (elapsed < 0)
                break;
        }
        Destroy(this.gameObject);
    }
}
