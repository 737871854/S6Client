/**
 * Copyright (c) 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：VersionManager.cs
 * 简	述：更新所有资源，核对版本。
 * 作	者：Fanki
 * 创建标识：2015/08/24
 */

using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Qtz.Q5.Version
{
	public class VersionManager
	{
		#region 单例模式
		static private VersionManager _instance;
		public static VersionManager Instance
        {
            get
            {
				if (_instance == null)
                {
					_instance = new VersionManager();
                }
				return _instance;
            }
        }

		private VersionManager()
        {

        }
		#endregion //单例模式

		#region 私有属性
		/// <summary>
		/// gamemanager管理保存
		/// </summary>
		private GameManager _gameManager;

		/// <summary>
		/// loadingScene
		/// </summary>
		//private LoadingScene _loadingScene;

		/// <summary>
		/// 版本控制下载方案
		/// </summary>
		private VersionControlBase _versionControl;

		/// <summary>
		/// 配置先要初始化
		/// </summary>
		private PkgConfig _pkgConfig;
		#endregion //私有变量

		#region 资源更新 Fanki 2015-08-12 更新资源效验
		/// <summary>
		/// 获得控制方案
		/// </summary>
		/// <param name="controltype"></param>
		protected void InitControlByType(VersionControlType controltype)
		{
			switch(controltype)
			{
				case VersionControlType.kApp:
                    _versionControl = UtilCommon.AddComponent<VersionControlApp>(_gameManager.gameObject) as VersionControlApp;
					break;
				case VersionControlType.kDev:
                    _versionControl = UtilCommon.AddComponent<VersionControlDev>(_gameManager.gameObject) as VersionControlDev;
					break;
				case VersionControlType.kArt:
                    _versionControl = UtilCommon.AddComponent<VersionControlArt>(_gameManager.gameObject) as VersionControlArt;
					break;
				default:
                    _versionControl = UtilCommon.AddComponent<VersionControlApp>(_gameManager.gameObject) as VersionControlApp;
					break;
			}
		}

		/// <summary>
		/// 保存gamemanager，开始init
		/// </summary>
		/// <param name="ins"></param>
		public void StartInitResources(GameManager ins)
		{
			_gameManager = ins;
            _pkgConfig = UtilCommon.AddComponent<PkgConfig>(_gameManager.gameObject) as PkgConfig;
			_pkgConfig.SetCallback(InitVersionContrl);
			_pkgConfig.OnStartDownload();
		}

		/// <summary>
		/// 开启下载方案
		/// </summary>
		/// <param name="isSucess"></param>
		/// <param name="msg"></param>
		protected void InitVersionContrl(bool isSucess, string msg)
		{
			// 方便开发者
			VersionControlType contype = Application.isEditor ? VersionControlType.kDev : (VersionControlType)Config.Instance.GetVersionControlType();

			// 初始化控制方案
			InitControlByType(contype);
			_versionControl.Init(_gameManager);
			_versionControl.StartUpdate();
		}

		/// <summary>
		/// 开始游戏的回调
		/// </summary>
		public void OnResourceInited()
		{
			//_loadingScene.SetPercentage(1.0f);
			//_loadingScene = null;

			// 初始化资源运行
			if (_gameManager)
				_gameManager.OnResourceInited();
			DestroyFunc();
		}

		/// <summary>
		/// 销毁
		/// </summary>
		public void DestroyFunc()
		{
			_pkgConfig.DestroyPackage();
			_versionControl.Destroy();
			_instance = null;
		}
		#endregion //更新资源效验
	}
}
