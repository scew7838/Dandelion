using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    public Vector3 origin = new Vector3(-4, -1, 6);
    public float offset;

    float time = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameManager.instance.RecalculateVecteor(origin).Item1;
        transform.localScale = GameManager.instance.RecalculateVecteor(origin).Item1;
        time = offset;
        Update();        
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 1f&&!GameManager.instance.gameEnd)
        {
            time -= 1;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time * 5f);

        }
        else if (time > 0.5f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1-3f*(time - 0.5f));
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time*5f);
        }

        Vector3 now = origin;
        now.z = (6 - GameManager.instance.curve.Evaluate(time) * 6);
        transform.position = GameManager.instance.RecalculateVecteor(now).Item1;
        transform.position += Vector3.forward * 0.1f;
        transform.localScale = Vector3.one * GameManager.instance.RecalculateVecteor(now).Item2;
        time += Time.deltaTime * GameManager.instance.roadAnim.speed / 3f;
    }
}
