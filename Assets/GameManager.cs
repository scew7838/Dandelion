using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Animator canvasAnim;
    public Animator stoneAnim;
    public Animator roadAnim;

    public SpriteRenderer blur;

    public Text timer;
    
    public Image fade;
    public Seed seed;
    public Transform Character;

    public int startSpeed = 3;
    public bool go = false;
    
    const string gone = "Gone";
    const string end = "End";


    int choosenSeed;
    Transform road;
    AudioSource source;
    AudioClip clip;
    public bool gameEnd = false;
    public bool gameEnd2 = false;
    public AnimationCurve curve;

    public Transform[] blocks = new Transform[6];
    public Sprite[] sprites;
    public CanvasGroup Endto;

    public SpriteRenderer flower;
    public SpriteRenderer flower2;
    public float playtime;
    IEnumerator StartGame_Object()
    {
        while (!gameEnd)
        {

            for (int i = 0; i < 100; i++)
            {
                int rand = Random.Range(0, 6);
                Transform temp = blocks[rand];
                blocks[rand] = blocks[0];
                blocks[0] = temp;
            }

            for (int i = 0; i < 6; i++)
            {

                Vector3 vec = new Vector3(-Random.Range(-2f, 2f), Character.position.y+5, 6);

                blocks[i].position = RecalculateVecteor(vec).Item1;
                blocks[i].localScale = Vector3.one * RecalculateVecteor(vec).Item2;
                blocks[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                blocks[i].gameObject.SetActive(true);
                float time = 0;
                bool first = true;
                while (curve.Evaluate(time) < 1f)
                {

                    if (curve.Evaluate(time) > 0.8f)
                    {
                        if (first)
                        {

                            if (Mathf.Abs(Character.position.x - vec.x) < 1)
                            {
                                roadAnim.speed -= 0.03f;
                                Character.position -= Vector3.up*0.03f;
                                if (roadAnim.speed < 1.4f)
                                    roadAnim.speed = 1.4f;
                                if (Character.position.y < -4f)
                                    Character.position = Vector3.down * 4f;




                                StartCoroutine(Red());

                            }



                            first = false;
                        
                        }



                        blocks[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - (curve.Evaluate(time) - 0.8f)/0.2f);
                    }
                    else
                    {
                        blocks[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time * 7f);
                    }

                    Vector3 now = vec;
                    now.z = (6 - curve.Evaluate(time) * 6);
                    blocks[i].position = RecalculateVecteor(now).Item1;
                    blocks[i].localScale = Vector3.one * RecalculateVecteor(now).Item2;
                    yield return null;
                    time += Time.deltaTime * roadAnim.speed / 3f;
                }
                blocks[i].gameObject.SetActive(false);



            }





        }






    }

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(this);
            return;
        }
        timer.text = "";
        instance = this;
        roadAnim.speed = 0;
        road = roadAnim.transform;

        source = gameObject.AddComponent<AudioSource>();
        clip = Microphone.Start(Microphone.devices[0].ToString(), true, 1, 44100);
        source.clip = clip;
        source.mute = true;
        source.loop = true;

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
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;

            float[] clipdata = new float[44100];
            clip.GetData(clipdata, source.timeSamples);
            float maxSound = 0;
            for (int i = 0; i < clipdata.Length; i++)
            {
                if (clipdata[i] > maxSound)
                    maxSound = clipdata[i];
            }


            if (maxSound > 0.5f)
            {
                break;
            }
            yield return null;
        }
        Microphone.End(Microphone.devices[0].ToString());
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
        
        float delta = 0;
        while (delta < 1)
        {
            roadAnim.speed = delta*startSpeed;
            blur.color = new Color(1, 1, 1, 1 - delta/2);
            road.position = Vector3.up * (1 - delta/2)+Vector3.forward*20;
            yield return null;
            delta += Time.deltaTime;
        }
        roadAnim.speed = startSpeed;
        while (delta < 2)
        {
            Character.position = Vector3.up * 6 * (2.4f - 1.4f*delta);
            blur.color = new Color(1, 1, 1, 1 - delta / 2);
            road.position = Vector3.up * (1 - delta / 2) + Vector3.forward * 20;
            yield return null;
            delta += Time.deltaTime;
        }

        StartCoroutine("StartGame_Object");

        Character.position = Vector3.up*-2.4f;
        blur.color = new Color(1, 1, 1, 0);
        road.position = Vector3.zero + Vector3.forward * 20;
        yield return new WaitUntil(() => canvasAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        yield return new WaitUntil(() => stoneAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        float Timer = playtime;

        while (true&&Timer>0)
        {




            float go = Input.acceleration.x*5;
            go=Mathf.Clamp(go, -2.5f, 2.5f);


            timer.text = Mathf.CeilToInt(Timer).ToString();


            float dtime = Time.fixedDeltaTime;
            Timer -= dtime;
            if (Character.position.x + go * dtime < -2)
            {

                Character.position = new Vector3(-2, Character.position.y, 0);
            }
            else if (Character.position.x + go * dtime > 2)
            {

                Character.position = new Vector3(2, Character.position.y, 0);
            }
            else
            {
                Character.position += Vector3.right * go * dtime;
            }

            if (Character.position.y - 0.0981f * dtime > -4f)
            {
                Character.position += Vector3.up * -0.0981f * dtime;
                roadAnim.speed -= 0.0981f * dtime;
            }
            else
            {
                Character.position = new Vector3(Character.position.x, -4f, 0);
                roadAnim.speed = 1.4f;
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        gameEnd = true;

        StopCoroutine("StartGame_Object");

        timer.text = "";


        if (Timer > 0)
        {
            delta = 0;
            while (delta < 1)
            {

                fade.color = new Color(1, 1, 1, delta);
                yield return null;
                delta += Time.deltaTime;
            }
            fade.color = Color.white;

            SceneManager.LoadScene(0);
            yield break;
        }




        float delta0 = 0;
        while (delta0 < 2)
        {
            for (int i = 0; i < 6; i++)
            {
                blocks[i].GetComponent<SpriteRenderer>().color = blocks[i].GetComponent<SpriteRenderer>().color * (2 - delta) / 2f;
            }
            yield return null;
            delta0 += Time.deltaTime;
        }
        for (int i = 0; i < 6; i++)
        {
            blocks[i].GetComponent<SpriteRenderer>().color = Color.clear;
        }


        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.GetInt("seed" + i.ToString(), 0) != 0)
                count++;

        }

        if (count == 0)
        {
            flower.sprite = null;
        }
        else
        {
            flower.sprite = sprites[count - 1];
        }

        gameEnd2 = true;

        roadAnim.Play(end);
        roadAnim.Update(0);
        yield return new WaitUntil(() => roadAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        delta = 0;
        PlayerPrefs.SetInt("seed" + choosenSeed.ToString(), 1);

        while (delta < 1)
        {

            Character.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - delta);

            yield return null;
            delta += Time.deltaTime;
        
        }
        Character.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);



        count = 0;
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.GetInt("seed" + i.ToString(), 0) != 0)
                count++;

        }
        flower2.sprite = sprites[count - 1];

        delta = 0;
        while (delta < 1)
        {

            flower.color = new Color(1, 1, 1, 1 - delta);
            flower2.color = new Color(1, 1, 1, delta);

            yield return null;
            delta += Time.deltaTime;
        
        }
        flower.color = new Color(1, 1, 1, 0);
        flower2.color = new Color(1, 1, 1, 1);





        if (count == 5)
        {
            for (int i = 0; i < 5; i++)
            {
               
                    PlayerPrefs.SetInt("seed" + i.ToString(), 0);
                
            }
            
            yield return new WaitForSeconds(2f);
            Endto.alpha = 1;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        }


        delta = 0;
        while (delta < 1)
        {

            fade.color = new Color(1, 1, 1, delta);
            yield return null;
            delta += Time.deltaTime;
        }
        fade.color = Color.white;

        SceneManager.LoadScene(0);




    }


    public (Vector2,float) RecalculateVecteor(Vector3 vec)
    {

        vec.z = 6 - vec.z;

        float scale = ((1f + (vec.z / 6f)) / 2f);
        float floor = 1 - vec.z;
        float floorToY = (0.15f+0.85f* (vec.z / 6f))*(vec.y) * scale;
        float floorToX = (0.4f + 0.60f * (vec.z / 6f)) * (vec.x) * scale;

        scale = (vec.z/6f);



        return (new Vector2(floorToX, floor + floorToY),scale);
    }


    public GameObject red;
    IEnumerator Red()
    {

        red.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        red.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.15f);
        red.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        red.gameObject.SetActive(false);
    }


}
