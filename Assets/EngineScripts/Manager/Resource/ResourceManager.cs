/********************************************************************
    Copyright (c) 2015,骞垮窞鎿庡ぉ鏌辩綉缁滅?鎶鏈夐檺鍏?徃
    All rights reserved.

    鏂囦欢鍚嶇О锛歊esourceManager.cs
    绠    杩帮細璧勬簮鍔犺浇锛屽師鏉ヨ?鍚勭?閫昏緫浠ｇ爜鐩存帴浣跨敤锛屼负浜嗘彁楂楢pp杩愯?鏁堢巼锛屽湪浣跨敤闇瑕佸?娆″姞杞界殑璧勬簮鏃讹紝
    璇蜂紭鍏堜娇鐢⊿implePool.Instance.Spawn鏉ヨ繘琛孏ameObject鐨勫垱寤恒傚叾鍐呴儴瀵逛簬璧勬簮鍔犺浇鐨勪紭鍖曁
    鍒涘缓鏍囪瘑锛歀orry 2015/10/8
*********************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ResourceMisc;
using UnityEngine.Assertions;
using System.Text.RegularExpressions;

public class ResourceManager : MonoBehaviour 
{
	const string K_FILELIST = "FileList.json";
    #region resource container

	// bundle鏄?惁鍦烘櫙鍖勌
	List<string>						_sceneBundles	= new List<string>();

	// 璧勬簮璺?緞瀵瑰寘鍚嶅掓煡琛?
	Dictionary<string, string>			_asset2Bundle	= new Dictionary<string, string>();

	// bundle缂撳瓨;
	Dictionary<string, BundleWrapper>	m_bundleCache	= new Dictionary<string, BundleWrapper>();

	// asset缂撳瓨;
	Dictionary<string, AssetWrapper>	m_assetWrapperCache		= new Dictionary<string, AssetWrapper>();

    // bundle渚濊禆鏂囦欢;
    AssetBundleManifest m_manifest;
    AssetBundle m_ab;

    // 鏈?湴鍖曠L10n)bundle渚濊禆鍏崇郴鏂囦欢;
    AssetBundleManifest m_manifestL10n;
    AssetBundle m_abL10n;

    bool m_isLocalization = false;
    #endregion//res container

    string GetLocalFileUrl(string parPath)
	{
        string path = "";
        if (Const.LocalABMode)
        {
            path = Const.LocalTestWebUrl.Replace("file:///", "");
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    path += "iphone/assetbundle/";
                    break;
                case RuntimePlatform.Android:
                    path += "android/assetbundle/";
                    break;
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    path += "pc/assetbundle/";
                    break;
                case RuntimePlatform.OSXEditor:
                    path += "iphone/assetbundle/";
                    break;
            }
        }
        else
        {
            path = Util.resourcesPath;
        }
        if (m_isLocalization)
            return path + "L10n/" + Const.L10n + "/" + parPath;
        return path + parPath;
    }

    /// <summary>
    /// 璁剧疆璇诲彇鏈?湴鍖栬祫婧愶紝杩樻槸Common璧勬簮銆佁
    /// </summary>
    void SetLocalization(bool param)
    {
        m_isLocalization = param;
    }

    void LoadFileList()
	{
		if(!System.IO.File.Exists(GetLocalFileUrl(K_FILELIST)))
			return;

		FileList res_info = FileList.LoadFromFile(GetLocalFileUrl(K_FILELIST));
		
		if (res_info == null)
			return;

		// 璁板綍璧勬簮鍊掓煡琛?
		foreach (KeyValuePair<string, string> pair in res_info.res2bundleData)
			_asset2Bundle.Add(pair.Key, pair.Value);

		// 璁板綍鍦烘櫙鍖呭垪琛?
		foreach (KeyValuePair<string, string> pair in res_info.bundle2sceneData)
			_sceneBundles.Add(pair.Key);

	}

    //鏈?嚱鏁板彧鑳借皟鐢ㄤ竴娆犨init
    public void initialize()
    {
        if(m_ab != null)
        {
            Debugger.LogError("閲嶅?璋冪敤ResourceManager initialize");
            return;
        }

        SetLocalization(false);
        LoadFileList();
        //璇诲彇鍑烘暣涓?洰褰曠殑渚濊禆鍏崇郴,杩欎釜鏂囦欢鍚嶇О鍜岀洰褰曞悕绉颁竴鏍耳
        string mUrl = GetLocalFileUrl("assetbundle");
        byte[] dep_content = System.IO.File.ReadAllBytes(mUrl);
        m_ab = AssetBundle.LoadFromMemory(dep_content);
        Assert.IsNotNull<AssetBundle>(m_ab, "璧勬簮涓嶅瓨鍦?" + mUrl);
        m_manifest = (AssetBundleManifest)m_ab.LoadAsset("AssetBundleManifest");

        SetLocalization(true);
        //瀵逛簬鏈?湴鍖栫殑璧勬簮杩涜?璇诲彇锛屾渶閲嶈?鐨勬槸璇诲彇璧勬簮鍏宠仈鐨勬柟寮廁
        mUrl = GetLocalFileUrl("assetbundle");
        if(System.IO.File.Exists(mUrl))
        {
            dep_content = System.IO.File.ReadAllBytes(mUrl);
            m_abL10n = AssetBundle.LoadFromMemory(dep_content);
            Assert.IsNotNull<AssetBundle>(m_abL10n, "璧勬簮涓嶅瓨鍦?" + mUrl);
            m_manifestL10n = (AssetBundleManifest)m_ab.LoadAsset("AssetBundleManifest");
        }else
        {
            Debugger.LogWarning("L10n 鏈?湴鍖栨枃浠朵笉瀛樺湪:" + mUrl);
        }
        //mab.Unload(false);
    }

	// 鍒ゆ柇鏄?惁鍦烘櫙鍖勌
	private bool IsSceneBundle(string bundleName)
	{
		return _sceneBundles.Contains(bundleName);
	}

	// 鍚屾?鍔犺浇
    public AssetWrapper LoadAsset(string assetPath, System.Type assetType)
    {//鍦╡ditor涓?紝鍏堝湪鐩?綍璧勬簮涓?姞杞斤紝鍔犺浇涓嶅埌鍐嶈繘鍏ョ洰褰曚腑杩涜?
        //string realPath = assetPath.Replace("\\", "/");
#if UNITY_EDITOR
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath(assetPath, assetType);
            if (asset != null)
            {//濡傛灉鏄疎ditor涓?殑璧勬簮灏变笉鍋氳繃澶氬?鐞嗕簡.
                AssetWrapper ret = new AssetWrapper(asset, assetPath, assetType, "", false);
                return ret;
            }
        }
#endif
#if UNITY_IPHONE
        Debugger.Log("IOS loadRes:" + assetPath);
#endif
        //鍘籥ssetbundle涓?繘琛屽姞杞茧棣栧厛鎵惧埌璧勬簮瀵瑰簲鐨刟b; Lorry
        if (_asset2Bundle.ContainsKey(assetPath) == false)
        {
            Debug.LogError("_asset2Bundle.ContainsKey(" + assetPath + ") == false");
            return null;
        }

        //棣栧厛鐪嬭祫婧怉ssetWrapper鐨勭紦瀛樺瓧鍏氟
        if (m_assetWrapperCache.ContainsKey(assetPath))
        {
            m_assetWrapperCache[assetPath].AddRef();
            return m_assetWrapperCache[assetPath];
        }

        string abName = _asset2Bundle[assetPath];
        //闇瑕侀?鍏堝垽鏂?姞杞歼Lorry
        if (m_bundleCache.ContainsKey(abName))
        {//宸茬粡瀛樺湪姝?undle鐨勭紦瀛橈紝浣嗘槸娌℃湁asset缂撳瓨锛岃瘉鏄庤祫婧愬寘宸茬粡鍔犺浇锛屽彧鏄?病鏈塴oad 姝＿asset,杩欓噷娌℃湁澶勭悊濂界殑
            Object obj = m_bundleCache[abName].GetBundle().LoadAsset(assetPath, assetType);
            AssetWrapper ret = new AssetWrapper(obj, assetPath, assetType, abName, IsSceneBundle(abName));
            m_assetWrapperCache.Add(assetPath, ret);
            //TODO:鑰冭檻缁檅undle娣诲姞寮曠敤,浣嗕及璁狓0%涓嶇敤閿姣?闇浠旂粏鑰冭檻 Lorry
            m_bundleCache[abName].AddRefAsset(assetPath);
            return ret;
        }


        //鍒ゆ柇姝よ繖涓猘ssetPath鏄?惁鏈夋湰鍦扮増鏈?
        SetLocalization(true);
        string target_local_path = GetLocalFileUrl(abName);
        if (System.IO.File.Exists(target_local_path))
            return LoadRes(assetPath, assetType);
        SetLocalization(false);
        return LoadRes(assetPath, assetType);
	}


    private AssetWrapper LoadRes(string assetPath, System.Type assetType)
    {
        //寮濮嬪姞杞藉?搴旂殑璧勬簮鍖勭.assetbundle)
        string abName = _asset2Bundle[assetPath];
        string[] dps;
        if(m_isLocalization)
            dps = m_manifestL10n.GetAllDependencies(abName);
        else
            dps = m_manifest.GetAllDependencies(abName);

        for (int i = 0; i < dps.Length; i++)
        {
            string dep_name = dps[i];
            if (m_bundleCache.ContainsKey(dep_name))
            {
                m_bundleCache[dep_name].AddRefAsset(assetPath);
                continue;
            }
            else
            {
                if (string.IsNullOrEmpty(dep_name))
                {
                    Log.PrintMsg("鏌ヤ竴涓娺" + abName + " 鐨刴anifest鏂囦欢锛屾湁渚濊禆鏄?┖鐨勩佱");
                    continue;
                }
                //string local_path = GetLocalFileUrl(dep_name);
                //byte[] dep_content = System.IO.File.ReadAllBytes(local_path);
                //AssetBundle dep_bundle = AssetBundle.CreateFromMemoryImmediate(dep_content);
                ////AssetBundle dep_bundle = AssetBundle.CreateFromFile(local_path); //鍦ㄦ煇浜涘钩鍙颁笂璇诲彇鍘嬬缉鏂囦欢浼氬穿婧冨緟鏂皏ersion瑙ｅ喅
                //Assert.IsNotNull<AssetBundle>(dep_bundle, "渚濊禆鎵撳寘璧勬簮鏈夐棶棰楖" + dep_name);
                //Object temp = dep_bundle.mainAsset;
                //if(temp!= null)
                //    Debugger.Log("dep_bundle mainAsset:" + temp.name);
                //BundleWrapper loaded_bundle_bw = new BundleWrapper(dep_name, dep_bundle);
                //loaded_bundle_bw.AddRefAsset(assetPath);
                //m_bundleCache.Add(dep_name, loaded_bundle_bw); // keep in bundle cache
                BundleWrapper loaded_bundle_bw = LoadBundle(dep_name);
                loaded_bundle_bw.AddRefAsset(assetPath);
                Object temp = loaded_bundle_bw.GetBundle().mainAsset;
                if (temp != null)
                    Debugger.Log("dep_bundle mainAsset:" + temp.name);

            }
        }//渚濊禆璧勬簮鍔犺浇瀹屾瘯; Lorry


        if (m_bundleCache.ContainsKey(abName))
        {//宸茬粡瀛樺湪姝?undle鐨勭紦瀛橈紝浣嗘槸娌℃湁asset缂撳瓨锛岃瘉鏄庤祫婧愬寘宸茬粡鍔犺浇锛屽彧鏄?病鏈塴oad 姝＿asset,鍦ㄧ壒娈婃儏鍐典笅鍑虹幇bug
            Object obj = m_bundleCache[abName].GetBundle().LoadAsset(assetPath, assetType);
            AssetWrapper ret = new AssetWrapper(obj, assetPath, assetType, abName, IsSceneBundle(abName));
            m_assetWrapperCache.Add(assetPath, ret);
            //TODO:鑰冭檻缁檅undle娣诲姞寮曠敤,浣嗕及璁狓0%涓嶇敤閿姣?闇浠旂粏鑰冭檻 Lorry
            m_bundleCache[abName].AddRefAsset(assetPath);
            return ret;
        }

        // load target bundle
        BundleWrapper target_bundle_bw = LoadBundle(abName);
        target_bundle_bw.AddRefAsset(assetPath);
        // load asset
        AssetWrapper ret_aw;
        if (IsSceneBundle(abName))
        {//濡傛灉鏄?満鏅?紝assetBundle杞藉叆鍚庯紝閫氳繃loadlevel杩涘叆;
            ret_aw = new AssetWrapper(null, assetPath, assetType, abName, true);
        }
        else
        {
			// 璇诲彇鏈?湴鍖栬祫婧愭椂锛屽皢瀹為檯璺?緞涓?殑/L10n/xxx/琛ヤ笂; ly0464
			string realPath = assetPath;
			if (m_isLocalization)
				realPath = Regex.Replace(realPath, "^Assets/", "Assets/L10n/" + Const.L10n + "/");
			Object obj = target_bundle_bw.GetBundle().LoadAsset(realPath, assetType);
			if (obj == null)
            {
                Debugger.LogError("targetBundle Wrong!");
                return null;
            }
            ret_aw = new AssetWrapper(obj, assetPath, assetType, abName, false);
            // add bundle refs
            //AddBundleRefAsset(abName, assetPath);
        }
        m_assetWrapperCache.Add(assetPath, ret_aw);

        return ret_aw;
    }

    // 閲婃斁璧勬簮寮曠敤鏁?
    public void ReleaseAsset(AssetWrapper asset)
    {
        asset.Release();
        if (asset.GetRefCount() < 1)
        {
            // Destroy
            DestroyAsset(asset);
        }
    }

    //鍦ㄨ繃鍦烘櫙鐨勬椂鍊欐竻鐞嗘墍鏈夎浇鍏ョ殑assert鍜宎ssertbundle
    public void ClearAllAsset()
    {
        Debug.Log("ResourceManager ClearAllAsset");
        //foreach (KeyValuePair<string, AssetWrapper> kv in _assetCache)
        //{
        //    //Resources.UnloadAsset(kv.Value.GetAsset());
        //    Object.Destroy(kv.Value.GetAsset());
        //}
        m_assetWrapperCache.Clear();

        //鑷?姩閿姣佸叏閮ㄧ殑璧勬簮
        foreach (KeyValuePair<string, BundleWrapper> kv in m_bundleCache)
        {
            kv.Value.GetBundle().Unload(true);
        }
        m_bundleCache.Clear();
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 鍐呴儴鍔犺浇assetbundle鐨兲
    /// </summary>
    BundleWrapper LoadBundle(string abName)
    {
        string target_local_path = GetLocalFileUrl(abName);
		if(!System.IO.File.Exists(target_local_path))
		{
			// 鏈?湴鍖栬祫婧愭湭鎵惧埌渚濊禆鍖咃紝鍘诲叕鍏卞寘鐩?綍鎵教
			if(m_isLocalization)
			{
				SetLocalization(false);
				target_local_path = GetLocalFileUrl(abName);
				SetLocalization(true);
			}
			else 
			{
				return null;
			}
		}
        byte[] target_content = System.IO.File.ReadAllBytes(target_local_path);
        AssetBundle target_bundle = AssetBundle.LoadFromMemory(target_content);
        //AssetBundle target_bundle = AssetBundle.CreateFromFile(target_local_path);
        Assert.IsNotNull<AssetBundle>(target_bundle, "assetbundle璧勬簮涓嶅瓨鍦?" + abName);
        BundleWrapper target_bundle_bw = new BundleWrapper(abName, target_bundle);
        m_bundleCache.Add(abName, target_bundle_bw);
        return target_bundle_bw;
    }
    // 閿姣佽祫婧徧
	void DestroyAsset(AssetWrapper asset)
	{
        string assetPath = asset.GetAssetPath();
        string[] dps = asset.GetDps();
        string mainBundle = asset.GetBundleName();
        if (m_bundleCache.ContainsKey(mainBundle))
        {
            m_bundleCache[mainBundle].DecRefAsset(assetPath);
            if (dps != null)
            {
                for (int i = 0; i < dps.Length; i++)
                {
                    string dep_name = dps[i];
                    if (m_bundleCache.ContainsKey(dep_name))
                    {
                        m_bundleCache[dep_name].DecRefAsset(assetPath);
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("AssetWrapper :" + assetPath +" has no bundle:" + mainBundle + " in m_bundleCache");
        }
        //鑰冭檻姣忚繃涓娈垫椂闂村垽鏂?繖浜汢undles鏄?惁宸茬粡娌℃湁琚?娇鐢?紝鐒跺悗鐩存帴閲婃斁
        //m_bundleCache.Remove();
	}

	void Awake() 
	{

	}
	
	void OnDestroy() 
	{
        m_manifest = null;
        if(m_ab!=null)
            m_ab.Unload(true);

        m_manifestL10n = null;
        if(m_abL10n!= null)
            m_abL10n.Unload(true);
        ClearAllAsset();
#if UNITY_EDITOR
        UnityEditor.EditorUtility.UnloadUnusedAssetsImmediate();
#endif
		Debugger.Log("~ResourceManager was destroy!");
    }

    #region 鏆傛椂搴熷純浠ｇ爜
    // 纭??寮傛?鍔犺浇缁撴灉
    public AssetWrapper CheckAsyncResult(string asset_path)
    {
        return null;
    }
    // 寮傛?鍔犺浇
    public IEnumerator LoadAssetAsync(string asset_path, System.Type asset_type)
    {
        if (!Application.isEditor && !_asset2Bundle.ContainsKey(asset_path))
            yield break;

        // see if there's a cache shot
        if (m_assetWrapperCache.ContainsKey(asset_path))
        {
            m_assetWrapperCache[asset_path].AddRef();
            yield break;
        }
        if (!Application.isEditor)
        {
            //yield return StartCoroutine(LoadFromBundleAsync(asset_path, asset_type));
            yield return StartCoroutine(LoadFromLocalAsync(asset_path, asset_type));
        }
        else
        {
            yield return StartCoroutine(LoadFromLocalAsync(asset_path, asset_type));
        }
    }

    // 寮傛?鍔犺浇鏈?湴璧勬簮
    IEnumerator LoadFromLocalAsync(string asset_path, System.Type asset_type)
    {
        yield return new WaitForEndOfFrame();
    }

    // 寮傛?鍔犺浇bundle璧勬簮
    IEnumerator LoadFromBundleAsync(string asset_path, System.Type asset_type)
    {
        yield return new WaitForEndOfFrame();
    }
    #endregion //鏆傛椂搴熷純

    #region 娣诲姞Shader涓㈠け鐨勫?鐞嗕唬鐮?
    static public void excuteShader(GameObject _gameobject)
    {
        Renderer[] renders = _gameobject.transform.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer rd in renders)
        {
            if (rd != null && rd.sharedMaterial != null)
            {
                //if (rd.sharedMaterial.shader.isSupported == false)
                {
                    //Debugger.Log("Not Support mat:" + rd.sharedMaterial.name + ",shader:" + rd.sharedMaterial.shader.name);
                    rd.sharedMaterial.shader = Shader.Find(rd.sharedMaterial.shader.name);
                }
                //Debug.Log("@@@@@@@Out put shareMaterial Name:" + rd.sharedMaterial.name);
            }
        }
    }
    #endregion
}
