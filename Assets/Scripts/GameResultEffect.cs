using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameResultEffect : MonoBehaviour {

    protected GameObject parent = null;
    protected GameObject img    = null;

    protected List<GameObject> goList;

	// Use this for initialization
	public void Start () {

        parent = transform.gameObject;
        img = transform.Find("ImgC_Fx").gameObject;
        goList = new List<GameObject>();
        StartCoroutine(OnCreateFX());
	}

    IEnumerator OnCreateFX()
    {
        while (true)
        {
            if (GameLogic.Instance.state == 0)
            {
                int count = Random.Range(1, 3);

                for (int index = 0; index < count; ++index)
                {
                    GameObject go = GameObject.Instantiate(img) as GameObject;
                    go.transform.SetParent(parent.transform);
                    Vector2 pos = new Vector2(Random.Range(-900, 900), Random.Range(-400, 400));
                    go.GetComponent<RectTransform>().anchoredPosition = pos;

                    go.SetActive(true);
                    goList.Add(go);
                }
            }
            yield return new WaitForSeconds(1.0f);
        }

    }
	
	// Update is called once per frame
    public void Update()
    {

        if (goList.Count > 0)
        {
            GameObject go = goList[0];
            Animator animator = go.GetComponent<Animator>();
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1.0f && info.IsName("Play"))
            {
                GameObject.Destroy(go);
                goList.RemoveAt(0);
            }
        }

	}
}
