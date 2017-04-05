using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Demo : MonoBehaviour
{
    private List<GameObject> _poolObjList = new List<GameObject>();

    void Start()
    {
        //需要自定义创建池对象里的个数时请调用此方法，否则直接调用Spawn
        SimplePool.Instance.CreatePrefabPool("Assets/Z_Test/Cube.prefab", 2);
    }

    void OnGUI()
    {
        //if (GUILayout.Button("Create Pool"))
        //{
        //    //创建CubePool池
        //    GameObject go = SimplePool.Instance.Spawn("Assets/Z_Test/Cube.prefab", Vector3.zero, Quaternion.identity);
        //    _poolObjList.Add(go);
        //}

        //if (GUILayout.Button("Recyle Object"))
        //{
        //    if (_poolObjList.Count < 1)
        //        return;
        //    for (int i = 0; i < _poolObjList.Count; i++)
        //    {
        //        SimplePool.Instance.DeSpawn(_poolObjList[i]);
        //    }
        //    _poolObjList.Clear();
        //}

        //if (GUILayout.Button("Release Pool"))
        //{
        //    //释放CubePool池
        //    SimplePool.Instance.Release();
        //}
    }
}
