using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using DG.Tweening.Core;

namespace ClashGame
{
    public class GameSeverInfo
    {
        public int      ID   { get; set; }
        public string   Name { get; set; }
        public string   IP   { get; set; }
        public int      Port { get; set; }
        public int      Flag { get; set; }
        public string   IdList { get; set; } 
    }
    public class UIManager : MonoBehaviour
    {
        private string strHttp = "http://192.168.70.39/load.php";
        private Camera m_uiCamera = null;
        private GameObject m_uiAudioGObj = null;
        public bool bWndCloseAnimating = false;
        //public UISrceen m_UISrceen = null;
        public GameObject m_gobjBackGround = null;
        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <returns></returns>
        public void Init()
        {
            Debuger.Log("uimanager init");


            InitDBUIWnd();

            UICanvasMgr.mInstance.InitCanvas();

            //mySequence = DOTween.Sequence();

            //DontDestroyOnLoad(transform);

            if (GameApp.m_UIManager.GetWnd("LoginWnd").IsClose())
            {
                GameApp.m_UIManager.GetWnd("LoginWnd").OpenWnd();
            }
            
          //  StartCoroutine(LoadFromHttp());
        }

        IEnumerator LoadFromHttp()
        {
            WWW www = new WWW(strHttp);
            yield return www;
            Debuger.Log("====================" + www.text);
            JsonToList(www);
        }
        void JsonToList(WWW w)
        {
            //if (string.IsNullOrEmpty(w.text))
            //{
            //    Debuger.LogError("sever list is null!!");
            //    return;
            //}
            //string temp = w.text;
            //jarr = LitJson.JsonMapper.ToObject(w.text);
            //if(jarr.IsArray)
            //{
            //    for(int i = 0; i < jarr.Count; i++)
            //    {
            //        Debuger.Log(jarr[i]["name"]);
            //    }
            //    LoginWnd wnd = GameApp.m_UIManager.GetWnd("LoginWnd") as LoginWnd;
            //    if(wnd.IsOpen())
            //    {
            //        wnd.ShowDefaultSever();
            //    }
            //}
           

        }

        void Update()
        {
            foreach (UIBaseWnd wnd in UIBaseWnd.DicWnd.Values)
            {
                if (wnd.WndRootGameobject != null)
                {
                    wnd.Update();
                }
            }
            //if (m_UISrceen != null)
            //    m_UISrceen.Tick();
        }

        public Camera uiCamera
        {
            get
            {
                if (m_uiCamera == null)
                {
                    m_uiCamera = UICanvasMgr.mInstance.GetUIRoot().transform.Find("UICamera").GetComponent<Camera>();
                }
                return m_uiCamera;
            }
        }

        public GameObject AddUI(GameObject Prefab, UICanvasMgr.eUICanvasLevel level) { return AddUI(Prefab, null, Vector3.zero, level); }

        /// <summary>
        /// 添加主UICamera接管的UI GameObject
        /// </summary>
        /// <param name="Prefab">需要由主UICamera接管的GameObject</param>
        /// <param name="target">该UI GameObject跟随的对象</param>
        /// <returns>返回创建的UI GameObject</returns>
        /// Ex:GameApp.m_UIManager.AddUI(Resources.Load("UI/Login/Prefabs/uitest")as GameObject, transform);
        public GameObject AddUI(GameObject Prefab, Transform target, Vector3 OffsetV3, UICanvasMgr.eUICanvasLevel level)
        {
            if (Prefab == null)
            {
                Debuger.Log("AddUI(Prefab is null)");
                return null;
            }


            GameObject goGetCanvasRoot = null;
            goGetCanvasRoot = UICanvasMgr.mInstance.GetUICanvaRootsByLevel(level);

            if (goGetCanvasRoot != null)
            {
                GameObject child = GameObject.Instantiate(Prefab) as GameObject;
                if (child == null) return null;
                child.layer = goGetCanvasRoot.layer;

                Transform t = child.transform;
                t.SetParent(goGetCanvasRoot.transform);
                if (target != null)
                {
                    AddHUDToTarget hud = child.GetComponent<AddHUDToTarget>();
                    if (hud == null)
                    {
                        hud = child.AddComponent<AddHUDToTarget>();
                    }
                    hud.target = target;
                    hud.offSet = OffsetV3;
                }
                else
                {
                    t.localPosition = Vector3.zero;
                }
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;

                if (UICanvasMgr.mInstance.UIAddedDic.ContainsKey((int)level))
                {
                    UICanvasMgr.mInstance.UIAddedDic[(int)level].Add(child);
                }
                else
                {
                    List<GameObject> list = new List<GameObject>();
                    list.Add(child);
                    UICanvasMgr.mInstance.UIAddedDic.Add((int)level, list);
                }

                return child;
            }

            return null;
        }

