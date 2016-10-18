using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 实时排名逻辑类
/// </summary>
public class RealTimeRankLogic
{
    #region Singleton
    protected static RealTimeRankLogic instance = null;
    protected RealTimeRankLogic()
    {

    }

    public static RealTimeRankLogic Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new RealTimeRankLogic();
            }
            return instance;
        }
    }
    #endregion

    protected List<Text> rankTextList = null;
    protected GameObject root = null;

    /// <summary>
    /// 初始化
    /// </summary>
    public void ReInit()
    {
        root = UIBase.Instance.GetUIGameObjectUnderMainCanvas("RealTimeRank");
        if (root == null)
        {
            Debug.LogError("UI Error: Not found UI with name RealTimeRank!");
            return;
        }

        if (rankTextList != null)
        {
            rankTextList.Clear();
            rankTextList = null;
        }

        rankTextList = new List<Text>();

        Transform rootTransform = root.transform;

        for (int i = 1; i < 7; ++i)
        {
            Text textCom = rootTransform.Find(string.Format("TxtC_RankIndex{1}", i, i)).gameObject.GetComponent<Text>();
            textCom.text = i.ToString();
            rankTextList.Add(textCom);
        }

        if (root != null)
        {
            root.SetActive(false);
        }
    }

    public void Show(bool show)
    {
        if (root != null)
        {
            root.SetActive(show);
        }
    }

    public void Update(List<RealTimeRank> rankList)
    {
        if (rankTextList != null)
        {
            for (int i = 0; i < 6; ++i)
            {
                rankTextList[i].text = rankList[i].Id.ToString();
            }
        }
    }
}
