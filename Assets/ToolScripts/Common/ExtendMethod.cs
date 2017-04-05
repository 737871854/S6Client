/*
 * Copyright (c) 
 * 
 * 文件名称：   ExtendMethod.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Mike 2017/4/5 15:07:24
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public static class ExtendMethod
{

    public static T GetOrAddComponent<T>(this GameObject go) where T : Component, new()
    {
        T t = go.GetComponent<T>();
        if (null == t)
            t = go.AddComponent<T>();
        return t;
    }
}
