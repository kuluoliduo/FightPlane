using UnityEngine;
using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ClashGame
{
    public class UIWndData
    {
        public string WndName;      //窗口类名
        public string PrefabPath;   //窗口对应的prefab路径
        public bool Mutex;          //是否和其它窗口有互斥关系（打开本窗口时关闭其它所有有互斥关系的窗口）
        public bool Model;          //是否模态
        public bool BlurEffect;     //打开窗口时是否开启背景模糊
        public bool CanCharMove;    //窗口打开时人物可否移动，1是可移动
        public bool WndAnimation;   //窗口动画，1是可有动画
    }

    public class DBUIWnd
    {
        public Dictionary<int, UIWndData> m_dicUIWnd = new Dictionary<int, UIWndData>();

        public Dictionary<int, UIWndData> GetUIWndData()
        {
            return m_dicUIWnd;
        }

        public string GetUIWndPrefabPath(string strName)
        {
            foreach (UIWndData data in m_dicUIWnd.Values)
            {
                if (strName.Equals(data.WndName))
                {
                    return data.PrefabPath;
                }
            }
            return null;
        }

        public bool GetUIWndDataByID(int nID, out UIWndData data)
        {
            return m_dicUIWnd.TryGetValue(nID, out data);
        }

        public bool LoadData(string strFilename)
    {
        Debuger.Log("dbuiwnd LoadData::" + strFilename + "/" + m_dicUIWnd.Count);
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset textAsset = (TextAsset)Resources.Load(strFilename, typeof(TextAsset));
        //GameRes res = new GameRes(strFilename);
        //res.m_eCatchType = GameRes.eCatchType.CT_None;
        //res = ResourcesManager.LoadObj(res);
        //TextAsset textAsset = (TextAsset)res.m_Object;
        if (textAsset == null)
        {
            Debuger.LogError("Load UIWnd DB Failed!");
            return false;
        }
        xmlDoc.Load(new StringReader(textAsset.text));
        XmlElement xmlRoot = xmlDoc.DocumentElement;
        //Debuger.Log("1111111111111111111111111111");
        XmlNodeList nodeList = xmlRoot.ChildNodes;
        foreach (XmlNode node in nodeList)
        {
            if (!(node is XmlElement))
            {
                Debuger.LogError("UIWnd have a node which is not a XmlElement!");
                continue;
            }

            XmlElement xmlElem = (XmlElement)node;

            int nID = XmlConvert.ToInt32(xmlElem.GetAttribute("ID"));
            UIWndData data = new UIWndData();

            data.WndName = xmlElem.GetAttribute("Name");
            data.PrefabPath = xmlElem.GetAttribute("PrefabPath");
            data.Mutex = XmlConvert.ToBoolean(xmlElem.GetAttribute("Mutex"));
            data.Model = XmlConvert.ToBoolean(xmlElem.GetAttribute("Model"));
            data.BlurEffect = XmlConvert.ToBoolean(xmlElem.GetAttribute("BlurEffect"));
            data.CanCharMove = XmlConvert.ToBoolean(xmlElem.GetAttribute("CanCharMove"));
            data.WndAnimation = XmlConvert.ToBoolean(xmlElem.GetAttribute("WndAnimation"));
            // 对于重复配置的，用后面的覆盖前面的
            if (m_dicUIWnd.ContainsKey(nID))
            {
                if (data.WndName != "LoadingWnd")
                    Debuger.LogWarning("<Wnd Config Warning!> Two wnd have the same name: " + data.WndName);
                m_dicUIWnd.Remove(nID);
            }
            m_dicUIWnd.Add(nID, data);
        }
        //Debuger.Log("2222222222222222222222222222222222222");
        Debuger.Log("dbuiwnd LoadData::" + strFilename + "/" + m_dicUIWnd.Count);
        return true;
    }
    }
}
