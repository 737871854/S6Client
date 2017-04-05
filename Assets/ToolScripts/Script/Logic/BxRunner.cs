/********************************************************************
    Copyright (c) 2015,XXX网络科技有限公司

    All rights reserved.

	文件名称：BxRunner.cs
	简    述：比赛选手逻辑类
	创建标识：Yeah 2015/12/30
*********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BxRunner
{
    protected BxRunnerView view = null;
    protected Runway myRunway = null;
    /// <summary>
    /// 最终名次
    /// </summary>
    protected int myRank = 1;
    protected int myRunwayIndex = 0;

    //赛段
    protected int segmentCount = 0;
    protected float runTime = 0;

    protected List<Vector3> framsPositionList = null;
    protected List<float> frameLengthList = null;

    protected int runFrameCount = 0;
    protected int frameIndex = 0;

    /// <summary>
    /// 是否跑完，引来做回放控制的;
    /// </summary>
    protected bool myRaceOver = false;
    protected int waitFrames = 0;
    protected float endSpeed = 0;

    protected static GameObject cameraMovingGO = null;

    public static Transform CameraMovingTransform = null;

    public enum RunnerState
    {
        None = 0,
        Runing = 1,
        Gogogo = 2,
        AfterGoGoGo = 3,   
        WaitReplay = 4,     
        Replaying = 5,
        AfterReplay = 6,
    }
    
    public bool IsRunning
    {
        get;
        protected set;
    }

    public Transform ViewTransform
    {
        get
        {
            return view.MyTransform;
        }
    }

    public Transform RunwayDestion
    {
        get
        {
            return myRunway.DestGo;
        }
    }

    public Transform RunwayOrigin
    {
        get
        {
            return myRunway.OriginGO;
        }
    }

    protected RunnerState state = RunnerState.None;

    public BxRunner(int runway)
    {
        GameObject runwatData = GameObject.Find("RunwayData");
        Transform trans = runwatData.transform;

        view = new BxRunnerView(runway);
        myRunway = GameObject.Find("RunwayData/Runway" + runway).GetComponent<Runway>();
        myRunwayIndex = runway;

        if(cameraMovingGO == null)
        {
            cameraMovingGO = GameObject.Find("Runners/CameraMoving");
            CameraMovingTransform = cameraMovingGO.transform;
        }
    }
    
    public float Update()
    {
        if(IsRunning)
        {
            if(state == RunnerState.Runing)
            {
                if (frameIndex < runFrameCount)
                {
                    view.UpdatePosition(framsPositionList[frameIndex]);
                    return frameLengthList[frameIndex++];
                }
                else
                {
                    view.UpdatePosition(framsPositionList[frameIndex++]);
                    state = RunnerState.Gogogo;
                    SnailRun.Instance.Freezing();
                    view.ShowRankImg(myRank);
                    return Define.RUNWAY_LENGTH;
                }
            }
            else if(state == RunnerState.Gogogo)
            {
                waitFrames = 0;
                state = RunnerState.AfterGoGoGo;
                return Define.RUNWAY_LENGTH;
            }
            else if(state == RunnerState.AfterGoGoGo)
            {
                waitFrames++;
                if (waitFrames < 90)
                {
                    view.UpdatePosition(framsPositionList[runFrameCount + waitFrames]);
                }
                else
                {
                    SnailRun.Instance.OnRunnerRunOver(this, runFrameCount);
                    state = RunnerState.WaitReplay;
                }
            }
            else if(state == RunnerState.Replaying)
            {
                view.UpdatePosition(framsPositionList[frameIndex++]);

                if (frameIndex > runFrameCount+50)
                {
                    state = RunnerState.AfterReplay;
                }
            }
            else if(state == RunnerState.AfterReplay)
            {
                state = RunnerState.None;
                IsRunning = false;
                SnailRun.Instance.OnRunnerReplayOver();
            }
            return Define.RUNWAY_LENGTH;
        }
        return 0;
    }

    /// <summary>
    /// 设置最终名次，用名次和用时去随机做表现，返回比赛所用的帧数;
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="useTime"></param>
    /// <returns></returns>
    public int SetFinelRank(int rank, float useTime)
    {
        myRank = rank;
        RandomRunnerData(useTime);
        return runFrameCount;
    }

    /// <summary>
    /// 开始跑步
    /// </summary>
    public void Start()
    {
        state = RunnerState.Runing;
        IsRunning = true;
        frameIndex = 0;
        view.Start();
        view.MyTrailRender.time = 0.5f;
    }

    /// <summary>
    /// 准备;
    /// </summary>
    public void Ready()
    {
        view.Ready(myRunway.Origin, myRunway.Destion);
    }

    public void Replay(int rpFrameIndex)
    {
        state = RunnerState.Replaying;
        frameIndex = rpFrameIndex;
        view.MyTrailRender.time = 2f;
    }

    /// <summary>
    /// 随机用户数据，用来模拟表现的;
    /// </summary>
    protected void RandomRunnerData(float runTime)
    {
        segmentCount = RandomLib.Instance.GetRaceSegmentNum();
        List<float> segRate = RandomLib.Instance.GetSegmentRate(segmentCount);

        float averageSpeed = Define.RUNWAY_LENGTH / runTime;
        float averageSegLength = Define.RUNWAY_LENGTH / segmentCount;
        List<SegmentData> segLengthList = new List<SegmentData>();
        float lengthDelta = 0;
        float prevSpeed = 0;
        float timeDelta = 0;

        for (int i = 0; i < segmentCount; ++i)
        {
            SegmentData data = new SegmentData();
            data.RefSpeed = segRate[i] * averageSpeed;
            data.Length = segRate[i] * averageSegLength;
            data.StartPos = lengthDelta;
            data.EndPos = data.Length + lengthDelta;
            lengthDelta += data.Length;
            data.LerpPosition(prevSpeed, ref timeDelta);
            prevSpeed = data.NormalSpeed;
            segLengthList.Add(data);
        }

        //记住最后冲刺速度;
        endSpeed = prevSpeed;

        if (frameLengthList != null)
        {
            frameLengthList = null;
        }

        frameLengthList = new List<float>();
        for (int i = 0; i < segmentCount; ++i)
        {
            frameLengthList.AddRange(segLengthList[i].GetPosArray());
        }

        if (framsPositionList != null)
        {
            framsPositionList = null;
        }

        framsPositionList = new List<Vector3>();
        for (int i = 0; i < frameLengthList.Count; ++i)
        {
            Vector3 framePos = Vector3.Lerp(myRunway.Origin, myRunway.Destion, frameLengthList[i] / Define.RUNWAY_LENGTH);
            framsPositionList.Add(framePos);
        }
        runFrameCount = framsPositionList.Count;

        #region 正常跑完后;
        int aIndex = 1;
        Vector3 dir = (myRunway.Destion - myRunway.Origin).normalized;
        while (aIndex < 121)
        {
            Vector3 framePos = myRunway.Destion + dir * aIndex * endSpeed * Define.FRAME_TIME;
            framsPositionList.Add(framePos);
            aIndex++;
        }
        #endregion

        //释放;
        for (int i = 0; i < segmentCount; ++i)
        {
            segLengthList[i] = null;
        }
        segLengthList.Clear();
        segLengthList = null;
    }
}
