using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.GZip;
using System.Net;

public class UtilCommon{
	
    public delegate void VoidDelegate(GameObject go, PointerEventData eventData = null);
    public delegate void DragDelegate(PointerEventData eventData);
    public delegate void ShakeDelegate(int id);
    public delegate void ShakeByBuildingDelegate(string buildingId, int level);
    public delegate void BoolDelagete(bool boolParam);
    //返回bool的无参代理
    public delegate bool BoolRetDelegate();

    public static int Int(object o) {
        return Convert.ToInt32(o);
    }

    public static float Float(object o) {
        return (float)Math.Round(Convert.ToSingle(o), 2);
    }

    public static long Long(object o) {
        return Convert.ToInt64(o);
    }

    public static int Random(int min, int max) {
        return UnityEngine.Random.Range(min, max);
    }

    public static float Random(float min, float max) {
        return UnityEngine.Random.Range(min, max);
    }

    public static string Uid(string uid) {
        int position = uid.LastIndexOf('_');
        return uid.Remove(0, position + 1);
    }

    public static long GetTime() { 
        TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
        return (long)ts.TotalMilliseconds;
    }

    /// <summary>
    /// 搜索子物体组件-GameObject版
    /// </summary>
    public static T Get<T>(GameObject go, string subnode) where T : Component {
        if (go != null) {
            Transform sub = go.transform.FindChild(subnode);
            if (sub != null) return sub.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 搜索子物体组件-Transform版
    /// </summary>
    public static T Get<T>(Transform go, string subnode) where T : Component {
        if (go != null) {
            Transform sub = go.FindChild(subnode);
            if (sub != null) return sub.GetComponent<T>();
        }
        return null;
    }

    /// <summary>
    /// 搜索子物体组件-Component版
    /// </summary>
    public static T Get<T>(Component go, string subnode) where T : Component {
        return go.transform.FindChild(subnode).GetComponent<T>();
    }
	
    public static Component AddComponent(GameObject go, string className) {
        Type t = Type.GetType(className);
		/*
		if (t == null)
		{
			System.Reflection.Assembly assembly = System.Reflection.Assembly.Load("UnityEngine.dll") ;
			t = assembly.GetType(className);
		}
		*/
        return AddComponent(go, t);
    }

    public static Component AddComponent(GameObject go, Type type) {
        return go.AddComponent(type);
    }

    public static Component AddComponent<T>(GameObject go) where T : Component {
        return go.AddComponent<T>();
    }

    public static Component GetComponent(GameObject go, string className) {
        return go.GetComponent(className);
    }

    public static Component GetComponent(GameObject go, Type t) {
        return go.GetComponent(t);
    }

    /// <summary>
    /// 查找子对象
    /// </summary>
    public static GameObject Child(GameObject go, string subnode) {
        return Child(go.transform, subnode);
    }

    /// <summary>
    /// 查找子对象
    /// </summary>
    public static GameObject Child(Transform go, string subnode) {
        Transform tran = go.FindChild(subnode);
        if (tran == null) return null;
        return tran.gameObject;
    }

    public static GameObject ChildRecursion(GameObject go, string name) {
        Transform t = go.transform;
        for (int i = 0; i < t.childCount; ++i) {
            Transform child = t.GetChild(i);
            if (child.name == name) {
                return child.gameObject;
            } else {
                GameObject childNext = ChildRecursion(child.gameObject, name);
                if (childNext != null)
                    return childNext.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    public static GameObject Peer(GameObject go, string subnode) {
        return Peer(go.transform, subnode);
    }

    /// <summary>
    /// 取平级对象
    /// </summary>
    public static GameObject Peer(Transform go, string subnode) {
        Transform tran = go.parent.FindChild(subnode);
        if (tran == null) return null;
        return tran.gameObject;
    }

    /// <summary>
    /// 手机震动
    /// </summary>
    public static void Vibrate() {
        //int canVibrate = PlayerPrefs.GetInt(Const.AppPrefix + "Vibrate", 1);
        //if (canVibrate == 1) iPhoneUtils.Vibrate();
    }

    /// <summary>
    /// Base64编码
    /// </summary>
    public static string Encode(string message) {
        byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(message);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Base64解码
    /// </summary>
    public static string Decode(string message) {
        byte[] bytes = Convert.FromBase64String(message);
        return Encoding.GetEncoding("utf-8").GetString(bytes);
    }

    /// <summary>
    /// 判断数字
    /// </summary>
    public static bool IsNumeric(string str) {
        if (str == null || str.Length == 0) return false;
        for (int i = 0; i < str.Length; i++ ) {
            if (!Char.IsNumber(str[i])) { return false; }
        }
        return true;
    }

    /// <summary>
    /// HashToMD5Hex
    /// </summary>
    public static string HashToMD5Hex(string sourceStr) {
        byte[] Bytes = Encoding.UTF8.GetBytes(sourceStr);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider()) {
            byte[] result = md5.ComputeHash(Bytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                builder.Append(result[i].ToString("x2"));
            return builder.ToString();
        }
    }

    /// <summary>
    /// 计算字符串的MD5值
    /// </summary>
    public static string md5(string source) {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
        byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
        md5.Clear();

        string destString = "";
        for (int i = 0; i < md5Data.Length; i++) {
            destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
        }
        destString = destString.PadLeft(32, '0');
        return destString;
    }

        /// <summary>
    /// 计算文件的MD5值
    /// </summary>
    public static string md5file(string file) {
        try {
            FileStream fs = new FileStream(file, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++) {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        } catch (Exception ex) {
            throw new Exception("md5file() fail, error:" + ex.Message);
        }
    }

    /// <summary>
    /// 功能：压缩字符串
    /// </summary>
    /// <param name="infile">被压缩的文件路径</param>
    /// <param name="outfile">生成压缩文件的路径</param>
    public static void CompressFile(string infile, string outfile) {
        Stream gs = new GZipOutputStream(File.Create(outfile));
        FileStream fs = File.OpenRead(infile);
        byte[] writeData = new byte[fs.Length];
        fs.Read(writeData, 0, (int)fs.Length);
        gs.Write(writeData, 0, writeData.Length);
        gs.Close(); fs.Close();
    }

    /// <summary>
    /// 功能：输入文件路径，返回解压后的字符串
    /// </summary>
    public static string DecompressFile(string infile) {
        string result = string.Empty;
        Stream gs = new GZipInputStream(File.OpenRead(infile));
        MemoryStream ms = new MemoryStream();
        int size = 2048;
        byte[] writeData = new byte[size]; 
        while (true) {
            size = gs.Read(writeData, 0, size); 
            if (size > 0) {
                ms.Write(writeData, 0, size); 
            } else {
                break; 
            }
        }
        result = new UTF8Encoding().GetString(ms.ToArray());
        gs.Close(); ms.Close();
        return result;
    }

    /// <summary>
    /// 压缩字符串
    /// </summary>
    public static string Compress(string source) {
        byte[] data = Encoding.UTF8.GetBytes(source);
        MemoryStream ms = null;
        using (ms = new MemoryStream()) {
            using (Stream stream = new GZipOutputStream(ms)) {
                try {
                    stream.Write(data, 0, data.Length);
                } finally {
                    stream.Close();
                    ms.Close();
                }
            }
        }
        return Convert.ToBase64String(ms.ToArray());
    }

    /// <summary>
    /// 解压字符串
    /// </summary>
    public static string Decompress(string source) {
        string result = string.Empty;
        byte[] buffer = null;
        try {
            buffer = Convert.FromBase64String(source);
        } catch {
            Debug.LogError("Decompress---->>>>" + source);
        }
        using (MemoryStream ms = new MemoryStream(buffer)) {
            using (Stream sm = new GZipInputStream(ms)) {
                StreamReader reader = new StreamReader(sm, Encoding.UTF8);
                try {
                    result = reader.ReadToEnd();
                } finally {
                    sm.Close();
                    ms.Close();
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 清除所有子节点
    /// </summary>
    public static void ClearChild(Transform go) {
        if (go == null) return;
        for (int i = go.childCount - 1; i >= 0; i--) {
            GameObject.Destroy(go.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 生成一个Key名
    /// </summary>
    public static string GetKey(string key) {
        return Const.AppPrefix + Const.UserId + "_" + key; 
    }

    /// <summary>
    /// 取得整型
    /// </summary>
    public static int GetInt(string key) {
        string name = GetKey(key);
        return PlayerPrefs.GetInt(name);
    }

    /// <summary>
    /// 有没有值
    /// </summary>
    public static bool HasKey(string key) {
        string name = GetKey(key);
        return PlayerPrefs.HasKey(name);
    }

    /// <summary>
    /// 保存整型
    /// </summary>
    public static void SetInt(string key, int value) {
        string name = GetKey(key);
        PlayerPrefs.DeleteKey(name);
        PlayerPrefs.SetInt(name, value);
    }

    /// <summary>
    /// 取得数据
    /// </summary>
    public static string GetString(string key) {
        string name = GetKey(key);
        return PlayerPrefs.GetString(name);
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    public static void SetString(string key, string value) {
        string name = GetKey(key);
        PlayerPrefs.DeleteKey(name);
        PlayerPrefs.SetString(name, value);
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    public static void RemoveData(string key) {
        string name = GetKey(key);
        PlayerPrefs.DeleteKey(name);
    }

    /// <summary>
    /// 清理内存
    /// </summary>
    public static void ClearMemory() {
        GC.Collect(); Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 是否为数字
    /// </summary>
    public static bool IsNumber(string strNumber) {
        Regex regex = new Regex("[^0-9]");
        return !regex.IsMatch(strNumber);
    }

    /// <summary>
    /// 取得数据存放目录
    /// </summary>
    public static string DataPath {
        get {
			/*
            string game = Const.AppName.ToLower();
            if (Application.platform == RuntimePlatform.IPhonePlayer || 
                Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.WP8Player) {
                return Application.persistentDataPath + "/" + game + "/";
            } 
            if (Const.DebugMode) {
				return Application.dataPath + "/";// + "/" + Const.AssetDirname + "/";
            }else{
			}
            return "c:/" + game + "/";
			*/
			string path;
			if(!Application.isEditor)
			{
				path = Application.persistentDataPath;
			}
			else
			{
				path = Application.dataPath;
			}
			return path + "/";
        }
    }

    public static string resourcesPath {
        get{
            return Application.persistentDataPath + "/resources/";            //    return Application.dataPath + "/resources/";
        }
    }

    public static string storePath {
        get {
            return Application.persistentDataPath + "/datastore/";
        }
    }

    /// <summary>
    /// 取得行文本
    /// </summary>
    public static string GetFileText(string path) {
        return File.ReadAllText(path);
    }

    /// <summary>
    /// 网络可用
    /// </summary>
    public static bool NetAvailable {
        get {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }

    /// <summary>
    /// 是否是无线
    /// </summary>
    public static bool IsWifi {
        get {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }
    }

    /// <summary>
    /// 应用程序内容路径
    /// </summary>
    public static string AppContentPath() {
        string path = string.Empty;
        switch (Application.platform) {
            case RuntimePlatform.Android:
                path = "jar:file://" + Application.dataPath + "!/assets";
            break;
            case RuntimePlatform.IPhonePlayer:
                path = Application.dataPath + "/Raw";
            break;
            default:
                path = Application.dataPath + "/" + Const.AssetDirname;
            break;
        }
        return path;
    }

    /// <summary>
    /// 是否是登录场景
    /// </summary>
    public static bool isLogin {
        get { return Application.loadedLevelName.CompareTo("login") == 0; }
    }

    /// <summary>
    /// 是否是城镇场景
    /// </summary>
    public static bool isMain {
        get { return Application.loadedLevelName.CompareTo("main") == 0; }
    }

    /// <summary>
    /// 判断是否是战斗场景
    /// </summary>
    public static bool isFight {
        get { return Application.loadedLevelName.CompareTo("fight") == 0; }
    }

    public static string LuaPath() {
        if (Application.isEditor) {
            return Application.dataPath + "/Lua/";
        }
        return DataPath + "lua/"; 
    }

    /// <summary>
    /// 取得Lua路径
    /// </summary>
    public static string LuaPath(string name) {
		if (name.EndsWith(".lua"))
			return LuaPath() + name;
		else
			return LuaPath() + name + ".lua";
    }

    public static string JsonConfigPath(string name) {
        return LuaPath() + name;
    }

	public static GameObject LoadAsset(string path, Type type) {
		return null;
	}

	public static string ParseArgName(string arg){
		if(!arg.StartsWith("$")) return null;
		return arg.Substring(1, arg.Length - 1);
	}

    public static string ReadAllTextFromFile(string path) {
        return System.IO.File.ReadAllText(path);
    }

    public static void WriteAllTextToFile(string path, string content) {
        System.IO.File.WriteAllText(path, content);
    }

    public static bool IsFileExist(string path) {
        return System.IO.File.Exists(path);
    }

    public static bool IsDirectoryExist(string path) {
        return System.IO.Directory.Exists(path);
    }

    public static bool CreateDirectory(string path) {
        bool result = true;
        try {
            System.IO.Directory.CreateDirectory(path);
        } catch (Exception exp) {
            Debugger.LogError(string.Format("create directory fail {0}", exp.ToString()));
            result = false;
        }
        return result;
    }

	public static int BitLshift(int n, int c){
		return n << c;
	}

	public static int BitRshift(int n, int c){
		return n >> c;
	}

	public static int Inv(int n){
		return ~n;
	}

	public static int BitAnd(int l, int r){
		return l & r;
	}

	public static int BitOr(int l, int r){
		return l | r;
	}

	public static int BitXor(int l, int r){
		return l ^ r;
	}

	public static Vector3 GetXZProj(Vector3 vec3){
		return new Vector3(vec3.x, 0, vec3.z);
	}

	//递归搜索某个go的孩子，根据名字
	public static Transform SearchChildByName(Transform target, string name){
		if(target.name == name) return target;
		for(int i = 0; i < target.childCount; ++i){
			var result = SearchChildByName(target.GetChild(i), name);
			if(result != null) return result;
		}
		
		return null;
	}

	public static bool AttachEffect(GameObject obj, GameObject effectObject, string attachJoint){
		Transform attachTransform = SearchChildByName(obj.transform, attachJoint);
		
		if(attachTransform == null){
			Debugger.LogError("attach joint " + attachJoint + " doesn't exist in this gameobject " + obj.name );
			return false;
		}

		effectObject.transform.SetParent(attachTransform, false);
		return true;
	}

	public static bool IsOverUI()
	{
        if (EventSystem.current)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        return false;
	}

    #region 资源更新和版本检查 // Fanki 更新资源和版本效验 2015-08-12
    /// <summary>
    /// 获得版本号
    /// </summary>
    /// <returns></returns>
    public static string GetVersion()
    {
        return version;
    }

    /// <summary>
    /// 设置版本号
    /// </summary>
    /// <param name="versionStr"></param>
    public static void SetVersion(string versionStr)
    {
        version = versionStr;
    }

    /// <summary>
    /// 对比版本号，0不用更新，1更新资源，2重新下载安装包
    /// </summary>
    /// <param name="oldVersionStr">旧版本号</param>
    /// <param name="newVersionStr">新版本号</param>
    /// <returns></returns>
    public static int VersionCompare(string oldVersionStr, string newVersionStr)
    {
        int comType = 0;
        if (oldVersionStr.Equals(newVersionStr))
        {
            return comType;
        }
        else
        {
            string[] s1 = oldVersionStr.Split(new char[] { '.' });
            string[] s2 = newVersionStr.Split(new char[] { '.' });
            // 2 是大版本号
            // 1 是小资源号
            if (int.Parse(s2[2]) > int.Parse(s1[2]))
            {
                return 2;
            }
            else if (int.Parse(s2[2]) < int.Parse(s1[2]))
            {
                // 特殊处理
                if (IsFirstOpen())
                {
                    return 1;
                }
            }

            return 1;
        }
    }

    /// <summary>
    /// 读json文件返回版本号
    /// </summary>
    /// <param name="verFileName"></param>
    /// <returns></returns>
    public static string ReadVersionFile(string verFilePath)
    {
        string versionPath = verFilePath;
        if (IsFileExist(versionPath))
        {
            string content = ReadAllTextFromFile(versionPath);
            LitJson.JsonReader reader = new LitJson.JsonReader(content);
            //VersionData config = LitJson.JsonMapper.ToObject<VersionData>(reader);
            LitJson.JsonData config = LitJson.JsonMapper.ToObject(reader);

            return (string)config["version"];
        }
        else
        {
            return "0.0.0.0";
        }
    }

    /// <summary>
    /// 是否第一次更新
    /// </summary>
    /// <returns></returns>
    public static bool IsFirstOpen()
    {
        return !IsFileExist(Application.persistentDataPath + "/Version.json");
    }

    /// <summary>
    /// 获得网络下载的版本号，临时的
    /// </summary>
    /// <returns></returns>
    public static string GetTempVersion()
    {
        return tempVersion;//改成对应的版本号就可以，测试某个版本的 1.1.x.x
    }

    /// <summary>
    /// 设置临时网络最新版本号
    /// </summary>
    /// <returns></returns>
    public static void SetTempVersion(string newVersion)
    {
        tempVersion = newVersion;
    }

	public static double ByteToNumber(byte[] bytes)
	{
		return BitConverter.ToDouble (bytes, 0);
	}

	public static byte[] NumberToByte(double vlue)
	{
		return BitConverter.GetBytes (vlue);
	}

	public static string[] GetFiles(string path, string math = "")
	{
		if (math == "")
			math = "*";

		string[] files = System.IO.Directory.GetFiles(path, math, System.IO.SearchOption.AllDirectories);

		return files;
	}

    public static string version = "0.0.0.0";
    public static string tempVersion = "0.0.0.0";
    #endregion // Fanki 更新资源和版本效验 2015-08-12

    #region 加载配置文件接口 友庆原创
    /// <summary>
    /// 加载lua文件
    /// </summary>
    public static string LoadLuaConfigFile(string luaFile)
    {
        string path = LuaPath(luaFile);
        return ReadAllTextFromFile(path);
    }

    /// <summary>
    /// 加载Json配置文件， 返回字符串
    /// </summary>
    public static string LoadJsonConfigFile(string jsonFile)
    {
        string path = JsonConfigPath(jsonFile);
        return ReadAllTextFromFile(path);
    }

    /// <summary>
    /// 加载Json配置，返回对应的类对象
    /// </summary>
    public static T LoadJsonConfig<T>(string jsonFile)
    {
        string content = LoadJsonConfigFile(jsonFile);
        return LitJson.JsonMapper.ToObject<T>(content);
    }
    #endregion

    public static string GetLocalIPAddress()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "";
    }
}
