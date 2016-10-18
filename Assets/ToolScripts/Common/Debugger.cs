using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class Debugger
{
    public static bool needAIMsg = false;
    public static bool needSaveFile = false;

    public static Dictionary<uint, List<string>> aiMsgDic = null;

    public static void Log(string str, params object[] args)
    {
		if(args.Length > 0){
	        str = string.Format(str, args);
		}
        Debug.Log(str);
    }

    public static void LogWarning(string str, params object[] args)
    {
        str = string.Format(str, args);
        Debug.LogWarning(str);
    }

    public static void LogError(string str, params object[] args)
    {
        str = string.Format(str, args);
        Debug.LogError(str);
    }
    /*
    public static void PrintAIMsg(uint guid, string str)
    {
        if (needAIMsg)
        {
            Debug.Log(string.Format("[---AILog--]{0}:{1} {2}", FightTime.frameCount, Time.time.ToString(), str));
        }

        if(needSaveFile)
        {
            if(aiMsgDic == null)
            {
                aiMsgDic = new Dictionary<uint, List<string>>();
            }

            if(!aiMsgDic.ContainsKey(guid))
            {
                aiMsgDic.Add(guid, new List<string>());
            }

            aiMsgDic[guid].Add(string.Format("[---AILog--]{0}:{1} {2}\n", FightTime.frameCount, Time.time.ToString(), str));
        }
    }

    public static void SaveAIMsgFile()
    {
        if(needSaveFile)
        {
            foreach(KeyValuePair<uint, List<string>> kvp in aiMsgDic)
            {
                //保存的文件名是：编号(可能是玩家或者联盟编号)_当前时间戳_场景名;
                //保存的路径是 Application.persistentDataPath/records/;
                string filePath = string.Format("{0}/AIRecord", Application.persistentDataPath);
                if (Directory.Exists(filePath) == false)
                {
                    Directory.CreateDirectory(filePath);
                }
                string filePathName = string.Format("{0}/{1}.txt", filePath, kvp.Key);
                if (File.Exists(filePathName))
                {
                    File.Delete(filePathName);
                }

                StreamWriter writer = new StreamWriter(filePathName, false, Encoding.UTF8);
                
                for (int i = 0; i < kvp.Value.Count; ++i)
                {
                    writer.WriteLine(kvp.Value[i]);
                }

                writer.Close();
                writer = null;
            }
        }
    }
     * */
}
