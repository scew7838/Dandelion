using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Animator canvasAnim;
    public Animator stoneAnim;
    public Animator roadAnim;

    public SpriteRenderer blur;

    
    public Image fade;
    public Seed seed;
    public Transform Character;

    public int startSpeed = 3;
    public bool go = false;
    
    const string gone = "Gone";


    int choosenSeed;
    Transform road;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        roadAnim.speed = 0;
        road = roadAnim.transform;

        StartCoroutine(FadeIn());


    }

    IEnumerator FadeIn()
    {
        float delta = 0;
        while (delta < 1)
        {

            fade.color = new Color(1, 1, 1, 1-delta);
            yield return null;
            delta += Time.deltaTime;
        }
        fade.color = Color.clear;

        while (!go)
        {
            yield return null;
        }
        Blow();


    }



    void Blow()
    {
        choosenSeed = seed.Blow();
        StartCoroutine(GameStart());
        
    }
    IEnumerator GameStart()
    {
        canvasAnim.Play(gone);
        canvasAnim.Update(0);
        stoneAnim.Play(gone);
        stoneAnim.Update(0);
        roadAnim.speed = startSpeed;
        float delta = 0;
        while (delta < 1)
        {
            blur.color = new Color(1, 1, 1, 1 - delta/2);
            road.position = Vector3.up * (1 - delta/2)+Vector3.forward*20;
            yield return null;
            delta += Time.deltaTime;
        }
        while (delta < 2)
        {
            Character.position = Vector3.up * 6 * (2.4f - 1.4f*delta);
            blur.color = new Color(1, 1, 1, 1 - delta / 2);
            road.position = Vector3.up * (1 - delta / 2) + Vector3.forward * 20;
            yield return null;
            delta += Time.deltaTime;
        }
        Character.position = Vector3.up*-2.4f;
        blur.color = new Color(1, 1, 1, 0);
        road.position = Vector3.zero + Vector3.forward * 20;
        yield return new WaitUntil(() => canvasAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        yield return new WaitUntil(() => stoneAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
    }

    

}
