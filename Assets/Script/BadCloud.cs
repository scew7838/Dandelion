using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCloud : khBase
{
    //�ҽ� �� ��ġ x,y (z�� ������ ����)
    public Vector3  m_VanishingPoint;
    //���ǵ�
    public float    m_Speed;
    // Y��ǥ �̻� ������ ������� (���� ��� ������� ���밪)
    public float    m_OutPosY;
    public float    m_CloundIncreaseScale = 0.2f;
    //�÷��̾�� �ε��� y ����
    public float    m_ColStartPosY; //�갡 �� ����
    public float    m_ColEndPosY;
    //���� ���� ���� ����
    public float    m_LeftPosX;
    public float    m_RightPosX;

    //���� ����
    private Vector2     m_v2Dir;
    private float       m_fAddSpeed = 0f;

    //������Ʈ Ű�� �ٷ� ���� ��
    private void OnEnable()
    {
        Init();
    }

    // ������Ʈ Ǯ����, Ǯ���ϸ鼭 �ش� �Լ��� ȣ�� �� ��� ��.
    public override void Init()
    {
        base.Init();

        //�߰��Ǵ� �ӵ�
        m_fAddSpeed = 0f;

        //ù ���� ��ġ�� �ҽ���
        transform.position      = m_VanishingPoint;

        //���� ������ ���� Ŀ���� ���� �����̶� (1,1)�� �ִ�
        float fScaleZ           = transform.localScale.z;
        Vector3 v3CloudScale    = Vector3.zero;
        v3CloudScale.z          = fScaleZ;
        transform.localScale    = v3CloudScale;

        //���� X��
        float fRandom = Random.Range(m_LeftPosX, m_RightPosX);

        //Dir ���ϱ�
        Vector2 v2EndPos    = new Vector2(fRandom, m_OutPosY);
        Vector2 v2StartPos  = m_VanishingPoint;
        m_v2Dir             = v2EndPos - v2StartPos;
        m_v2Dir             = m_v2Dir.normalized;

        //������ ����
        StartCoroutine(IECloundMove());
    }

    //���� ������
    IEnumerator IECloundMove ()
    {
        Vector3 v3Scale = transform.localScale;

        while (transform.position.y > m_OutPosY)
        {
            //������ ��ġ �̵�
            Vector2 v2Dir       = transform.position;
            //v2Dir.x             = m_v2Dir.x;
            //v2Dir.y             = m_v2Dir.y;
            v2Dir               += m_v2Dir.normalized * Time.deltaTime * (m_Speed + m_fAddSpeed);
            m_fAddSpeed         += Time.deltaTime*2f;
            transform.position  = v2Dir;

            //������ ũ�� ����
            float fTimeScale = Time.deltaTime * m_CloundIncreaseScale;
            v3Scale.x       += fTimeScale;
            v3Scale.y       += fTimeScale;
            v3Scale.z       += fTimeScale;

            ////���� ũ�� ����
            //if (v3Scale.x > 1f)
            //{
            //    v3Scale.x = v3Scale.y = v3Scale.z = 1f;
            //}

            transform.localScale = v3Scale;

            yield return null;
        }

        End();
    }

    //�������� ������, �΋H���� �ؼ� ������ �Լ�
    private void End()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //���� Ȯ��
        float fPosY = transform.position.y;
        //���� Y ������ Start~End ���̿� �ִٸ� (End�� �Ʒ�)
        if (fPosY >= m_ColEndPosY || fPosY <= m_ColStartPosY)
        {
            //�÷��̾�� �ε����ٸ�
            if(collision.CompareTag("Player"))
            {
                //�÷��̾� �浹 �� ȣ�� �Լ�


                //�� (�ε��� �� �������� �� �ƴϸ� ����)
                End();
            }
        }
    }
}

