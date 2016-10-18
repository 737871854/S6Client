/********************************************************************
    Copyright (c) 2015,XXX网络科技有限公司

    All rights reserved.

	文件名称：Game.cs
	简    述：比赛中摄像机控制类
	创建标识：Yeah 2015/12/30
*********************************************************************/
using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    #region Singleton
    protected static FollowTarget instance = null;
    public static FollowTarget Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("TestCamera").GetComponent<FollowTarget>();
                if (instance == null)
                {
                    instance = GameObject.Find("TestCamera").AddComponent<FollowTarget>();
                }
            }
            return instance;
        }
    }
    #endregion

    public Transform target = null;

    public Vector3 Adi = Vector3.zero;

    public float RotX = 0;
    public float RotY = 90;
    public float RotZ = 0;

    protected bool needFollow = false;

    void FixedUpdate()
    {
        if(needFollow)
        {
            if (target != null)
            {
                Vector3 myPo = target.position - Vector3.forward;
                float wantedHeight = target.position.y;
                myPo = new Vector3(myPo.x, wantedHeight, myPo.z) + Adi;
                transform.position = myPo;
                transform.LookAt(target.position + new Vector3(RotX, RotY, RotZ));
            }            
        }
    }    

    public void StartMoving()
    {
        SnailRun.Instance.Start();
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }
        needFollow = true;
    }

    public void EndMoving()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = true;
        }
    }

    public void ShowLast4()
    {
        GameObject p44 = UIBase.Instance.GetUIGameObjectUnderMainCanvas("Prepare4Seconds");
        if(p44 != null)
        {
            p44.SetActive(true);
        }
    }

    public void StopFollow()
    {
        needFollow = false;
    }
}
