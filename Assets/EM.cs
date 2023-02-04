using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EM : MonoBehaviour
{
    public SpriteRenderer m_EMSpriteRenderer;
    //±ôºýÀÌ´Â ½Ã°£
    public float m_Time = 0.5f;
    //¾ËÆÄ ¼Óµµ
    public float m_AlphaSpeed = 10f;

    //ÁÂ¿ì ±ôºýÀÌ ½ÃÀü
    public void OnEM(Transform _Transform)
    {
        gameObject.SetActive(true);

        if(_Transform.position.x > 0)
        {
            transform.position = new Vector3(0.73f, -3.87f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, -2.096f);
            StartCoroutine(StartEM(true));
        }
        else
        {
            transform.position = new Vector3(-0.73f, -3.87f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 57.904f);
            StartCoroutine(StartEM(false));
        }
    }

    //ÁÂ¿ì ±ôºýÀÌ ½ÃÀü2
    IEnumerator StartEM(bool _bRight)
    {
        Color color     = m_EMSpriteRenderer.color;
        color.a         = 1f;
        bool bAlphaUp   = false;
        float fTimeTemp = 0f;
        while(fTimeTemp < m_Time)
        {
            if(bAlphaUp)
            {
                color.a += Time.deltaTime * m_AlphaSpeed;
                if(color.a > 1f)
                {
                    color.a     = 1f;
                    bAlphaUp    = false;
                }
            }
            else
            {
                color.a -= Time.deltaTime * m_AlphaSpeed;
                if(color.a < 0f)
                {
                    color.a = 0f;
                    bAlphaUp = true;
                }
            }
            m_EMSpriteRenderer.color = color;
            yield return null;
            fTimeTemp += Time.deltaTime;
        }

        gameObject.SetActive(false);
    }
}
