using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public bool m_bEnd = false;

    private void Awake()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn ()
    {
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 0f;
        while (color.a < 1f)
        {
            gameObject.GetComponent<Image>().color = color;
            yield return null;
            color.a += Time.deltaTime;
        }
        color.a = 1f;
        gameObject.GetComponent<Image>().color = color;

        float fTime = 0f;
        while(fTime < 4f)
        {
            yield return null;
            fTime += Time.deltaTime;
        }

        m_bEnd = true;
    }
}
