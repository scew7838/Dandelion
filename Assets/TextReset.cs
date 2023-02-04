using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextReset : MonoBehaviour
{
    public float m_fHalfWidth;
    public float m_fHalfHeight;
    Vector3 m_v3Pos;
    private void Awake()
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        m_fHalfWidth = rectTransform.rect.width * 0.5f;
        m_fHalfHeight = rectTransform.rect.height * 0.5f;
        m_v3Pos = transform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector3 v3Pos = Input.GetTouch(0).position;
            if (v3Pos.x > m_v3Pos.x - m_fHalfWidth
                && v3Pos.x < m_v3Pos.x + m_fHalfWidth
                && v3Pos.y > m_v3Pos.y - m_fHalfHeight
                && v3Pos.y < m_v3Pos.y + m_fHalfHeight)
            {
                ResetTouch();
            }
        }
    }

    private void ResetTouch()
    {
        for(int i =0; i < 5; ++i)
        {
            PlayerPrefs.SetInt("seed" + i.ToString(), 0);
        }
        SceneManager.LoadScene(0);

    }
}
