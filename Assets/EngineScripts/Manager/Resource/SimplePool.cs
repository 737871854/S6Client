/********************************************************************
    Copyright (c) 2015,广州擎天柱网络科技有限公司
    All rights reserved.

    文件名称：SimplePool.cs
    简    述：简单池缓冲。
              执行SimplePool的Spawn方法后会自动执行池对象上OnSpawned(SpawnPool pool)方法
              执行SimplePool的DeSpawn方法后会自动执行池对象上OnDespawned(SpawnPool pool)方法
              以上两个方法需自行创建
    创建标识：Lorry 2015/09/06

    修改描述：整理代码以符合Q6项目
    修改标识：zon 2015/12/18
*********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public class SimplePool : MonoBehaviour
{
    private const string CONTAINER_NAME = "SimplePool";
    private static SimplePool _instance;
    public static SimplePool Instance
    {
        get
        {
            if (_instance == null)
            {
                SimplePool counter = (SimplePool)FindObjectOfType(typeof(SimplePool));
                if (counter != null && counter.IsPlacedCorrectly())
                {
                    _instance = counter;
                }
                else
                {
                    GameObject go = new GameObject(CONTAINER_NAME);
                    //go.layer = 7;
                    _instance = go.AddComponent<SimplePool>();
                }
            }
            return _instance;
        }
    }

    private SpawnPool _dogFacePool;
    private List<string> _prefabList;
    private Dictionary<string, Transform> _prefabDic;
    private Transform _dogFacePrefab;
    /// <summary>
    /// 池中对象的保存位置，避免被玩家看到。
    /// </summary>
    private Vector3 POOL_POS = new Vector3(0, -100, 0);


    private bool IsPlacedCorrectly()
    {
        return (transform.parent == null &&
        GetComponentsInChildren<Component>().Length == 1);
    }

    #region MonoBehaviour

    void Awake()
    {
        Create();
    }

    void OnDestroy()
    {
        Release();
        _instance = null;
    }

    #endregion
    
    /// <summary>
    /// 在这里进行初步的池建立，因为我们需要用的prefab类型太多，所以只针对需要反复使用的特效和战斗单位使用。
    /// 如果每个prefab都使用pool，这样一来pool就会太多了。
    /// </summary>
    void Create()
    {
        _prefabList = new List<string>();
        _prefabDic = new Dictionary<string, Transform>();
        _dogFacePool = PoolManager.Pools.Create("Dogface", this.gameObject);
    }

    /// <summary>
    /// 释放对象池
    /// </summary>
    public void Release()
    {
        PoolManager.Pools.Destroy("Dogface");
        _dogFacePool = null;
        _prefabDic = null;
        _prefabList = null;
    }

    /// <summary>
    /// 创建对象到池里
    /// </summary>
    /// <param name="resPath">对象路径</param>
    /// <param name="count">对象个数</param>
    /// <returns></returns>
    public Transform CreatePrefabPool(string resPath, int count)
    {
        if (_prefabDic.ContainsKey(resPath) == false)
        {
            ResourceMisc.AssetWrapper asset = ioo.resourceManager.LoadAsset(resPath, typeof(GameObject));
            GameObject obj = asset.GetAsset() as GameObject;
            obj.transform.position = POOL_POS;
            obj.transform.rotation = Quaternion.identity;
            ResourceManager.excuteShader(obj);
            _dogFacePrefab = obj.transform;
            PrefabPool prefabPool = new PrefabPool(_dogFacePrefab);
            prefabPool.preloadAmount = count;      // 预创建的个数
            prefabPool.limitInstances = false;
            _dogFacePool.CreatePrefabPool(prefabPool);
            _prefabDic.Add(resPath, _dogFacePrefab);
        }
        return _prefabDic[resPath];
    }

    /// <summary>
    /// 创建池对象，首次创建会自动缓存15个，如需控制个数使用CreatePrefabPool()
    /// </summary>
    /// <param name="resPath">对象路径</param>
    /// <param name="pos">生成对象坐标</param>
    /// <param name="rot">旋转方向</param>
    /// <returns></returns>
    public GameObject Spawn(string resPath, Vector3 pos, Quaternion rot)
    {
        if (_prefabDic.ContainsKey(resPath) == false)
            CreatePrefabPool(resPath, 15);

        Transform inst;
        inst = _dogFacePool.Spawn(_prefabDic[resPath], pos, rot);
        return inst.gameObject;
    }

    /// <summary>
    /// 回收对象到池里
    /// </summary>
    /// <param name="obj">回收对象</param>
    public void DeSpawn(GameObject obj)
    {
        _dogFacePool.Despawn(obj.transform);
    }
}
