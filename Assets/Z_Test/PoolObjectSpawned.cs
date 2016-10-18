/********************************************************************
    Copyright (c) 2015,广州擎天柱网络科技有限公司
    All rights reserved.

    文件名称：PoolObjectSpawned.cs
    简    述：当此脚本挂靠在一个Prefab时，通过SimplePool创建池对象会自动执行上面的OnSpawned以及OnDespawned方法。
    创建标识：Lorry 2015/09/06

*********************************************************************/

using UnityEngine;
using System.Collections;
using PathologicalGames;

public class PoolObjectSpawned : MonoBehaviour
{
    /// <summary>
    /// 对象被创建完毕时自动执行此方法
    /// </summary>
    /// <param name="pool"></param>
    public void OnSpawned(SpawnPool pool)
    {
        Debug.Log
        (
            string.Format
            (
                "OnSpawned running for '{0}' in pool '{1}'.",
                this.name,
                pool.poolName
            )
        );
    }

    /// <summary>
    /// 对象被回收时自动执行此方法
    /// </summary>
    /// <param name="pool"></param>
    public void OnDespawned(SpawnPool pool)
    {
        Debug.Log
        (
            string.Format
            (
                "OnDespawned unning for '{0}' in pool '{1}'.",
                this.name,
                pool.poolName
            )
        );
    }
}