        /// <summary>
        /// 移动UI GameObject到指定目标
        /// </summary>
        /// <param name="goUI">目标UI</param>
        /// <param name="target">目标点</param>
        /// <returns>true，移动成功</returns>
        public bool MoveUIAdded(GameObject goUI, Transform target)
        {

            if (uiCamera != null && goUI != null)
            {

                if (target != null)
                {
                    //AddHUDToTarget hud = goUI.GetComponent<AddHUDToTarget>();
                    //if (hud == null)
                    //{
                    //    hud = goUI.AddComponent<AddHUDToTarget>();
                    //}
                    //if (hud != null) hud.target = target;
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除创建的UI GameObject
        /// </summary>
        /// <param name="goUI"></param>
        /// <returns>true 删除成功</returns>
        public bool DeleteUIAdded(GameObject goUI)
        {


            foreach (List<GameObject> list in UICanvasMgr.mInstance.UIAddedDic.Values)
            {
                foreach (GameObject goTarget in list)
                {
                    if (GameObject.Equals(goTarget, goUI))
                    {
                        list.Remove(goTarget);
                        GameObject.Destroy(goTarget);
                        break;
                    }
                }
            }


            return false;
        }

        #region UIBaseWnd
        public void InitDBUIWnd()
        {
            Debuger.Log("DBManager.GetDBUIWnd().GetUIWndData().Count" + DBManager.m_DBUIWnd.GetUIWndData().Count);
            foreach (UIWndData data in DBManager.m_DBUIWnd.GetUIWndData().Values)
            {
               // Debuger.Log("DBManager.m_DBUIWnd.GetUIWndData().Count====" + DBManager.m_DBUIWnd.GetUIWndData().Count);
                //Debuger.Log("data.WndName====" + data.WndName);
                UIBaseWnd baseWnd = Assembly.GetExecutingAssembly().CreateInstance("ClashGame." + data.WndName) as UIBaseWnd;

                if (baseWnd != null)
                {
                    // 后面的覆盖前面的
                    if (UIBaseWnd.DicWnd.ContainsKey(data.WndName))
                        UIBaseWnd.DicWnd.Remove(data.WndName);

                    baseWnd.Init(data);
                    UIBaseWnd.DicWnd.Add(data.WndName, baseWnd);
                }

            }
        }

        public void OpenWnd(string strWndName)
        {
            UIBaseWnd wnd = UIBaseWnd.GetWndByName(strWndName);
            if (wnd == null)
            {
                Debuger.LogError("Open Window " + strWndName + "fail!");
                return;
            }

            //if (GameApp.GetGuideManager() != null)
            //    GameApp.GetGuideManager().OpenWindow(strWndName);
            wnd.OpenWnd();
        }

        public void CloseWnd(string strWndName)
        {
            UIBaseWnd wnd = UIBaseWnd.GetWndByName(strWndName);
            if (wnd == null)
            {
                Debuger.LogError("Close Window " + strWndName + "fail!");
                return;
            }
            //Debuger.Log("CloseWnd " + strWndName);
            wnd.CloseWnd();

        }

        /// <summary>
        ///关闭除HUD外的所有窗口
        /// </summary>
        public void CloseAllWndExceptHUD(string name)
        {
            foreach (UIWndData data in DBManager.m_DBUIWnd.GetUIWndData().Values)
            {
                UIBaseWnd wnd = UIBaseWnd.GetWndByName(data.WndName);
                if (wnd != null && !string.Equals(data.WndName, "HUD") && !string.Equals(data.WndName, name) && wnd.IsOpen())
                {
                    wnd.CloseWnd();
                }
            }
        }

        public bool OnlyHudOpen()
        {
            UIBaseWnd wnd = UIBaseWnd.GetWndByName("HUD");
            if (!wnd.IsOpen())
            {
                return false;
            }

            foreach (UIBaseWnd data in UIBaseWnd.DicWnd.Values)
            {
                if (data.m_strWndName != "HUD")
                {
                    if (data.IsOpen())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool OnlyHUDOpen()
        {
            bool only = false;
            foreach (UIBaseWnd data in UIBaseWnd.DicWnd.Values)
            {
                if (data.m_strWndName != "HUD")
                {
                    if (data.IsOpen())
                    {
                        only = false;
                    }
                }
                else
                {
                    only = true;
                }
            }
            return only;
        }

        public void CloseAllWnd()
        {
            foreach (UIWndData data in DBManager.m_DBUIWnd.GetUIWndData().Values)
            {
                UIBaseWnd wnd = UIBaseWnd.GetWndByName(data.WndName);
                if (wnd == null)
                {
                    Debuger.LogWarning("CloseAllWnd is error=" + data.WndName);
                }
                else if (wnd.IsOpen())
                {
                    wnd.CloseWnd();
                }
            }
        }

        public UIBaseWnd GetWnd(string strWndName)
        {
            UIBaseWnd wnd = UIBaseWnd.GetWndByName(strWndName);
            if (wnd == null)
            {
                Debuger.LogError("Get Window " + strWndName + "fail!");
                return null;
            }

            return wnd;
        }

        public void SwitchWnd(string strWndName)
        {
            UIBaseWnd wnd = UIBaseWnd.GetWndByName(strWndName);
            if (wnd == null)
            {
                Debuger.LogError("Open Window " + strWndName + "fail!");
                return;
            }

            wnd.SwitchWnd();
        }
        #endregion

      

        public void ActionUISrceen(int fadeID)
        {
            //if(m_UISrceen==null)
            //{
            //    ConstructUISrceen();
            //}
            //if(m_UISrceen!=null)
            //{
            //    m_UISrceen.SetFadeFun(fadeID);
            //}
        }

        //public void ConstructUISrceen()
        //{
            
        //    string dir = "UI/UISrceen/UISrceen";
        //    if (m_UISrceen == null)
        //    {
        //        GameObject m_uiUISrceenPrefab = AddUI(Resources.Load(dir) as GameObject, UICanvasMgr.eUICanvasLevel.Level_3);
        //        m_uiUISrceenPrefab.GetComponent<RectTransform>().localPosition = new Vector3(0, 0,0);
        //        m_uiUISrceenPrefab.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, 0);             
        //        m_UISrceen = new UISrceen();
        //        m_UISrceen.Init(m_uiUISrceenPrefab.transform.Find("Image").GetComponent<Image>() as Image);
        //    }
        //}

        public void ShowBackGround(bool choice)
        {
            Debuger.Log("ShowBackGround" + choice);
            if (m_gobjBackGround == null)
            {
                m_gobjBackGround = GameApp.m_UIManager.AddUI(Resources.Load("NetRes/UI/Wnd/MainHUD/BackGround") as GameObject, UICanvasMgr.eUICanvasLevel.Level_2);
                m_gobjBackGround.GetComponent<RectTransform>().localPosition = Vector3.zero;
                m_gobjBackGround.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                Vector3 originalScale = m_gobjBackGround.transform.localScale;
                m_gobjBackGround.transform.localScale = Vector3.one;
            }
            m_gobjBackGround.SetActive(choice);
            Debuger.Log("m_gobjBackGround.SetActive(choice)" + choice);
        }

        public void AppearUIElement(Transform uiTrans, bool appear)
        {
            if (uiTrans == null)
                return;
            if (appear)
            {
                uiTrans.localPosition = Vector3.zero; 
            }
            else
            {
                uiTrans.localPosition = new Vector3(0, 10000, 0);
            }
        }
        
        //public Sprite LoadSprite(string spriteName)
        //{
        //    float time = Time.realtimeSinceStartup;
        //    //资源热更新通过AssetBundle加载图片，请查看LoadABTest
        //   // Debuger.Log("Spritename=============" + spriteName);
        //    GameRes res = new GameRes("NetRes/UI/" + spriteName.Substring(spriteName.IndexOf('-') + 1, spriteName.IndexOf('_')) + "/" + "sp-" + spriteName);
        //    res.m_eCatchType = GameRes.eCatchType.CT_DontDestroy;
        //    res = ResourcesManager.LoadObj(res);
        //   // Debuger.Log((Time.realtimeSinceStartup - time) + "----2=============" + spriteName);
        //    GameObject obj = res.m_Object as GameObject;  
        //    //Debuger.Log((Time.realtimeSinceStartup-time)+"----Spritename=============" + spriteName);
        //    return obj.GetComponent<SpriteRenderer>().sprite;
          
        //}
        //public Font LoadFont(string fontName)
        //{
        //    //资源热更新通过AssetBundle加载图片，请查看LoadABTest
        //    // Debuger.Log("Spritename=============" + spriteName);
        //    GameRes res = new GameRes("NetRes/Font/" + fontName + "/" + fontName);
        //    res.m_eCatchType = GameRes.eCatchType.CT_DontDestroy;
        //    res = ResourcesManager.LoadObj(res);
        //    return res.m_Object as Font;
        //}

    }
}
