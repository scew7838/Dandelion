using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public  TextMeshProUGUI m_ScoreTxt1;
    public  TextMeshProUGUI m_ScoreTxt10;
    private float           m_fTime;
    private float           m_fTimeSource; //원본 값 담아 둘 공간

    public  delegate void DelFunc();
    private DelFunc m_DelEndStartFunc;

    public void StartScore(float _fTimeCount, DelFunc _DelEndStartFunc)
    {
        m_fTime             = _fTimeCount;
        m_DelEndStartFunc   = null;
        m_fTimeSource       = m_fTime;
        m_DelEndStartFunc   += _DelEndStartFunc;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while(m_fTime > -0.5f)
        {
            //0초를 잠깐 보여주기 위해 (그래서 사실상 완벽한 시간초는 아님, 0,5초 더 줌.
            int iTimeTemp = (int)m_fTime + 1;

            m_ScoreTxt1.text    = (iTimeTemp % 10).ToString();
            m_ScoreTxt10.text   = (iTimeTemp / 10).ToString();
            yield return null;
            m_fTime -= Time.deltaTime;
        }
        m_ScoreTxt1.text    = "0";
        m_ScoreTxt10.text   = "0";
        m_DelEndStartFunc();
    }
}
