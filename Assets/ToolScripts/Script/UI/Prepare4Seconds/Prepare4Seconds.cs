using UnityEngine;
using System.Collections;

public class Prepare4Seconds : MonoBehaviour
{
    public GameObject[] Imgs;

    protected float timeDelta = 0;    

    // Update is called once per frame
    void Update()
    {
        timeDelta += Time.deltaTime;
        if(timeDelta > 5)
        {
            gameObject.SetActive(false);
            GameAudio.Instance.Pause(AudioName.AN_RaceLastFourSecond);
            GameAudio.Instance.PlayAudio(AudioName.AN_RaceAmbient);
        }
        else
        {
            Imgs[0].SetActive(timeDelta < 1);
            Imgs[1].SetActive(timeDelta >=1 && timeDelta < 2);
            Imgs[2].SetActive(timeDelta >= 2 && timeDelta < 3);
            Imgs[3].SetActive(timeDelta >= 3 && timeDelta < 4);
            Imgs[4].SetActive(timeDelta >= 4 && timeDelta < 5);
        }
    }

    void OnEnable()
    {
        timeDelta = 0;
        GameAudio.Instance.PlayAudio(AudioName.AN_RaceLastFourSecond);
        Log.print(1);
    }
}
