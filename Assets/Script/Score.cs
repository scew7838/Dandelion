using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public  TextMeshProUGUI m_ScoreTxt1;
    public  TextMeshProUGUI m_ScoreTxt10;
    private float           m_fTime;
    private float           m_fTimeSource; //���� �� ��� �� ����

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
            //0�ʸ� ��� �����ֱ� ���� (�׷��� ��ǻ� �Ϻ��� �ð��ʴ� �ƴ�, 0,5�� �� ��.
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
