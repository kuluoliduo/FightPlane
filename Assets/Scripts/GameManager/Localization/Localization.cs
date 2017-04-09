using UnityEngine;
using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using ClashGame;

public class Localization : MonoBehaviour {

    static Localization mInst;
    static public Localization mInstance
    {
        get
        {
            if(mInst == null)
            {
                GameObject go = new GameObject("_Localization");
                DontDestroyOnLoad(go);
                mInst = go.AddComponent<Localization>();
            }
            return mInst;
        }
    }

    /// <summary>
    /// 所有语言名的集合,用英文标识的
    /// </summary>
    private List<string> listLanguageName = new List<string>();

    /// <summary>
    /// 语言名对应的本地化名
    /// </summary>
    Dictionary<string, string> dicLanguageName = new Dictionary<string, string>();

    Dictionary<string, string> dicLanguageFont = new Dictionary<string, string>();

    /// <summary>
    /// 选择某个语言后存储所有本地化文档
    /// </summary>
    Dictionary<string, Dictionary<string, string>> dicLocalLanguages = new Dictionary<string, Dictionary<string, string>>();

    string curLanguage = "Chinese";
    Font ReferenceFont;
    Font ReplacementFont;

    /// <summary>
    /// Name of the currently active language.
    /// </summary>
    public string currentLanguage
    {
        get
        {
            if (string.IsNullOrEmpty(curLanguage))
            {
                currentLanguage = "Chinese"; 
             
            }
            return curLanguage;
        }
        set
        {
           
        }
    }

