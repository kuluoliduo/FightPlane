using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

class RoleInfo {
    public readonly List<string> commonTalk = new List<string>();
   
    public readonly Dictionary<string, bool> channelVoiceSetting = new Dictionary<string, bool>();
}
class CacheUserInfo {
    public string UserName { get; set; }
    public bool AudioActive { get; set; }
    public bool MusicActive { get; set; }

    public readonly Dictionary<string, RoleInfo> roleInfoDict = new Dictionary<string, RoleInfo>();
    public CacheUserInfo() {
        AudioActive = true;
        MusicActive = true;
        //isShowFirstBattleCG = false;
    }
    public string ToJson() {
        string data = JsonMapper.ToJson(this);
        return data;
    }
    public static CacheUserInfo ToObject(string data) {
        CacheUserInfo obj = new CacheUserInfo();
        obj = JsonMapper.ToObject<CacheUserInfo>(data);
        return obj;
    }
}

class RoleSkillInfo {
    public bool AutoBattle { get; set; }
    public int PetID { get; set; }
} 

class CacheUseSkillInfo {
    public readonly Dictionary<string, RoleSkillInfo> roleSkillInfo = new Dictionary<string, RoleSkillInfo>();
    public string ToJson() {
        string data = JsonMapper.ToJson(this);
        return data;
    }
    public static CacheUseSkillInfo ToObject(string data) {
        CacheUseSkillInfo obj = new CacheUseSkillInfo();
        obj = JsonMapper.ToObject<CacheUseSkillInfo>(data);
        return obj;
    }
}
class ItemFlagInfo {
    public long itemInitId { get; set; }
    public int itemId { get; set; }
    public bool isJustGot { get; set; }
    public bool isJuseIdentified { get; set; }
}

class CacheItemFlagInfo {
    public readonly Dictionary<string, ItemFlagInfo> itemInfoDic = new Dictionary<string, ItemFlagInfo>();
    public string ToJson() {
        string data = JsonMapper.ToJson(this);
        return data;
    }
    public static CacheItemFlagInfo ToObject(string data) {
        CacheItemFlagInfo obj = new CacheItemFlagInfo();
        obj = JsonMapper.ToObject<CacheItemFlagInfo>(data);
        return obj;
    }
}

class FileManager {
    private string m_fileName;
    private FileInfo m_fileInfo;
    public FileManager(string name) {
        m_fileName = Application.persistentDataPath + '/' + name;
        m_fileInfo = new FileInfo(m_fileName);
    }

    public string Read() {
        StreamReader streRead = null;
        if (m_fileInfo.Exists) {
            try {
                streRead = File.OpenText(m_fileName);
            }
            catch {
                return "";
            }
            string txt = "";
            txt = streRead.ReadToEnd();
            streRead.Close();
            streRead.Dispose();
            return txt;
        }
        return "";
    }

    public void Write(string txt) {
        StreamWriter streWrite;
        if (m_fileInfo.Exists) {
            streWrite = m_fileInfo.AppendText();
        }
        else {
            streWrite = m_fileInfo.CreateText();
        }
        streWrite.WriteLine(txt);
        streWrite.Close();
        streWrite.Dispose();
    }

    public void Delete() {
        if (m_fileInfo.Exists) {
            File.Delete(m_fileName);
        }
    }
}

class CacheManager {
    private static CacheManager m_instance;
    private CacheUserInfo m_userInfo;
    private CacheUseSkillInfo m_userskillInfo;
    private CacheItemFlagInfo m_itemFlagInfo;
    public const string CACHE_USER_INFO_FILE = "cacheInfo.txt";
    public const string CACHE_USER_SKILL_INFO_FILE = "cacheskillInfo.txt";
    public const string CACHE_ITEM_FLAG_INFO_FILE = "itemFlagInfo.txt";
    public const string VERSION_INFO_FILE = "version.txt";
    private CacheManager() {
        Init();
    }

