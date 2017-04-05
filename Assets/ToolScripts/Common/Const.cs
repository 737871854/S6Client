using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Const {
	public static bool DebugMode = true;					// 调试模式-用于内部测试
    public static bool UpdateMode = false;					// 实时更新模式
    public static bool LocalABMode = false;					// 实时更新模式

	public static string VersionFile = "Version.json";		// 更新版本文件
	public static string ConfigFile = "Config.json";		// 配置路径

	public static int TimerInterval = 1;
	public static int GameFrameRate = 60;					//游戏帧频

	public static bool UsePbc = false;						//PBC
	public static bool UseLpeg = false;						//LPEG
	public static bool UsePbLua = false;					//Protobuff-lua-gen
	public static bool UseCJson = false;					//CJson
	public static bool UseSQLite = false;					//SQLite

	public static string UserId = string.Empty;				//用户ID
	public static string AppName = "Q5";					//应用程序名称
	public static string AppPrefix = AppName + "_";			//应用程序前缀
	public static string ExtName = ".unity3d";				//素材扩展名
	public static string AssetDirname = "StreamingAssets";	//素材目录

#if UNITY_IPHONE
	public static string PackageUrl = "file://" + Application.streamingAssetsPath + "/";		//测试更新地址
#elif UNITY_STANDALONE_WIN
	public static string PackageUrl = "file:///" + Application.streamingAssetsPath + "/";		//测试更新地址
#elif UNITY_ANDROID
	public static string PackageUrl = Application.streamingAssetsPath + "/";				//测试更新地址
#else
	public static string PackageUrl = "file:///" + Application.streamingAssetsPath + "/";	//测试更新地址
#endif

	//本地编辑器测试用 测试更新地址
	public static string WebUrl = "http://192.168.16.96/q5/";											//测试更新地址
	//public static string LocalTestWebUrl = "http://192.168.16.96/develop_assetbundle/";				//资源开发测试指定路径
	public static string LocalTestWebUrl = "file:///" + Application.streamingAssetsPath + "/";		//默认安装包本地路径更新地址
    //public static string LocalTestWebUrl = "file:///C:/WorkS6/Res/AssetBundles/";		//本地指定路径

	public static int SocketPort = 8110;						//Socket服务器端口
	public static string SocketAddress = "192.168.0.21";		//Socket服务器地址

	public static Vector2 referenceResolution = new Vector2(1280, 720);

    /// <summary>
    /// 用一个string持有本地化的Tag
    /// enUS，zhTW,zhCN,ruRU,ptBR,koKR,itIT,frFR,esMX,esES,deDE
    /// </summary>
    public static string L10n = "zhCN";

    
}





