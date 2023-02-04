using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //public FMODUnity.EventReference[] sound;
    //FMOD.Studio.EventInstance wind;
    //FMOD.Studio.EventInstance music;
    //FMOD.Studio.EventInstance move;


    public AudioClip[] clips;

    AudioSource wind0;
    AudioSource music1;
    AudioSource move1;
    AudioSource move2;






    void Awake()
    {

        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;

        wind0 = gameObject.AddComponent<AudioSource>();
        music1 = gameObject.AddComponent<AudioSource>();
        move1 = gameObject.AddComponent<AudioSource>();
        move2 = gameObject.AddComponent<AudioSource>();

        wind0.loop = true;
        music1.loop = true;
        move1.loop = true;
        move2.loop = true;
        

        wind0.clip = clips[0];
        music1.clip = clips[1];
        wind0.Play();
        music1.Play();

        //wind = FMODUnity.RuntimeManager.CreateInstance(sound[0]);
        //wind.start();

        //music = FMODUnity.RuntimeManager.CreateInstance(sound[1]);
        //music.start();
        
       


        DontDestroyOnLoad(this);
    }
    public void ChangeBGMtoDone()
    {
        music1.PlayOneShot(clips[2]);
        music1.PlayOneShot(clips[8]);
        music1.PlayOneShot(clips[9]);
        music1.PlayOneShot(clips[10]);
    //    int position;
    //    music.getTimelinePosition(out position);
    //    music = FMODUnity.RuntimeManager.CreateInstance(sound[2]);
    //    music.setTimelinePosition(position);
    //    music.start();
    //
    }
    public void ChangeBGMtoOriginal()
    {
        
        //int position;
        //music.getTimelinePosition(out position);
        //music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        //music = FMODUnity.RuntimeManager.CreateInstance(sound[1]);
        //music.setTimelinePosition(position);
        //music.start();
    }
    public void Arrive()
    {

        //FMODUnity.RuntimeManager.PlayOneShot(sound[3]);

        music1.PlayOneShot(clips[3]);

    }

    public void CantGet()
    {
        //FMODUnity.RuntimeManager.PlayOneShot(sound[4]);
        music1.PlayOneShot(clips[4]);
        music1.PlayOneShot(clips[11]);
    }

    public void Exhale()
    {
        //FMODUnity.RuntimeManager.PlayOneShot(sound[5]);
        music1.PlayOneShot(clips[5]);
    }
    public void StartGo()
    {
        //FMODUnity.RuntimeManager.PlayOneShot(sound[6]);
        music1.PlayOneShot(clips[6]);
    }
    public void Move()
    {
        //move = FMODUnity.RuntimeManager.CreateInstance(sound[7]);
        //move.setTimelinePosition(0);
        //move.start();
        move1.clip = clips[7];
        move2.clip = clips[12];
        move1.Play();
        move2.Play();
    }
    public void MoveStop()
    {
        //move.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        move1.Stop();
        move2.Stop();
    }


}