    private void Init() {
        FileManager file = new FileManager(CACHE_USER_INFO_FILE);
        string userJson = file.Read();
        if (string.IsNullOrEmpty(userJson)) {
            m_userInfo = new CacheUserInfo();
        }
        else {
            m_userInfo = CacheUserInfo.ToObject(userJson);
        }

        FileManager skillfile = new FileManager(CACHE_USER_SKILL_INFO_FILE);
        string userskillJson = skillfile.Read();
        if (string.IsNullOrEmpty(userskillJson)) {
            m_userskillInfo = new CacheUseSkillInfo();
        }
        else {
            m_userskillInfo = CacheUseSkillInfo.ToObject(userskillJson);
        }
        FileManager itemfile = new FileManager(CACHE_ITEM_FLAG_INFO_FILE);
        string itemFlagJson = itemfile.Read();
        if (string.IsNullOrEmpty(itemFlagJson)) {
            m_itemFlagInfo = new CacheItemFlagInfo();
        }
        else {
            m_itemFlagInfo = CacheItemFlagInfo.ToObject(itemFlagJson);
        }
    }

    public static CacheManager GetInstance() {
        if (m_instance == null) {
            m_instance = new CacheManager();
        }
        return m_instance;
    }

    public void SetUserLoginInfo(CacheUserInfo info) {
        m_userInfo = info;
    }

    public void SaveCache() {
        FileManager file = new FileManager(CACHE_USER_INFO_FILE);
        file.Delete();
        file.Write(m_userInfo.ToJson());
    }

    public void SaveSkillCache() {
        FileManager file = new FileManager(CACHE_USER_SKILL_INFO_FILE);
        file.Delete();
        file.Write(m_userskillInfo.ToJson());
    }
    public void SaveItemCache() {
        FileManager file = new FileManager(CACHE_ITEM_FLAG_INFO_FILE);
        file.Delete();
        file.Write(m_itemFlagInfo.ToJson());
    }
    public CacheItemFlagInfo GetCacheItemInfo() {
        return m_itemFlagInfo;
    }

    public CacheUseSkillInfo GetCacheSkillInfo() {
        return m_userskillInfo;
    }

    public CacheUserInfo GetCacheInfo() {
        return m_userInfo;
    }




    public void AddUseSkillInfo(RoleSkillInfo info,string key) {
        CacheUseSkillInfo dict = GetCacheSkillInfo();
        if (dict != null) {
            if (dict.roleSkillInfo.Count > 30) {
                string first = string.Empty;
                var iter = dict.roleSkillInfo.GetEnumerator();
                while (iter.MoveNext()) {
                    first = iter.Current.Key;
                    break;
                }
                iter.Dispose();
                if (!string.IsNullOrEmpty(first)&&dict.roleSkillInfo.ContainsKey(first)) {
                    dict.roleSkillInfo.Remove(first);
                }
            }
            dict.roleSkillInfo.Add(key, info);
        }
    }
    /// <summary>
    /// 有标记的存，没标记的删
    /// </summary>
    /// <param name="itemFlag"></param>
    public void ChangeItemFlagInfo(long initId, int id, EItemFlagType flagType, bool flagValue) {
        CacheItemFlagInfo info = GetCacheItemInfo();
        string strInitId = initId.ToString();
        if (info != null) {
            if (info.itemInfoDic.ContainsKey(strInitId)) {
                if (flagType == EItemFlagType.JustGot) {
                    info.itemInfoDic[strInitId].isJustGot = flagValue;
                }
                if (flagType == EItemFlagType.JustIdentified) {
                    info.itemInfoDic[strInitId].isJuseIdentified = flagValue;
                }
                //没标记的删
                if ( (!info.itemInfoDic[strInitId].isJustGot) && (!info.itemInfoDic[strInitId].isJuseIdentified) ) {
                    info.itemInfoDic.Remove(strInitId);
                }
            }
            else {
                if (flagValue) {
                    ItemFlagInfo flag = new ItemFlagInfo();
                    flag.itemInitId = initId;
                    flag.itemId = id;
                    if (flagType == EItemFlagType.JustGot) {
                        flag.isJustGot = flagValue;
                        flag.isJuseIdentified = false;
                    }
                    if (flagType == EItemFlagType.JustIdentified) {
                        flag.isJustGot = false;
                        flag.isJuseIdentified = flagValue;
                    }
                    info.itemInfoDic.Add(strInitId, flag);
                    //Debug.LogError(flag.itemInitId);
                }
                else {
                    Debug.LogWarning("meaningless operation!!");
                }
            }
        }
        SaveItemCache();
    }
    public void DeleteItemFlagInfo(long initId) {
        CacheItemFlagInfo info = GetCacheItemInfo();
        if (info != null) {
            info.itemInfoDic.Remove(initId.ToString());
        }
        SaveItemCache();
    }

}
public enum EItemFlagType {
    JustGot,
    JustIdentified
}

