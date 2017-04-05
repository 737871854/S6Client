using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class GameAudio{

    static GameAudio m_instance;
    public static GameAudio Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameAudio();
            }
            return m_instance;
        }
    }

    protected Dictionary<AudioName, AudioSource> adDic = new Dictionary<AudioName, AudioSource>();
    public void InitSceneAudios()
    {
        adDic.Clear();
        adDic.Add(AudioName.AN_RaceBG, GameObject.Find("Audios/RaceBG").GetComponent<AudioSource>());
        adDic.Add(AudioName.AN_GoGoGo, GameObject.Find("Audios/GoGoGo").GetComponent<AudioSource>());
        adDic.Add(AudioName.AN_Replay, GameObject.Find("Audios/Replay").GetComponent<AudioSource>());
        adDic.Add(AudioName.AN_Win, GameObject.Find("Audios/Win").GetComponent<AudioSource>());
        adDic.Add(AudioName.AN_RaceLastFourSecond, GameObject.Find("Audios/RaceLastFourSecond").GetComponent<AudioSource>());
        adDic.Add(AudioName.AN_RaceAmbient, GameObject.Find("Audios/RaceAmbient").GetComponent<AudioSource>());
        adDic.Add(AudioName.AN_RaceGoGoGo, GameObject.Find("Audios/RaceGoGoGo").GetComponent<AudioSource>());
        
    }

    public void InitBetAudios()
    {
        adDic.Clear();
        adDic.Add(AudioName.AN_RaceBG,GameObject.Find("Audios/RaceBG").GetComponent<AudioSource>());
        adDic.Add(AudioName.AN_NumberSelected, GameObject.Find("Audios/NumberSelected").GetComponent<AudioSource>());
        adDic.Add(AudioName.AN_BetError, GameObject.Find("Audios/BetError").GetComponent<AudioSource>());
        adDic.Add(AudioName.AN_BetSuccess, GameObject.Find("Audios/BetSuccess").GetComponent<AudioSource>());
        adDic.Add(AudioName.AN_BetLastThirtySecond, GameObject.Find("Audios/BetLastThirtySecond").GetComponent<AudioSource>());
    }


    public void PlayAudio(AudioName an)
    {
        if (adDic.ContainsKey(an))
        {
            adDic[an].Play();
        }
    }

    public void PlayAudio(int id)
    {
        AudioName an = (AudioName)id;
        PlayAudio(an);
    }

    public void Pause(int id)
    {
        AudioName an = (AudioName)id;
        Pause(an);
    }

    public void Pause(AudioName an)
    {
        if (adDic.ContainsKey(an))
        {
            adDic[an].Pause();
        }
    }

    public void UnPause(int id)
    {
        AudioName an = (AudioName)id;
        UnPause(an);
    }

    public void UnPause(AudioName an)
    {
        if (adDic.ContainsKey(an))
        {
            adDic[an].UnPause();
        }
    }
}
