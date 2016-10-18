/********************************************************************
    Copyright (c) 2015,XXX网络科技有限公司

    All rights reserved.

	文件名称：Game.cs
	简    述：游戏基本类型定义
	创建标识：Yeah 2016/01/01
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    /// <summary>
    /// 最小赛段
    /// </summary>
    public static int MIN_SEGMENT = 5;
    /// <summary>
    /// 最大赛段
    /// </summary>
    public static int MAX_SEGMENT = 9;

    /// <summary>
    /// 最小速度比例
    /// </summary>
    public static float MIN_SPEED_RATE = 0.95f;
    /// <summary>
    /// 最大速度比例
    /// </summary>
    public static float MAX_SPEED_RATE = 1.05f;

    /// <summary>
    /// 赛道长度
    /// </summary>
    public static float RUNWAY_LENGTH = 500;

    /// <summary>
    /// 每帧时间;
    /// </summary>
    public static float FRAME_TIME = 0.0166666f;

    public static float MIN_ACCELERATION_RATE = 0.05f;
    public static float MAX_ACCELERATION_RATE = 0.10f;

    public static float MIN_NO1_TIME = 19.0f;
    public static float MAX_NO1_TIME = 21.0f;

    public static float MIN_RUNNERS_TIME_DELTA = 0.03f;
    public static float MAX_RUNNERS_TIME_DELTA = 0.07f;

    public static float REPLAY_RATE = 4;

    public static float SLOWDOWN_TIME = 1.2f;
}

public class SegmentData
{
    public float Length;
    public float StartPos;
    public float EndPos;
    /// <summary>
    /// 参考速度用来计算时间的;
    /// </summary>
    public float RefSpeed;

    /// <summary>
    /// 加速或加速后正常行驶的速度;
    /// </summary>
    public float NormalSpeed
    {
        protected set;
        get;
    }


    protected List<float> posList = new List<float>();

    /// <summary>
    /// 先变速，后匀速;
    /// </summary>
    /// <param name="prevSpeed"></param>
    public void LerpPosition(float prevSpeed, ref float lastPos)
    {
        float pos = lastPos;
        float costTime = Length / RefSpeed;

        //随机获取加速时间;
        float accelerationRate = RandomLib.Instance.GetAccelerationRate();

        float accerationTime = costTime * accelerationRate;
        float normalSpeedTime = costTime - accerationTime;

        //计算加速度(用运动公式计算的)
        // 通过 V = V0 + A*T0
        // S = V0*T0 + (1/2)*A*T0*T0 + V*T2
        // 其中A是加速度，V是匀速运动的速率，V0是起始速率，也就是参数prevSpeed
        // S是位移，也就是Length，T0是加速时间，T1是匀速运动时间.
        // 通过上面两个公式 得出 加速度 A = (2*S - 2V0*T1 - 2*V0*T2)/(T1*T1 + 2*T1*T2)
        // 这里不用考虑是加速还是减速，通过prevSpeed可以得出需要加速还是减速;

        float acceleration = 2 * (Length - prevSpeed * accerationTime - prevSpeed * normalSpeedTime) / (Mathf.Pow(accerationTime, 2) + 2 * accerationTime * normalSpeedTime);

        //计算加速后的均匀速速;
        NormalSpeed = prevSpeed + acceleration * accerationTime;

        float theTime = 0;
        while (theTime < accerationTime)
        {
            pos += (prevSpeed + (acceleration * theTime)) * Define.FRAME_TIME;
            posList.Add(pos);
            theTime += Define.FRAME_TIME;
        }

        while (theTime < costTime)
        {
            pos += NormalSpeed * Define.FRAME_TIME;
            posList.Add(pos);
            theTime += Define.FRAME_TIME;
        }

        lastPos = pos;
    }

    public float[] GetPosArray()
    {
        List<float> retList = new List<float>();
        for (int i = 0; i < posList.Count; ++i)
        {
            retList.Add(posList[i]);
        }

        return retList.ToArray();
    }

}

public enum AudioName
{
    AN_RaceBG = 0,
    AN_GoGoGo = 1,
    AN_Replay = 2,
    AN_Win = 3,
    AN_NumberSelected = 4,
    AN_BetError = 5,
    AN_BetSuccess = 6,
    AN_BetLastThirtySecond = 7,
    AN_RaceLastFourSecond = 8,
    AN_RaceAmbient = 9,
    AN_RaceGoGoGo = 10,
}
