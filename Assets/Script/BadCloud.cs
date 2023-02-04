using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCloud : khBase
{
    //소실 점 위치 x,y (z는 적당한 깊이)
    public Vector3  m_VanishingPoint;
    //스피드
    public float    m_Speed;
    // Y좌표 이상 나가면 사라진다 (음수 양수 상관없이 절대값)
    public float    m_OutPosY;
    public float    m_CloundIncreaseScale = 0.2f;
    //플레이어와 부딪힐 y 범위
    public float    m_ColStartPosY; //얘가 더 높음
    public float    m_ColEndPosY;
    //날라갈 방향 최종 범위
    public float    m_LeftPosX;
    public float    m_RightPosX;

    //날라갈 방향
    private Vector2     m_v2Dir;
    private float       m_fAddSpeed = 0f;

    //오브젝트 키면 바로 실행 됨
    private void OnEnable()
    {
        Init();
    }

    // 오브젝트 풀링시, 풀링하면서 해당 함수를 호출 해 줘야 함.
    public override void Init()
    {
        base.Init();

        //추가되는 속도
        m_fAddSpeed = 0f;

        //첫 시작 위치는 소실점
        transform.position      = m_VanishingPoint;

        //작은 점부터 점점 커지게 만들 예정이라 (1,1)이 최대
        float fScaleZ           = transform.localScale.z;
        Vector3 v3CloudScale    = Vector3.zero;
        v3CloudScale.z          = fScaleZ;
        transform.localScale    = v3CloudScale;

        //랜덤 X값
        float fRandom = Random.Range(m_LeftPosX, m_RightPosX);

        //Dir 구하기
        Vector2 v2EndPos    = new Vector2(fRandom, m_OutPosY);
        Vector2 v2StartPos  = m_VanishingPoint;
        m_v2Dir             = v2EndPos - v2StartPos;
        m_v2Dir             = m_v2Dir.normalized;

        //움직임 시작
        StartCoroutine(IECloundMove());
    }

    //구름 움직임
    IEnumerator IECloundMove ()
    {
        Vector3 v3Scale = transform.localScale;

        while (transform.position.y > m_OutPosY)
        {
            //구름의 위치 이동
            Vector2 v2Dir       = transform.position;
            //v2Dir.x             = m_v2Dir.x;
            //v2Dir.y             = m_v2Dir.y;
            v2Dir               += m_v2Dir.normalized * Time.deltaTime * (m_Speed + m_fAddSpeed);
            m_fAddSpeed         += Time.deltaTime*2f;
            transform.position  = v2Dir;

            //구름의 크기 변경
            float fTimeScale = Time.deltaTime * m_CloundIncreaseScale;
            v3Scale.x       += fTimeScale;
            v3Scale.y       += fTimeScale;
            v3Scale.z       += fTimeScale;

            ////구름 크기 제한
            //if (v3Scale.x > 1f)
            //{
            //    v3Scale.x = v3Scale.y = v3Scale.z = 1f;
            //}

            transform.localScale = v3Scale;

            yield return null;
        }

        End();
    }

    //움직임이 끝나든, 부딫히든 해서 끝나는 함수
    private void End()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //높이 확인
        float fPosY = transform.position.y;
        //현재 Y 포스가 Start~End 사이에 있다면 (End가 아래)
        if (fPosY >= m_ColEndPosY || fPosY <= m_ColStartPosY)
        {
            //플레이어와 부딪혔다면
            if(collision.CompareTag("Player"))
            {
                //플레이어 충돌 시 호출 함수


                //끝 (부딪힐 때 없어지는 게 아니면 뺀다)
                End();
            }
        }
    }
}

