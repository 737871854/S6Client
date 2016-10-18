/**
 * Copyright (c) 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：GameLogic.cs
 * 简    述：在Scripts路径中编写的c#代码是和游戏逻辑密不可分的。
 * 无法抽出来作为引擎或者工具代码，专门针对本游戏的相关代码.
 * 本文件用于lua启动蜗牛赛跑游戏，以及
 * 创建标识：lxt 2015/12/24
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic {
    static GameLogic m_instance;

    public static GameLogic Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameLogic();
            }
            return m_instance;
        }
    }

    public int state = 1;

    //用于启动游戏，调用蜗牛赛跑开始的函数
    public void StartGame()
	{
        //Debug.LogWarning("开始倒计时停止下注！！");
        //Debug.LogWarning("倒计时结束蜗牛开始赛跑了！！");

        state = 1;

        Animator animator = GameObject.Find("TestCamera").GetComponent<Animator>();
        SnailRun.Instance.Stop();
        animator.Play("None");
        FollowTarget.Instance.EndMoving();

        //随机比赛结果;
        List<int> ranks = new List<int>(new int[] { 1, 2, 3, 4, 5, 6 });
        Util.RandomSortList(ref ranks);
        SnailRun.Instance.Init();
        SnailRun.Instance.SetGameData(ranks);
        SnailRun.Instance.Ready();

        animator.Play("Animation");
        
    }

    public void ReadyToGo()
    {
        //Debug.LogWarning("开始倒计时停止下注！！");
    }

    public void EndGame()
    {
        state = 0;
        ioo.gameManager.uluaMgr.CallLuaFunction("GameManager.XXXCall");
    }
}
