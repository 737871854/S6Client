/**
 * Copyright (c) 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：LoadSceneMgr.cs
 * 简    述：控制场景的更新，在此进行特效和角色资源的预加载，导出到。
 * 创建标识：Lorry 2015/10/19 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class LoadingBar : MonoBehaviour
{
    private Image m_image;
    private Text m_text;
    void Awake()
    {
        //GameManager gameMgr = GameObject.Find("GameManager") as GameManager;
        m_image = GameObject.Find("ProgressBar").GetComponent("Image") as Image;
        m_text = GameObject.Find("Text").GetComponent("Text") as Text;
    }

    public void SetPercentage(float percentage)
    {
        m_image.fillAmount = percentage;
    }

    //Tip集合，实际开发中需要从外部文件中读取
    private string[] mTips = new string[]
                  {
                    "异步加载过程中你可以浏览游戏攻略",
                    "异步加载过程中你可以查看当前进度",
                    "异步加载过程中你可以判断是否加载完成",
                    "非常好玩的蜗牛竞速",
                    "难道是好运气今天都给你了！",
                  };

    void Update()
    {
        if(m_text != null)
        {
            m_text.text = mTips[Random.Range(0, 5)] + "(" + m_image.fillAmount * 100 + "%" + ")";
        }
    }
    void OnLevelWasLoaded(int level)
    {


    }

}

public class LoadSceneMgr
{
    public const string LOADING_UI_RES = "Assets/Prefabs/ui/Loading.prefab";
    static LoadSceneMgr m_instance;

    public static LoadSceneMgr Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new LoadSceneMgr();
            }
            return m_instance;
        }
    }

    private LoadingBar m_bar;

    /// <summary>
    /// 本来命名为scene,现在决定与Application.LoadLevel对应
    /// </summary>
    private string m_levelToLoad;
    private string m_levelAssetToLoad;

    private ResourceMisc.AssetWrapper m_levelAsset;
    //需要预加载的资源列表;
    private List<string> m_resourceList = new List<string>();
    private List<int> m_goCountInPool = new List<int>();

    /// <summary>
    /// 判断是否由LoadSceneMgr控制载入;
    /// </summary>
    public bool IsStartLoad = false;
    
    #region //公共函数初始化
    public void Init(GameObject load)
    {
        //GameObject load = GameObject.Find("LoadCanvas");
        if(load == null)
        {
            Debug.LogError("There is not LoadScene");
            return;
        }
        m_bar = load.AddComponent<LoadingBar>();
        GameObject.DontDestroyOnLoad(load);
    }

    public void SetLoadScene(string sceneName)
    {
        m_levelToLoad = sceneName;
        m_levelAssetToLoad = "Assets/Scenes/" + m_levelToLoad + "/" + m_levelToLoad + ".unity";
    }

    public void SetLoadScene(string assetName, string sceneName)
    {
        m_levelAssetToLoad = assetName;
        m_levelToLoad = sceneName;
    }

    public void AddPreLoadPrefab(string prefabName, int count = 0)
    {
        m_resourceList.Add(prefabName);
        m_goCountInPool.Add(count);
    }


    public void StartLoad()
    {
        IsStartLoad = true;
        Application.LoadLevel("empty");
    }
    #endregion
    
    #region 为了处理Loading背景和进度条而编写的函数(部分由GameManager调用)
    //载入了空场景
    public void OnLoadEmptylevel()
    {
        ioo.resourceManager.ClearAllAsset();
        ResourceMisc.AssetWrapper asset = ioo.resourceManager.LoadAsset(LOADING_UI_RES, typeof(GameObject));
        GameObject obj = GameObject.Instantiate(asset.GetAsset()) as GameObject;
        Init(obj);
        m_bar.SetPercentage(0);
        ioo.resourceManager.ReleaseAsset(asset);
        m_bar.StartCoroutine(OnSceneAssetLoading());
    }

    IEnumerator OnSceneAssetLoading()
    {
        Debug.Log("Time StartLoadLevelAsset:" + Time.realtimeSinceStartup + "|" + m_levelAssetToLoad);
        if(m_levelAssetToLoad != null && m_levelAssetToLoad !="")
        {
            m_levelAsset = ioo.resourceManager.LoadAsset(m_levelAssetToLoad, typeof(GameObject));
        }
        m_bar.SetPercentage(0.1f);
        Debug.Log("Time StartLoadLevel:" + Time.realtimeSinceStartup);
        //因为为unity载入资源一定需要audiolistener
        yield return new WaitForEndOfFrame();
        //如果场景特别大，这里应该用
        Application.LoadLevel(m_levelToLoad);
    }

    int m_resIndex;
    int m_level;
    //这里的0.3和0.7只是预估不同部分加载的时间，并不是精确的数字，为了进度条好看而已 Lorry
    public void OnLoadScnene(int level)
    {
        Debug.Log("Time LoadLevelEnd:" + Time.realtimeSinceStartup);
        m_bar.SetPercentage(0.3f);
        m_resIndex = 0;
        if (m_resourceList.Count == 0)
        {
            OnEndLoad();
            return;
        }

        m_level = level;
        m_bar.StartCoroutine(OnPreLoadingRes());
    }

    IEnumerator OnPreLoadingRes()
    {
        int resCount = m_resourceList.Count;
        float delta = 0.7f / resCount;

        int count = m_goCountInPool[m_resIndex];
        if (count == 0)
        {
            ioo.resourceManager.LoadAsset(m_resourceList[m_resIndex], typeof(GameObject));
        }
        else
        {
            SimplePool.Instance.CreatePrefabPool(m_resourceList[m_resIndex], count);
        }
        m_bar.SetPercentage(0.3f + m_resIndex * delta);
        m_resIndex++;
        if (m_resIndex < resCount)
        {
            yield return new WaitForSeconds(0.1f);
            m_bar.StartCoroutine(OnPreLoadingRes());
            //yield return new WaitForEndOfFrame();
        }
        else
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("PreLoadingRes Over!!!!!!");
            //IsStartLoad = false;
            m_resourceList.Clear();
            m_goCountInPool.Clear();
            OnEndLoad();
        }
    }

    //场景及预加载资源完毕时进行的操作;
    void OnEndLoad()
    {
        if (m_levelAsset != null)
        {
            ioo.resourceManager.ReleaseAsset(m_levelAsset);
            m_levelAsset = null;
        }
        //处理map的无效shader
        GameObject _gameobject = GameObject.Find("map");
        if (_gameobject != null)
            ProjectileBase.excuteShader(_gameobject);
        //资源加载完成之后，所有camera 的初始化和其他变化还是交给lua处理。
        //AudioListener temp = m_bar.gameObject.GetComponent<AudioListener>();
        GameObject.Destroy(m_bar.gameObject);

        //这里才调用lua的对应函数做进度;
        ioo.gameManager.uluaMgr.OnLevelLoaded(m_level);
    }
    #endregion

}

