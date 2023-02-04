using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flowers : MonoBehaviour
{
    List<GameObject> m_FlowersObjList;
    int count = 0;

    public GameObject m_Ending;

    private void Awake()
    {
        m_FlowersObjList = new List<GameObject>();

        int iCount = transform.childCount;
        for(int i =0; i < iCount; ++i)
        {
            m_FlowersObjList.Add(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.GetInt("seed" + i.ToString(), 0) != 0)
                count++;
        }
    }

    public void FlowerOn()
    {
        for (int i = 0; i < count; ++i)
        {
            m_FlowersObjList[i].SetActive(true);
        }

        GetComponent<Animator>().Play("Flower");
    }

    public void FlowerAniOff()
    {
        GetComponent<Animator>().StopPlayback();
    }

    public void LastFlowerOn()
    {
        StartCoroutine(IELastFlowerOn());
    }
    IEnumerator IELastFlowerOn()
    {
        GameObject LastFlower = m_FlowersObjList[transform.childCount - 1];
        LastFlower.SetActive(true);
        Color color = LastFlower.GetComponent<SpriteRenderer>().color;
        color.a = 0f;
        while(color.a < 1f)
        {
            LastFlower.GetComponent<SpriteRenderer>().color = color;
            yield return null;
            color.a += Time.deltaTime * 2f;
        }

        float fDelayTime = 3f;
        while (fDelayTime > 0f)
        {
            yield return null;
            fDelayTime -= Time.deltaTime;
        }

        color.a = 1f;
        LastFlower.GetComponent<SpriteRenderer>().color = color;
        m_Ending.SetActive(true);
    }
}
