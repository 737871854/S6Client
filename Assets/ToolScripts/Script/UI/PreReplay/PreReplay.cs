using UnityEngine;
using System.Collections;

public class PreReplay
{
    #region Singleton
    static PreReplay m_instance;

    public static PreReplay Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new PreReplay();
            }
            return m_instance;
        }
    }
    #endregion

    protected GameObject img = null;

    protected bool isWaiting = false;
    protected float timeDelta = 0;
    protected float waitTime = 2;
    public void Start()
    {
        isWaiting = true;
        timeDelta = 0;
        img = UIBase.Instance.GetUIGameObjectUnderMainCanvas("PreReplay");
        if(img != null)
        {
            GameAudio.Instance.PlayAudio(AudioName.AN_GoGoGo);
            GameAudio.Instance.Pause(AudioName.AN_RaceAmbient);
            img.SetActive(true);
        }
    }

    public void Update()
    {
        if(isWaiting)
        {
            timeDelta += Time.deltaTime;
            if(timeDelta >= waitTime)
            {
                isWaiting = false;
                SnailRun.Instance.StartReplay();
                if(img != null)
                {
                    img.SetActive(false);
                }
            }
        }
    }
}
