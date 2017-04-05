/********************************************************************
    Copyright (c) 2015,XXX网络科技有限公司

    All rights reserved.

	文件名称：RandomLib.cs
	简    述：比赛过程表现数据随机库。
	创建标识：Yeah 2015/12/30
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class RandomLib
{
    #region singleton
    protected static RandomLib instance = null;
    public static RandomLib Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RandomLib();
            }
            return instance;
        }
    }

    protected RandomLib()
    {

    }
    #endregion

    
    /// <summary>
    /// 获取随机用时
    /// </summary>
    /// <returns></returns>
    public List<float> GetRaceTimes()
    {
        float ret1 = Random.Range(Define.MIN_NO1_TIME, Define.MAX_NO1_TIME);

        float r1 = GetRaceTimeRate();
        float r2 = GetRaceTimeRate();
        float ret2 = Random.Range(ret1 + r1, ret1 + r1 + r2);

        r1 = GetRaceTimeRate();
        r2 = GetRaceTimeRate();
        float ret3 = Random.Range(ret2 + r1, ret2 + r1 + r2);

        r1 = GetRaceTimeRate();
        r2 = GetRaceTimeRate();
        float ret4 = Random.Range(ret3 + r1, ret3 + r1 + r2);

        r1 = GetRaceTimeRate();
        r2 = GetRaceTimeRate();
        float ret5 = Random.Range(ret4 + r1, ret4 + r1 + r2);

        r1 = GetRaceTimeRate();
        r2 = GetRaceTimeRate();
        float ret6 = Random.Range(ret5 + r1, ret5 + r1 + r2);

        List<float> retList = new List<float>();
        retList.AddRange(new float[] { ret1, ret2, ret3, ret4, ret5, ret6 });

        //if(Application.platform == RuntimePlatform.WindowsEditor)
        //{
        //    Debug.Log(string.Format("{0},{1},{2},{3},{4},{5}", ret1, ret2, ret3, ret4, ret5, ret6));
        //}

        return retList;
    }

    /// <summary>
    /// 获取每个赛段变化率
    /// </summary>
    /// <param name="segNun"></param>
    /// <returns></returns>
    public List<float> GetSegmentRate(int segNum)
    {
        float last = segNum;
        float delta = 0;
        float ret = 0;
        List<float> retList = new List<float>();
        for (int i = 0; i < segNum - 1; ++i)
        {
            ret = GetSpeedRateRange();
            delta += ret;
            retList.Add(ret);
        }

        ret = (segNum - delta);
        retList.Add(ret);
        return retList;
    }

    public int GetRaceSegmentNum()
    {
        return Random.Range(Define.MIN_SEGMENT, Define.MAX_SEGMENT);
    }

    /// <summary>
    /// 获取加速时间范围;
    /// </summary>
    /// <returns></returns>
    public float GetAccelerationRate()
    {
        return Random.Range(Define.MIN_ACCELERATION_RATE, Define.MAX_ACCELERATION_RATE);
    }

    protected float GetRaceTimeRate()
    {
        return Random.Range(Define.MIN_RUNNERS_TIME_DELTA, Define.MAX_RUNNERS_TIME_DELTA);
    }   

    protected float GetSpeedRateRange()
    {
        return Random.Range(Define.MIN_SPEED_RATE, Define.MAX_SPEED_RATE);
    }
}
