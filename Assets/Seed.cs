using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{

    public float windSpeed=0;
    [SerializeField]
    float limit = 20;

    [SerializeField]
    float[] windMultiplier;

    Quaternion[] quaternions;
    float[] totalRotation;
    Coroutine breeze;


    // Start is called before the first frame update
    void Start()
    {

        quaternions = new Quaternion[transform.childCount];
        totalRotation = new float[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            if (PlayerPrefs.GetInt("seed" + i.ToString()) == 1)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.clear;
            }
            quaternions[i] = transform.GetChild(i).rotation;
            totalRotation[i] = 0;
           
        }

        breeze = StartCoroutine(StartBreeze());

    }



    IEnumerator StartBreeze()
    {
        while (true)
        {
            while (windSpeed > -20)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    totalRotation[i] += windSpeed * windMultiplier[i] * Time.deltaTime;
                    if (totalRotation[i] > limit)
                    {
                        totalRotation[i] = limit;
                    }
                    else if (totalRotation[i] < -limit)
                    {
                        totalRotation[i] = -limit;
                    }
                    transform.GetChild(i).rotation = quaternions[i] * Quaternion.Euler(0,0,totalRotation[i]);
                    
                }
                yield return null;
                windSpeed -= 10*Time.deltaTime;
            }
            windSpeed = -20;
            while (windSpeed < 20)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    totalRotation[i] += windSpeed * windMultiplier[i] * Time.deltaTime;
                    if (totalRotation[i] > limit)
                    {
                        totalRotation[i] = limit;
                    }
                    else if (totalRotation[i] < -limit)
                    {
                        totalRotation[i] = -limit;
                    }
                    transform.GetChild(i).rotation = quaternions[i] * Quaternion.Euler(0, 0, totalRotation[i]);
                }
                yield return null;
                windSpeed += 10 * Time.deltaTime;
            }
            windSpeed = 20;
        }

    }

    public int Blow()
    {

        int rand = Random.Range(0, transform.childCount);
        int choosen = -1;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild((i + rand) % transform.childCount).GetComponent<SpriteRenderer>().color.a > 0.5f)
            {
                choosen = (i + rand) % transform.childCount;
                break;
            }        
        }
        if (choosen == -1)
        {
            return -1;
        }


        StartCoroutine(StartGame(choosen));

        return choosen;

    }
    IEnumerator StartGame(int choosen)
    {
        Transform seed = transform.GetChild(choosen);
        Vector3 seedPos = seed.position;
        float delta = 0;
        while (delta < 1)
        {
            seed.position = seedPos + Vector3.up * 10 * delta;
            yield return null;
            delta += Time.deltaTime;
        }
        seed.position = seedPos + Vector3.up * 10;





    }



}