    void Awake()
    {
        if (mInst == null)
        {
            mInst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

       
        //LoadFont(curLanguage);
   
        dicLocalLanguages.Clear();
    }

    public void PreInit()
    {
        string name = "GameStartLocal";
        string strPath = "LocalRes/DB/Localization/" + curLanguage + "/" + name;
        Debuger.Log(strPath);
        LoadFile(name, strPath);
    }

    public void init()
    {
        Debuger.Log("LoadAllLocalizations----" + curLanguage);
        LoadAllLocalizations(curLanguage);
    }

    /// <summary>
    /// Oddly enough... sometimes if there is no OnEnable function in Localization, it can get the Awake call after UILocalize's OnEnable.
    /// </summary>
    void OnEnable() { if (mInst == null) mInst = this; }

    /// <summary>
    /// Remove the instance reference.
    /// </summary>
    void OnDestroy() { if (mInst == this) mInst = null; }

    /// <summary>
    /// 得到相应文件中key对应的值
    /// </summary>
    public string GetText(string filename, int key)
    {
        if (dicLocalLanguages.ContainsKey(filename))
        {
            string val = null;
            dicLocalLanguages[filename].TryGetValue(key.ToString(), out val);
            if (val == null)
            {
                Debuger.LogWarning("localization filename key not found!" + filename + " " + key);
                return "no string";
            }
            return val;
        }
        else
        {
            Debuger.LogWarning("localization filename not found!" + filename);
        }
        return "no string";
    }

    /// <summary>
    /// 得到相应文件中key对应的值
    /// </summary>
    public string GetText(string filename, string key)
    {
        if (dicLocalLanguages.ContainsKey(filename))
        {
            string val = null;
            dicLocalLanguages[filename].TryGetValue(key, out val);
            if (val == null)
            {
                Debuger.LogWarning("localization filename key not found!" + filename + " " + key);
                return "no string";
            }
            return val;
        }
        else
        {
            Debuger.LogWarning("localization filename not found!" + filename);
        }
        return "no string";
    }


    /// <summary>
    /// 得到语言名的本地化名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetLocalizeLangName(string name)
    {
        string val;
        return (dicLanguageName.TryGetValue(name, out val)) ? val : null;
    }

    /// <summary>
    /// 根据本地化名得到语言名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetLangNameByLocalizeName(string name)
    {
        foreach (KeyValuePair<string, string> pair in dicLanguageName)
        {
            if (string.Equals(name, pair.Value))
            {
                return pair.Key;
            }
        }
        return null;
    }

    /// <summary>
    /// 根据语言名得到对应的字体名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetFontNameByLanguage(string name)
    {
        string key = GetLangNameByLocalizeName(name);
        string val = "";
        if (!string.IsNullOrEmpty(key))
        {
            dicLanguageFont.TryGetValue(key, out val);
        }
        return val;
    }

    /// <summary>
    /// 得到文件夹下所有语言名，经测试ipad不能搜索目录
    /// </summary>
    //void GetListLanguageName()
    //{
    //    DirectoryInfo folder = new DirectoryInfo("Assets/Resources/DB/Localization");
    //    foreach (DirectoryInfo next in folder.GetDirectories())
    //    {
    //        listLanguageName.Add(next.Name);
    //        //Debuger.LogWarning("GetListLanguageName name" + next.Name);
    //    }
    //}

    /// <summary>
    /// 必须放在LoadLanguageName后
    /// </summary>
    /// <param name="language"></param>
    //void LoadFont(string language)
    //{
    //    Debuger.Log("LoadFont==" + language);
    //    ReplacementFont = Resources.Load("UI/font/ChineseFont26", typeof(Font)) as Font;
       
    //    if (ReferenceFont == null)
    //    {
    //        ReferenceFont = Resources.Load("UI/font/Font_3_7", typeof(Font)) as Font;
    //    }

    //    ReferenceFont.replacement = ReplacementFont;
    //}

    /// <summary>
    /// 加载和某个语言相关的所有本地化文档
    /// </summary>
    /// <param name="language"></param>
    void LoadAllLocalizations(string language)
    {

        //string file = "DB/Localization/" + curLanguage;
        //XmlElement rootEle = CXmlRead.GetRootElement("DB/Localization/LocalizationFileName");
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset textAsset = (TextAsset)Resources.Load("NetRes/DB/Localization/LocalizationFileName", typeof(TextAsset));
        //GameRes res = new GameRes("NetRes/DB/Localization/LocalizationFileName", GameRes.eCatchType.CT_None);
        //res.m_eCatchType = GameRes.eCatchType.CT_None;
        //res = ResourcesManager.LoadObj(res);
        //TextAsset textAsset = (TextAsset)res.m_Object;
        if (textAsset == null)
        {
            Debuger.LogError("Load LoadLanguageName DB Failed!");
            return;
        }
        //Debuger.Log("DB/Localization/LocalizationFileName----------------");
        xmlDoc.Load(new StringReader(textAsset.text));
        XmlElement xmlRoot = xmlDoc.DocumentElement;

        foreach (XmlNode node in xmlRoot.ChildNodes)
        {
            if (node.Name == "row")
            {
                CXmlRead xmlRead = new CXmlRead(node as XmlElement);
                string name = xmlRead.Str("name");
                string strPath = "NetRes/DB/Localization/" + curLanguage + "/" + name;
               
                Debuger.Log(strPath);
                LoadFile(name, strPath);
            }
        }
        //UIRoot.Broadcast("OnLocalize", this);
        //OnChangeLanguage();
    }

    void LoadFile(string name, string path)
    {
        Debuger.Log("-----------------------------load file name=" + path);
        Dictionary<string, string> texts = new Dictionary<string, string>();
        // XmlElement rootEle = CXmlRead.GetRootElement(path);

        XmlDocument xmlDoc = new XmlDocument();
        TextAsset textAsset = (TextAsset)Resources.Load(path, typeof(TextAsset));
        //GameRes res = new GameRes(path, GameRes.eCatchType.CT_None);
        //res = ResourcesManager.LoadObj(res);
        //TextAsset textAsset = (TextAsset)res.m_Object;
        if (textAsset == null)
        {
            Debuger.LogError("Load LoadLanguageName DB Failed! path=" + path);
            return;
        }
        xmlDoc.Load(new StringReader(textAsset.text));
        XmlElement xmlRoot = xmlDoc.DocumentElement;

        if (xmlRoot == null)
        {
            Debuger.LogWarning("LoadFile fail:" + path);
            return;
        }
        foreach (XmlNode node in xmlRoot.ChildNodes)
        {
            if (node.Name == "row")
            {
                CXmlRead xmlRead = new CXmlRead(node as XmlElement);
                string id = xmlRead.Str("id");
                string text = xmlRead.Str("text");
                if (texts.ContainsKey(id))
                {
                    Debuger.LogWarning("duplicate record id name:" + name + "id:" + id);
                    continue;
                }
                texts.Add(id, text);
                //Debuger.LogWarning("GetListLanguageName" + text);
            }
        }
        dicLocalLanguages.Add(name, texts);
    }

    /// <summary>
    /// OnLocalize不起作用时调用
    /// </summary>
    //void OnChangeLanguage()
    //{
    //    if (NPCManager.instance != null)
    //    {
    //        //Debuger.LogWarning("OnChangeLanguage____________NPCManager");
    //        NPCManager.GetInstance().OnLocalize();
    //        NPCManager.GetInstance().OnLocalizeBubble();
    //    }                 
    //}
}
