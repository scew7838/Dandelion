using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cha : MonoBehaviour
{
    public float m_fScaleLowRatio = 0.3f;

    private float m_fGoalPosX;

    private float m_fSpeed = 2f;

    private bool m_bLastMoveStart = false;

    public void LastMove()
    {
        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.GetInt("seed" + i.ToString(), 0) != 0)
                count++;

        }
        switch(count)
        {
            case 0:
                m_fGoalPosX = -1.85f + 0.5f;
                break;
            case 1:
                m_fGoalPosX = 2.01f + 1f;
                break;
            case 2:
                m_fGoalPosX = -0.84f + 0.5f;
                break;
            case 3:
                m_fGoalPosX = -2.78f - 0.3f;
                break;
            case 4:
                m_fGoalPosX = 0.54f +0.5f;
                break;
        }
        m_bLastMoveStart = true;
    }
    private void Update()
    {
        if (m_bLastMoveStart)
        {
            if (transform.localScale.x > 0f)
            {
                transform.localScale -= Vector3.one * Time.deltaTime * m_fScaleLowRatio;
            }
            //오른쪽으로 가야 한다면
            Vector3 v3Pos = transform.position;
            if (m_fGoalPosX > 0f)
            {
                v3Pos.x += Time.deltaTime * m_fSpeed;
                if (v3Pos.x > m_fGoalPosX)
                {
                    v3Pos.x = m_fGoalPosX;
                }
            }
            else
            {
                v3Pos.x -= Time.deltaTime * m_fSpeed;
                if (v3Pos.x < m_fGoalPosX)
                {
                    v3Pos.x = m_fGoalPosX;
                }
            }
            transform.position = v3Pos;
        }
    }
}
