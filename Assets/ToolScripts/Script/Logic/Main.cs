using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    void Awake()
    {
        GameObject go = null;
        Transform runnerTransform = null;

        for (int i = 1; i < 7; ++i)
        {
            runnerTransform = GameObject.Find("Runners/Runner" + i).transform;
            ////go = GameObject.Find("RunwayData/Runway" + i + "/Origin");
            ////go.transform.position = runnerTransform.position;
            go = GameObject.Find("RunwayData/Runway" + i + "/Destnation");
            ////go.transform.position = runnerTransform.position + runnerTransform.forward * 15;
            runnerTransform.LookAt(go.transform.position);
        }
    }    

    //// Use this for initialization
    //void Start()
    //{
    //    //SnailRun.Instance.Init();
    //}
    
    void FixedUpdate()
    {
        SnailRun.Instance.Update();
    }

    //void OnGUI()
    //{
    //    //if (GUI.Button(new Rect(530, 30, 100, 50), "开始"))
    //    //{
    //    //    GameLogic.Instance.StartGame();
    //    //}

    //    //GUI.Label(new Rect(30, 100, 100, 40), "赛道总长度:");
    //    //string runwayLength = GUI.TextField(new Rect(140, 100, 50, 30), Define.RUNWAY_LENGTH.ToString());
    //    //Define.RUNWAY_LENGTH = float.Parse(runwayLength);

    //    //GUI.Label(new Rect(30, 140, 100, 40), "最小赛段:");
    //    //string minrunwaySeg = GUI.TextField(new Rect(140, 140, 50, 30), Define.MIN_SEGMENT.ToString());
    //    //Define.MIN_SEGMENT = int.Parse(minrunwaySeg);

    //    //GUI.Label(new Rect(30, 180, 100, 40), "最大赛段:");
    //    //string maxrunwaySeg = GUI.TextField(new Rect(140, 180, 50, 30), Define.MAX_SEGMENT.ToString());
    //    //Define.MAX_SEGMENT = int.Parse(maxrunwaySeg);

    //    //string param = string.Empty;

    //    //GUI.Label(new Rect(30, 220, 100, 40), "速度变化下限:");
    //    //param = GUI.TextField(new Rect(160, 225, 50, 30), Define.MIN_SPEED_RATE.ToString("#0.00"));
    //    //Define.MIN_SPEED_RATE = float.Parse(param);

    //    //GUI.Label(new Rect(30, 260, 100, 40), "速度变化上限:");
    //    //param = GUI.TextField(new Rect(160, 265, 50, 30), Define.MAX_SPEED_RATE.ToString("#0.00"));
    //    //Define.MAX_SPEED_RATE = float.Parse(param);

    //    //GUI.Label(new Rect(30, 300, 100, 40), "变速时间下限:");
    //    //param = GUI.TextField(new Rect(160, 305, 50, 30), Define.MIN_ACCELERATION_RATE.ToString("#0.00"));
    //    //Define.MIN_ACCELERATION_RATE = float.Parse(param);

    //    //GUI.Label(new Rect(30, 340, 100, 40), "变速时间上限:");
    //    //param = GUI.TextField(new Rect(160, 345, 50, 30), Define.MAX_ACCELERATION_RATE.ToString("#0.00"));
    //    //Define.MAX_ACCELERATION_RATE = float.Parse(param);


    //    //GUI.Label(new Rect(30, 380, 200, 40), "第一名用时下限(秒):");
    //    //param = GUI.TextArea(new Rect(180, 385, 50, 30), Define.MIN_NO1_TIME.ToString("#0.00"));
    //    //Define.MIN_NO1_TIME = float.Parse(param);

    //    //GUI.Label(new Rect(30, 420, 200, 40), "第一名用时上限(秒):");
    //    //param = GUI.TextArea(new Rect(180, 425, 50, 30), Define.MAX_NO1_TIME.ToString("#0.00"));
    //    //Define.MAX_NO1_TIME = float.Parse(param);


    //    //GUI.Label(new Rect(30, 460, 200, 40), "非第一名用时差值下限(秒):");
    //    //param = GUI.TextField(new Rect(195, 465, 50, 30), Define.MIN_RUNNERS_TIME_DELTA.ToString("#0.00"));
    //    //Define.MIN_RUNNERS_TIME_DELTA = float.Parse(param);

    //    //GUI.Label(new Rect(30, 500, 200, 40), "非第一名用时差值上限(秒):");
    //    //param = GUI.TextField(new Rect(195, 505, 50, 30), Define.MAX_RUNNERS_TIME_DELTA.ToString("#0.00"));
    //    //Define.MAX_RUNNERS_TIME_DELTA = float.Parse(param);        
    //}
}
