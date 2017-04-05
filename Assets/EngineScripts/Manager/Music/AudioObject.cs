/**
 * Copyright (c) 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：AudioObject.cs
 * 简    述：主要作用是在声音播放完成之后，销毁声音。
 * 创建标识：Lorry 2015/7/2
 * 
 */

using UnityEngine;
using System.Collections;


/// <summary>
/// 主要作用是在AudioClip播放完成之后销毁它
/// </summary>
public class AudioObject : MonoBehaviour {

    /// <summary>
    /// Audio event delegate type.
    /// </summary>
    public delegate void AudioEventDelegate(AudioObject audioObject);

    #region 属性
    /// <summary>
    /// 唯一id
    /// </summary>
    public string Guid
    {
        get;
        set;
    }

    private AudioEventDelegate _completelyPlayedDelegate;
    /// <summary>
    /// 播放完成委托;
    /// </summary>
    public AudioEventDelegate CompletelyPlayedDelegate
    {
        set
        {
            _completelyPlayedDelegate = value;
        }
        get
        {
            return _completelyPlayedDelegate;
        }
    }

    //声音效资的引用
    public AudioObject AudioItem;

    #endregion

    #region 私有变量

    private bool IsOver = false;
    private bool _loop = false; 
    //private AudioSource _audioSource;

    #endregion //私有变量
 
    #region MonoBehaviour内部函数
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (AudioItem == null)
        {
            Destroy(gameObject);
            return;
        }
        if (AudioItem!=null && IsPlaying() == false)
        {//将来如果要实现中断和暂停，可能要改代码Lorry
                Stop();
                Destroy(gameObject);
                return;
        }
    }

    void OnDestroy()
    {
        AudioItem = null;
        //此处代码保证结束回调不会被调用两次
        if (this._completelyPlayedDelegate != null)
            _completelyPlayedDelegate(this);
        this._completelyPlayedDelegate = null;
        //此方法PC环境会消耗13ms 暂时不使用;
        //Resources.UnloadAsset(_audioItem.AudioClip);
    }
    #endregion //MonoBehaviour内部函数

    #region 公有方法
    public void Play(bool loop = false)
    {
        //if (AudioItem != null)
        //{
        //    AudioItem.start();
        //}
    }

    public void Stop()
    {
        //if (AudioItem != null)
        //{
        //    AudioItem.stop(STOP_MODE.ALLOWFADEOUT);
        //    AudioItem.release();
        //}
    }


    public void SetLoop(bool loop)
    {
        _loop = loop;
    }

    public bool IsPlaying()
    {
    //    PLAYBACK_STATE state;
    //    AudioItem.getPlaybackState(out state);
    //    if (PLAYBACK_STATE.STOPPED == state)
    //    {
    //        if (_loop)
    //        {
    //            Play();
    //        }
    //        return _loop;
    //    }
        return true;
    }

    public void Playing3DSoundEffect()
    {
        //StartCoroutine(Update3DSoundEffect());
    }

    //IEnumerator Update3DSoundEffect()
    //{
    //    while (true)
    //    {
    //        if (AudioItem != null)
    //        {
    //            if (IsPlaying())
    //            {
    //                AudioItem.set3DAttributes(FModUtils.to3DAttributes(gameObject.transform));
    //            }
    //        }
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}
    #endregion //公有方法
    
    #region 私有方法
    ///// <summary>
    ///// 设置音源;这个方法已经合并到play里面去了。
    ///// </summary>
    //private bool SetAudioClip()
    //{
    //}
    #endregion //私有方法
}
