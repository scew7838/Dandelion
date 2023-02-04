using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    public Image        m_BackgroundImage;
    public float        m_FadeSpeed         = 1f;
    public GameObject   m_UIParent;

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color color = new Color(m_BackgroundImage.color.r, m_BackgroundImage.color.g, m_BackgroundImage.color.b, m_BackgroundImage.color.a);
        while(color.a < 1f)
        {
            color.a += Time.deltaTime * m_FadeSpeed;
            m_BackgroundImage.color = color;
            yield return null;
        }
        color.a = 1f;
        m_UIParent.SetActive(true);
    }
}
