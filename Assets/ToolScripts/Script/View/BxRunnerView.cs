using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BxRunnerView
{
    protected GameObject ViewGameobj = null;
    protected Transform transform = null;
    private AnimationPlayer animationPlayer;

    private TrailRenderer trailRender = null;

    public TrailRenderer MyTrailRender
    {
        get
        {
            return trailRender;
        }
    }


    public Transform MyTransform
    {
        get
        {
            return transform;
        }
    }

    protected bool inSlowDown = false;
    public bool InSlowDown
    {
        get
        {
            return inSlowDown;
        }
    }
    
    public BxRunnerView(int runway)
    {
        ViewGameobj = GameObject.Find("Runners/Runner" + runway);
        transform = ViewGameobj.transform;
        animationPlayer = transform.Find(runway.ToString()).gameObject.AddComponent<AnimationPlayer>();
        trailRender = ViewGameobj.GetComponent<TrailRenderer>();
    }

    public void Ready(Vector3 src, Vector3 dst)
    {
        transform.position = src;
        transform.LookAt(dst);
        animationPlayer.Play(AnimationID.ready);
    }  

    public void Start()
    {
        animationPlayer.Play(AnimationID.run);
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ShowRankImg(int rank)
    {
        Transform t = transform.Find("No.1");
        if (t != null)
        {
            if(rank == 1)
            {
                t.gameObject.SetActive(rank == 1);
                t.localPosition = new Vector3(0, 1.5f, 0.6f);
                t.localRotation = new Quaternion(0, -0.7148614f, 0, 0.6992662f);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
        }

        t = transform.Find("No.2");
        if (t != null)
        {
            if (rank == 2)
            {
                t.gameObject.SetActive(rank == 2);
                t.localPosition = new Vector3(0, 1.5f, 0.6f);
                t.localRotation = new Quaternion(0, -0.7148614f, 0, 0.6992662f);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    public void HideRankImg()
    {
        Transform t = transform.Find("No.1");
        if (t != null)
        {
            t.gameObject.SetActive(false);
        }

        t = transform.Find("No.2");
        if (t != null)
        {
            t.gameObject.SetActive(false);
        }
    }
}
