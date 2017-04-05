/********************************************************************
    Copyright (c) 2015,XXX网络科技有限公司

    All rights reserved.

	文件名称：SnailRun.cs
	简    述：比赛控制类
	创建标识：Yeah 2015/12/30
*********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class RealTimeRank
{
    public float S;
    public int Id;

    /// <summary>
    /// 设置数据;
    /// </summary>
    /// <param name="s"></param>
    /// <param name="id"></param>
    public void SetData(int id, float s)
    {
        Id = id;
        S = s;
    }
}

public class RankSort : IComparer
{
    //小于零 此实例按排序顺序在 obj 前面。
    //零 此实例与 obj 在排序顺序中出现的位置相同。
    //大于零 此实例按排序顺序在 obj 后面。
    public int Compare(object lhs, object rhs)
    {
        int ret = 0;

        RealTimeRank left = (RealTimeRank)lhs;
        RealTimeRank right = (RealTimeRank)rhs;

        if (FloatLess(left.S, right.S))
        {
            ret = 1;
        }
        else
        {
            ret = -1;
        }
        return ret;
    }

    /// <summary>
    /// 浮点数的比较
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    protected bool FloatLess(float lhs, float rhs)
    {
        if (lhs - rhs < 0.000001f)
        {
            return true;
        }
        return false;
    }
}



public class SnailRun
{
    #region singleton
    protected static SnailRun instance = null;
    public static SnailRun Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new SnailRun();
            }
            return instance;
        }
    }

    protected SnailRun()
    {
        
    }
    #endregion

    protected List<BxRunner> runners = new List<BxRunner>();

    public bool IsRunning
    {
        get;
        protected set;
    }

    //protected FollowTarget followTarget = null;

    protected int winnerFrames = 10000000;
    protected int runOverNums = 0;
    protected int replayOverNum = 0;


    protected float minTime = 100000;
    protected BxRunner winner = null;

    protected bool canUpdate = false;
    protected int frameDelta = 0;

    protected int freezingCount = 0;
    protected bool inFreezing = false;

    protected bool needUpdateRealTimeRank = false;

    protected List<RealTimeRank> realTimeRankList = new List<RealTimeRank>();

    public void Init()
    {
        #region 重新初始化;
        //因为这个类是单件，在重新加载场景后，对象销毁了，但是相关的对象引用还在，因此要清除，重新初始化一次;
        if (runners != null)
        {
            for (int i = 0; i < runners.Count; ++i)
            {
                runners[i] = null;
            }
            runners.Clear();
            runners = null;
        }
        runners = new List<BxRunner>();
        
        if(realTimeRankList != null)
        {
            for (int i = 0; i < realTimeRankList.Count; ++i)
            {
                realTimeRankList[i] = null;
            }
            realTimeRankList.Clear();
            realTimeRankList = null;
        }
        realTimeRankList = new List<RealTimeRank>();
        UIBase.Instance.ReInit();
        #endregion

        for (int i = 0; i < 6; ++i)
        {
            BxRunner obj = new BxRunner(i + 1);
            runners.Add(obj);
            realTimeRankList.Add(new RealTimeRank());
        }
        GameAudio.Instance.InitSceneAudios();
    }

    public void SetGameData(List<int> rank, bool isWin = false)
    {
        //Debug.Log(string.Format("FinalRank:{0} {1} {2} {3} {4} {5}", rank[0], rank[1], rank[2], rank[3], rank[4], rank[5]));

        Time.fixedDeltaTime = Define.FRAME_TIME;

        List<float> costTimes = RandomLib.Instance.GetRaceTimes();
        //Debug.Log(string.Format("Times:{0} {1} {2} {3} {4} {5}", costTimes[0], costTimes[1], costTimes[2], costTimes[3], costTimes[4], costTimes[5]));
        for (int i = 0; i < rank.Count; ++i)
        {
            runners[i].RunwayDestion.localPosition = new Vector3(0, 0, Define.RUNWAY_LENGTH);
            runners[i].SetFinelRank(rank[i], costTimes[rank[i]-1]);
            if(minTime > costTimes[i])
            {
                winner = runners[i];
                minTime = costTimes[i];
            }
        }
        RealTimeRankLogic.Instance.ReInit();
    }

    public void Ready()
    {
        for (int i = 0; i < runners.Count; ++i)
        {
            runners[i].Ready();
        }
    }
    
    public void Start()
    {
        RealTimeRankLogic.Instance.Show(true);
        for (int i = 0; i < runners.Count; ++i)
        {
            runners[i].Start();
        }
        IsRunning = true;
        runOverNums = 0;
        replayOverNum = 0;
        freezingCount = 0;
        inFreezing = false;
        needUpdateRealTimeRank = true;        
    }

    /// <summary>
    /// 随时停止;
    /// </summary>
    public void Stop()
    {
        RealTimeRankLogic.Instance.Show(false);
        IsRunning = false;
    }

    /// <summary>
    /// 正常结束;
    /// </summary>
    public void End()
    {        
        IsRunning = false;
        GameLogic.Instance.EndGame();
    }

    public void Update()
    {
        if(IsRunning)
        {
            if (inFreezing)
            {
                inFreezing = ((frameDelta++) < 90);
            }
            else
            {
                frameDelta = 0;
                for (int i = 0; i < runners.Count; ++i)
                {
                    realTimeRankList[i].SetData(i + 1, runners[i].Update());
                }
                realTimeRankList.Sort(new RankSort().Compare);
                UpdateRealTimeRank();
            }
            PreReplay.Instance.Update();
        }
    }
    
    protected void Replay()
    {
        GameAudio.Instance.PlayAudio(AudioName.AN_Replay);
        for (int i = 0; i < runners.Count; ++i)
        {
            runners[i].Replay(winnerFrames-30);
        }

        Time.fixedDeltaTime = Define.FRAME_TIME * Define.REPLAY_RATE;
    }

    public void OnRunnerRunOver(BxRunner runner, int usedFrameCount)
    {
        runOverNums++;
        if(usedFrameCount < winnerFrames)
        {
            winnerFrames = usedFrameCount;
        }

        if(runOverNums == 6)
        {
            RealTimeRankLogic.Instance.Show(false);
            StartWait();
        }

        if (runOverNums == 2)
        {
            GameAudio.Instance.PlayAudio(AudioName.AN_Win);
        }
    }

    public void OnRunnerReplayOver()
    {
        replayOverNum++;
        if (replayOverNum == 6)
        {
            Time.fixedDeltaTime = Define.FRAME_TIME;
            End();
            GameAudio.Instance.PlayAudio(AudioName.AN_Win);
        }
    }

    public void Freezing()
    {
        needUpdateRealTimeRank = false;
        if (freezingCount++ < 2)
        {
            FollowTarget.Instance.StopFollow();
            GameAudio.Instance.PlayAudio(AudioName.AN_RaceGoGoGo);
            inFreezing = true;
        }
    }

    /// <summary>
    /// 更新实时排名;
    /// </summary>
    protected void UpdateRealTimeRank()
    {
        if(needUpdateRealTimeRank)
        {
            RealTimeRankLogic.Instance.Update(realTimeRankList);
        }

        BxRunner.CameraMovingTransform.localPosition = new Vector3(0, 0, runners[realTimeRankList[0].Id - 1].ViewTransform.localPosition.z);
    }

    public void StartWait()
    {
        PreReplay.Instance.Start();
    }

    public void StartReplay()
    {
        Replay();
    }
}
