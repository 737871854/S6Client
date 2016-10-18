/**
 * Copyright (c) 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：AudioManager.cs
 * 简    述：利用Fmod Stdio播放音效。
 * 创建标识：Lorry 2015/12/24
 * 修改标识：陈宜徐---要求在播放声音的时候，可以同时播放多个相同音效。
 * 通过Fmod管理音频文件，利用Fmod的事件创建，加载，播放和控制音频的播放
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    #region 私有变量
    private UnityEngine.Object _audioPrefab = null;//音频预置对象;
    private Dictionary<string, AudioObject> dicAudioItem = new Dictionary<string, AudioObject>();
    private List<string> m_audioList = new List<string>();
    private GameObject _audioParent;
    #endregion

    #region MonoBehaviour内部函数
    void Awake()
    {//设置默认为立体声(要求3D音效); Lorry         //AudioSettings.speakerMode = AudioSpeakerMode.Stereo;
       /* AudioConfiguration ac = AudioSettings.GetConfiguration();
        ac.speakerMode = AudioSpeakerMode.Stereo;
        AudioSettings.Reset(ac);*/
        _audioParent = new GameObject("AudioParent");
        DontDestroyOnLoad(_audioParent);
    }
    #endregion

    #region 公共方法
    public void Init()
    {
        //LoadAudioClip("");
    }
    /// <summary>
    /// 在指定的位置播放声音
    /// </summary>
    public string PlayOnPoint(string audioName, Vector3 point)
    {
        return this.PlayAudio(audioName, point);
    }

    /// <summary>
    /// 播放音频;返回值暂时没有什么用。
    /// </summary>
    /// <param name="go">音频挂载的父对象</param>
    public string Play(string audioName, GameObject go)
    {
        /*if (CheckAudioList(audioName))
            return string.Empty;*/
        string audioGuid = string.Empty;
        try
        {
            AudioObject audioObject = CreateAudioSource(audioName);
            //dic_dicAudioObject[audioType].Add(audioObject.Guid, audioObject);
            if (go != null)
            {
                audioObject.transform.parent = go.transform;
                audioObject.transform.localPosition = Vector3.zero;
                audioObject.Play();
                audioObject.Playing3DSoundEffect();
                Debug.Log("Play(string audioName, GameObject go1");
            }
        }
        catch (Exception ex)
        {
            Debug.Log("音乐播放出错:" + ex.ToString());
        }
        return audioGuid;
    }

    /// <summary>
    /// 用于UI播放声音的接口
    /// </summary>
    /// <param name="audioName">声音的完整路径名称，包括扩展名</param>
    /// <param name="forcePlay">如果为true，就不会进行重复播放的剔除，一定会将此声音播放出来。</param>
    public void Play2D(string audioName, bool forcePlay = true)
    {
        if (forcePlay)
            m_audioList.Remove(audioName);
        Play(audioName, _audioParent);
    }


    /// <summary>
    /// 请注意，因为AudioObject在update时一旦停止就会被销毁，所以这个对象停止播放会被设置为null……
    /// </summary>
    public AudioObject m_BGM = null;
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayBGM(string audioName, bool loop)
    {
        AudioObject temp = LoadAudioClip(audioName);
        if (m_BGM == null)
        {
            m_BGM = CreateAudioSource(audioName);
            m_BGM.transform.parent = _audioParent.transform;
            m_BGM.transform.localPosition = Vector3.zero;
        }
        StopBGM();
        m_BGM.AudioItem = temp;
        m_BGM.SetLoop(loop);
        m_BGM.Play(loop);
        Debug.Log("Play(string audioName, GameObject go3");
    }

    public void StopBGM()
    {
        if (m_BGM != null)
        {
            m_BGM.Stop();
        }
    }

    /// <summary>
    /// 切换场景时，停止一切的声音(音效背景音乐等)
    /// </summary>
    public void StopAll()
    {
        if (_audioParent != null)
        {
            for (int i = 0; i < _audioParent.transform.childCount; i++)
            {
                GameObject go = _audioParent.transform.GetChild(i).gameObject;
                AudioObject audioObject = go.GetComponent<AudioObject>();
                audioObject.Stop();
                Destroy(go);
            }
            dicAudioItem.Clear();
        }
    }



    public void ClearOnInitScene()
    {
        //dicAudioItem.Clear();
    }


    /// <summary>
    /// 这个方法本来设计的是公共接口，现在 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private AudioObject LoadAudioClip(string path)
    {
        AudioObject ac = null;
        //if (dicAudioItem.ContainsKey(path))
        //{
        //    ac = dicAudioItem[path];
        //    Debug.Log("ac = ");
        //}
        //else
        //{
        //    Debug.Log("ac = FMOD_StudioSystem");
        //    ac = FMOD_StudioSystem.instance.GetEvent(path);
        //}

        //if (ac == null)
        //{//这主要是考虑到即使dicAudioItem中有AudioClip对象，也可能已经被清空为null
        //    ac = FMOD_StudioSystem.instance.GetEvent(path);
        //}
        return ac;
    }

    #endregion

    #region 私有方法
    private string PlayAudio(string audioName, Vector3 point)
    {
        if (CheckAudioList(audioName))
            return string.Empty;
        string audioGuid = string.Empty;
        try
        {
            //if (JudgeAudioInDistance(audioName, gameObject))
            //{//声音在小区域范围内太多时，没有必要全部播放;
            //    GameObject.Destroy(gameObject);
            //    return string.Empty;
            //}
            AudioObject audioObject = CreateAudioSource(audioName);
            audioObject.transform.localPosition = point;
            audioObject.transform.parent = _audioParent.transform;
            audioObject.Play();
            Debug.Log("Play(string audioName, GameObject go2");
            //audioGuid = audioName;
        }
        catch (Exception ex)
        {
            Debug.Log("音乐播放出错:" + ex.ToString());
        }
        return audioGuid;
    }

    //每一个声音都需要一个AudioSource来进行播放。
    private AudioObject CreateAudioSource(string audioName)
    {
        UnityEngine.Object obj = Resources.Load("AudioObject");
        GameObject gameObject = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
        //GameObject gameObject = GameObject.Instantiate(_audioPrefab) as GameObject;
        AudioObject audioObject = gameObject.AddComponent<AudioObject>();
        audioObject.AudioItem = LoadAudioClip(audioName);
        audioObject.CompletelyPlayedDelegate = AudioPlayComplete;
        audioObject.Guid = System.Guid.NewGuid().ToString();
        audioObject.gameObject.name = audioName;//audioObject.Guid;

        m_audioList.Add(audioName);
        return audioObject;
    }

    //音频播放结束后触发;
    private void AudioPlayComplete(AudioObject audioObject)
    {
        //将AudioObject 从dic_dicAudioObject中移除;
        //if (dic_dicAudioObject.ContainsKey(audioObject.AudioType))
        //{
        //    Dictionary<string, AudioObject> dicAudioObject = dic_dicAudioObject[audioObject.AudioType];
        //    if (dicAudioObject.ContainsKey(audioObject.Guid))
        //    {
        //        if (dicAudioObject.Count == 1)
        //            dic_dicAudioObject.Remove(audioObject.AudioType);
        //        else
        //            dicAudioObject.Remove(audioObject.Guid);
        //    }
        //}
        m_audioList.Remove(audioObject.gameObject.name);
    }

    private bool CheckAudioList(string audioName)
    {
        if (m_audioList.Contains(audioName))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Test
    public bool isDebug = false;
    public string m_AudioName = "Assets/Music/ogg/tc-tf0001.ogg";
    //音乐名称：Assets/Music/ogg/yx-ty0001.ogg

    public Vector3 m_Pos = Vector3.one;
    void OnGUI()
    {
        //if (isDebug == false)
        //    return;
        //#region 测试播放音效;
        //m_AudioName = GUI.TextField(new Rect(90, 100, 100, 22), m_AudioName);
        //if (GUI.Button(new Rect(15, 100, 70, 22), "Play"))
        //{
        //    if (m_AudioName == "")
        //        return;

        //    PlayBGM(m_AudioName, true);
        //    //PlayOnPoint(m_AudioName, m_Pos);
        //}
        //#endregion

        //if (GUI.Button(new Rect(195, 100, 60, 22), "Stop"))
        //{
        //    StopBGM();
        //}

        //if (GUI.Button(new Rect(260, 100, 50, 22), "StopAll"))
        //{
        //    StopAll();
        //}

        //if (GUI.Button(new Rect(430, 100, 75, 22), "test2"))
        //{
        //    //dicAudioItem.Remove(m_AudioName);

        //    //if (dicAudioItem[m_AudioName] == null)
        //    //    Debug.Log(m_AudioName + " is NULL!");

        //    //dicAudioItem.Add(m_AudioName, null);
        //    //ResourceMisc.AssetWrapper aw = ioo.resourceManager.LoadRes(m_AudioName, typeof(AudioClip));
        //    //AudioClip ac = (AudioClip)aw.GetAsset();//Resources.Load(path, typeof(AudioClip));
        //    //dicAudioItem.Add(m_AudioName, ac);
        //}
    }
    #endregion
}
