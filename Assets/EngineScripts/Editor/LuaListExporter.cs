using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
public static class LuaListExporter
{
	private static string kLuaRootPath = Application.dataPath + "/Lua";
	private static string kLuaStreamPath = Application.streamingAssetsPath + "/lua";

	[MenuItem("S6/Lua相关/4. 导出Lua文件列表", false, 50)]
	static void ExportLuaList()
	{
		if (System.IO.Directory.Exists(kLuaStreamPath))
		{
			FileUtil.DeleteFileOrDirectory(kLuaStreamPath);
		}
		System.IO.Directory.CreateDirectory (kLuaStreamPath);

		LuaList ll = new LuaList();
		string[] allLuaList = System.IO.Directory.GetFiles(kLuaRootPath, "*", System.IO.SearchOption.AllDirectories);

		for(int i = 0; i < allLuaList.Length; i++)
		{
			string filePath = allLuaList[i];
			if (filePath.EndsWith(".meta")) continue;
            if (filePath.Contains(".svn")) continue;
			if (filePath.Contains(".bak")) continue;
			if (filePath.Contains(".bat")) continue;
			if (filePath.Contains(".txt")) continue;
			if (filePath.Contains(".DS_Store")) continue;
			string relaPath = FileUtil.GetProjectRelativePath(filePath).Replace("\\", "/").Substring(11);
			string fileMd5 = UtilCommon.md5file(filePath);

			ll.LuaFileMD5Data.Add(relaPath, fileMd5);

			string to_path = filePath.Replace(kLuaRootPath, kLuaStreamPath);
			string to_path_name = System.IO.Path.GetDirectoryName(to_path);
			if (!System.IO.Directory.Exists(to_path_name))
			{
				System.IO.Directory.CreateDirectory (to_path_name);
			}
			FileUtil.CopyFileOrDirectoryFollowSymlinks(filePath, to_path);
		}
		//ll.WriteToFile(kLuaRootPath + "/LuaFileList.json");
		ll.WriteToFile (kLuaStreamPath + "/LuaFileList.json");
	}
}
