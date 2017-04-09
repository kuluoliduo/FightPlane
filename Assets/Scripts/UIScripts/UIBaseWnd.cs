using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;

namespace ClashGame
{
    /// <summary>
    /// UI窗口基类
    /// </summary>
    public class UIBaseWnd
    {
        public static Dictionary<string, UIBaseWnd> DicWnd = new Dictionary<string, UIBaseWnd>();
        public static Stack<UIBaseWnd> StackWnd = new Stack<UIBaseWnd>();
        public float scaleDuration = 0.5f;

        public static UIBaseWnd GetWndByName(string strName)
        {
            //Debuger.Log(DicWnd.Count);
            //foreach (KeyValuePair<string, UIBaseWnd> kv in DicWnd)
            //{
            //    Debuger.Log(kv.Value.m_strWndName);
            //}
            if (DicWnd.ContainsKey(strName))
            {
                return DicWnd[strName];
            }
            Debuger.Log(strName + " is null");
            return null;
        }

        public enum eWndStatus
        {
            WS_CLOSE,
            WS_OPEN,
        }

        //// UIManager快捷访问接口
        //protected UIManager UIMgr
        //{
        //    get { return GameApp.m_UIManager; }
        //}

        public GameObject WndRootGameobject; //窗口prefab的gameobject
        protected eWndStatus m_eWndStatus;   //窗口状态
        protected GameObject PrefabAsset;    //Wnd的Prefab.Asset

        #region 配置文件UIWnd.xml中的属性
        public string m_strWndName;          //窗口类名
        protected string m_strPrefabPath;    //窗口Prefab对应路径
        protected bool m_bMutex;             //是否和其它窗口有互斥关系（打开本窗口时关闭其它所有有互斥关系的窗口）
        protected bool m_bModel;             //是否模态
        protected bool m_bBlurEffect;        //打开窗口时是否开启背景模糊
        protected bool m_bCanCharMove;       //窗口打开时人物可否移动,1是可移动
        protected bool m_bWndAnimation;      //窗口动画，1是有动画
        #endregion

        private static string strMutexWnd;   //存储当前互斥窗口


        /// <summary>
        /// 初始化窗口默认参数
        /// </summary>
        /// <param name="strWndName"></param>
        public virtual void Init(UIWndData data)
        {
            m_strWndName = data.WndName;
            m_strPrefabPath = data.PrefabPath;
            m_bMutex = data.Mutex;
            m_bModel = data.Model;
            m_bBlurEffect = data.BlurEffect;
            m_bCanCharMove = data.CanCharMove;
            m_bWndAnimation = data.WndAnimation;
           // Debuger.Log(m_strWndName);
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        public virtual void OpenWnd()
        {
            //Debuger.Log("OpenWnd=" + m_strWndName);
           // Debuger.Log("OpenWnd====start===========" + Time.realtimeSinceStartup);
            if (m_strPrefabPath == null)
            {
                Debuger.LogError("Open window " + m_strWndName + "fail!" + "Please check PrefabPath");
                return;
            }

            //不能重复打开窗口
            if (DicWnd.ContainsKey(m_strWndName) && DicWnd[m_strWndName].IsOpen())
            {
                Debuger.LogWarning("Can't Open window more than once! m_strWndName=" + m_strWndName);
                return;
            }
            
            //关闭当前互斥的窗口
            if (strMutexWnd != null && strMutexWnd != m_strWndName)
            {
               // Debuger.Log("CloseWnd====start===========" + Time.realtimeSinceStartup);
                GameApp.m_UIManager.CloseWnd(strMutexWnd);
               // Debuger.Log("CloseWnd======over=========" + Time.realtimeSinceStartup);

                strMutexWnd = null;

            }

            //打开大背景
         //   GameApp.m_UIManager.ShowBackGround(m_bBlurEffect);


            //HUD只与BattleOverWnd不互斥
            //if ((!string.Equals(m_strWndName, "BattleOverWnd") && !string.Equals(m_strWndName, "StartBeforeWnd") && !string.Equals(m_strWndName, "MonsterStartWnd")) && DicWnd["HUD"].IsOpen())
            //{
            //    //((HUD)UIBaseWnd.GetWndByName("HUD")).CloseWnd();
            //    if (GameApp.m_UIManager.m_Controller != null)
            //    {
            //        GameApp.m_UIManager.m_Controller.Keyboard.gameObject.SetActive(false);
            //    }
            //}

            GameObject wndParent = UICanvasMgr.mInstance.GetUICanvaRootsByLevel(UICanvasMgr.eUICanvasLevel.Level_3);
            if (wndParent == null)
            {
                Debuger.LogError("Cant find out ICanvasMgr.eUICanvasLevel.Level_3 GameObject!");
                return;
            }
           // Debuger.LogWarning("begin="+Time.realtimeSinceStartup);
            //GameRes res = new GameRes();
            //res.m_sResName = m_strPrefabPath;
            //res = ResourcesManager.LoadObj(res);
            //PrefabAsset = res.m_Object as GameObject;

            PrefabAsset = Resources.Load(m_strPrefabPath) as GameObject;
            OnPrefabLoaded();
           // Debuger.LogWarning("begin   1111111111111111111=" + Time.realtimeSinceStartup);
            GameObject child = Object.Instantiate(PrefabAsset) as GameObject;
            //Debuger.LogWarning("end="+Time.realtimeSinceStartup);
            if (child == null)
            {
                Debuger.LogError("Instantiate window " + m_strWndName + "fail!" + "Please check P refabPath");
                return;
            }
            child.name = child.name.Substring(0, child.name.Length - 7);//RoomWnd(Clone)去掉"(Clone)"
            //if (m_strWndName == "MainHUD")
            //    SpritesAddToPrefab(child.transform);

            Vector3 originalPosition = child.GetComponent<RectTransform>().localPosition;
            Vector2 originalSizeDelta = child.GetComponent<RectTransform>().sizeDelta;
            Vector3 originalScale = child.GetComponent<RectTransform>().localScale;
            Quaternion originalQuat = child.GetComponent<RectTransform>().localRotation;

            RectTransform t = child.GetComponent<RectTransform>();
            t.SetParent(wndParent.transform);
            t.localPosition = originalPosition;
            t.sizeDelta = originalSizeDelta;
            t.localRotation = originalQuat;

            if (!m_bWndAnimation)
            {
                t.localScale = Vector3.one;
                WndRootGameobject = child;
            }
            else
            {
                t.localScale = Vector3.zero;
                WndRootGameobject = child;

                FreezonCurWndOperation();
                //先关后开
                Tweener tweener = WndRootGameobject.transform.DOScale(originalScale, scaleDuration);
                if (GameApp.m_UIManager.bWndCloseAnimating)
                {
                    tweener.SetDelay(scaleDuration);
                }
                //设置这个Tween不受Time.scale影响
                tweener.SetUpdate(true);
                tweener.SetEase(Ease.OutBack);
                tweener.OnComplete(delegate()
                    {
                        DeFreezonCurWndOperation();
                    });

            }

            //HUD不入栈，hud互斥了现在
            if (m_strWndName != "HUD")
            {
                if (!StackWnd.Contains(DicWnd[m_strWndName]))
                {
                    if (StackWnd.Count >= 10)
                    {
                        Debuger.LogWarning("StackWnd.count is 10!");
                        //return;
                    }
                    //StackWnd.Push(DicWnd[m_strWndName]);  不再入栈了，暂时不用这东西了
                    //Debuger.Log("StackWnd.Count+++++++++" + StackWnd.Count);
                }
            }

            if (m_bMutex)
            {
                strMutexWnd = m_strWndName;
            }
            SetWndStatus(eWndStatus.WS_OPEN);
           // Debuger.Log("Open=======over==================" + Time.realtimeSinceStartup);
           // Debuger.Log("OpenWnd111=" + m_strWndName);
        }

        /// <summary>
        /// 关闭窗口，各个窗口关闭请调用ReturnWnd和DirectCloseWnd;
        /// </summary>
        public virtual void CloseWnd()
        {
            // Debuger.Log("---------------------------------------------------------------------base close="+m_strWndName);
            OnBeforeClose();

            ////if (!string.Equals(m_strWndName, "HUD") && DicWnd["HUD"].IsOpen())
            ////{
            ////    ((HUD)UIBaseWnd.GetWndByName("HUD")).ShowHUD(true);
            ////}

            //if (m_bBlurEffect)
            //{
            //    GameApp.m_UIManager.EnableBlurEffect(false);
            //    GameApp.m_UIManager.EnableBackgroundMask(false);
            //}

            if (WndRootGameobject != null)
            {
                //if (!m_bWndAnimation)
                //{
                GameObject.Destroy(WndRootGameobject);
                SetWndStatus(eWndStatus.WS_CLOSE);
                WndRootGameobject = null;
                //}
                //else
                //{
                //    //颜色渐变
                //    Color origColor = new Color(0, 0, 0, 0);
                //    MaskableGraphic[] graphicAll = WndRootGameobject.GetComponentsInChildren<MaskableGraphic>();
                //    foreach (MaskableGraphic gra in graphicAll)
                //    {
                //        origColor = gra.color + new Color(0, 0, 0, -gra.color.a);
                //        gra.CrossFadeColor(origColor, scaleDuration / 2, true, true);
                //    }

                //    FreezonCurWndOperation();
                //    //先关后开
                //    GameApp.m_UIManager.bWndCloseAnimating = true;
                //    Tweener tweener = WndRootGameobject.transform.DOScale(Vector3.zero, scaleDuration);
                //    //设置这个Tween不受Time.scale影响
                //    tweener.SetUpdate(true);
                //    tweener.SetEase(Ease.InBack);
                //    tweener.onComplete = delegate()
                //        {
                //            DeFreezonCurWndOperation();
                //            GameApp.m_UIManager.bWndCloseAnimating = false;

                //            GameObject.Destroy(WndRootGameobject);
                //            SetWndStatus(eWndStatus.WS_CLOSE);
                //            WndRootGameobject = null;
                //        };

                //}
                Debuger.Log("1111111111-----------------------base close=" + m_strWndName);

            }

            //关闭大背景
          //  GameApp.m_UIManager.ShowBackGround(m_bBlurEffect);

            if (PrefabAsset != null)
            {
                PrefabAsset = null;
            }

           // float time1 = Time.realtimeSinceStartup;
            Resources.UnloadUnusedAssets();
           // Debuger.LogWarning("UnloadUnusedAssets time1=" + (Time.realtimeSinceStartup-time1));

            ////   if (GameQuality.Instance().m_GameQuality == enumGameQuality.GQ_Fast)
            ////  {
            ////   Resources.UnloadUnusedAssets();
            ////  System.GC.Collect();
            ////   }
            if (strMutexWnd != null && strMutexWnd == m_strWndName)
            {
                strMutexWnd = null;
            }
            //if (GameApp.GetPlayerGuideManager() != null)
            //{
            //    GameApp.GetPlayerGuideManager().ShowGuide();
            //}

        }

        /// <summary>
        /// 关闭当前窗口，如果有，返回上个窗口
        /// </summary>
        public virtual void ReturnWnd()
        {
            CloseWnd();
            if (StackWnd.Count > 0)
            {
                StackWnd.Pop();
                if (StackWnd.Count > 0)
                {
                    StackWnd.Pop().OpenWnd();
                }
                Debuger.Log("StackWnd.Count-------" + StackWnd.Count);
            }

        }

        /// <summary>
        /// 关闭窗口，并清空StackWnd栈
        /// </summary>
        public virtual void DirectCloseWnd()
        {
            CloseWnd();
            StackWnd.Clear();
        }

        /// <summary>
        /// 打开/关闭窗口
        /// </summary>
        public virtual void SwitchWnd()
        {
            if (IsOpen())
            {
                CloseWnd();
            }
            else if (IsClose())
            {
                OpenWnd();
            }
        }

        /// <summary>
        /// 设置窗口当前状态
        /// </summary>
        /// <param name="e"></param>
        public virtual void SetWndStatus(eWndStatus e)
        {
            m_eWndStatus = e;
            switch (e)
            {
                case eWndStatus.WS_CLOSE:
                    OnClose();
                    break;
                case eWndStatus.WS_OPEN:
                    OnOpen();
                    break;
            }
        }

        /// <summary>
        /// 获取窗口状态
        /// </summary>
        /// <returns></returns>
        public virtual eWndStatus GetWndStatus()
        {
            return m_eWndStatus;
        }

        /// <summary>
        /// 窗口是否打开着
        /// </summary>
        public virtual bool IsOpen()
        {
            return m_eWndStatus == eWndStatus.WS_OPEN;
        }

        /// <summary>
        /// 窗口是否关闭着
        /// </summary>
        public virtual bool IsClose()
        {
            return m_eWndStatus == eWndStatus.WS_CLOSE;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// 窗口打开时人物可否移动
        /// </summary>
        /// <returns>true表示可移动</returns>
        public virtual bool CanCharMove()
        {
            return m_bCanCharMove;
        }

        /// <summary>
        /// SetDontDestroyOnLoad
        /// </summary>
        public virtual void SetDontDestroyOnLoad()
        {
            GameObject.DontDestroyOnLoad(WndRootGameobject);
        }

        /// <summary>
        /// 窗口打开时调用
        /// </summary>
        protected virtual void OnOpen()
        {

        }

        /// <summary>
        /// 窗口关闭时调用
        /// </summary>
        protected virtual void OnClose()
        {

            //RewardItemMsg.CloseItemMsg();
        }

        /// <summary>
        /// 窗口关闭前调用
        /// </summary>
        protected virtual void OnBeforeClose()
        {

        }

        /// <summary>
        /// 资源加载完毕回调
        /// </summary>
        protected virtual void OnPrefabLoaded()
        {

        }

        /// <summary>
        /// 冻结当前界面操作
        /// </summary>
        private void FreezonCurWndOperation()
        {
            Transform wndmask = WndRootGameobject.transform.Find("WndMask_Bottom");
            if (wndmask != null)
            {
                wndmask.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 允许当前界面操作
        /// </summary>
        private void DeFreezonCurWndOperation()
        {
            Transform wndmask = WndRootGameobject.transform.Find("WndMask_Bottom");
            if (wndmask != null)
            {
                wndmask.gameObject.SetActive(false);
            }
        }

        private void SpritesAddToPrefab(Transform prefabTrans)
        {
            Debuger.Log("timestart=========" + Time.realtimeSinceStartup);
            List<string> pathList = new List<string>();
            PrefabSpriteInfoStorage infoStorage = prefabTrans.GetComponent<PrefabSpriteInfoStorage>();
            if (infoStorage == null)
            {
                Debuger.LogError(prefabTrans.name + "---------PrefabSpriteInfoStorage Component is null");
            }
            //Debuger.Log("infoStorage.PrefabName===================" + infoStorage.PrefabName);

            Image[] imageList = prefabTrans.GetComponentsInChildren<Image>();
            foreach (Image img in imageList)
            {

                pathList.Clear();
                Transform transParent = img.transform;
                while (transParent.name != prefabTrans.name)
                {
                    pathList.Add(transParent.name);
                    transParent = transParent.parent;
                }
                pathList.Add(transParent.name);

                string ImageTransPath = ListToStringPath(pathList);

                //img.sprite = GameApp.m_UIManager.LoadSprite(GetSpriteNameFromList(ImageTransPath, infoStorage.PSIList));
            }

            Debuger.Log(imageList.Length+"------timestart textList=========" + Time.realtimeSinceStartup);


            Text[] textList = prefabTrans.GetComponentsInChildren<Text>();
            foreach (Text txt in textList)
            {

                pathList.Clear();
                Transform transParent = txt.transform;
                while (transParent.name != prefabTrans.name)
                {
                    pathList.Add(transParent.name);
                    transParent = transParent.parent;
                }
                pathList.Add(transParent.name);

                string textTransPath = ListToStringPath(pathList);

                //txt.font = GameApp.m_UIManager.LoadFont(GetSpriteNameFromList(textTransPath, infoStorage.PSIList));
            }
            Debuger.Log("timeEnd=========" + Time.realtimeSinceStartup);
        }
        private string ListToStringPath(List<string> listPath)
        {
            string path = "";
            if (listPath.Count == 0)
            {
                Debuger.LogError("listPath.Count ==== 0");
            }
            if (listPath.Count > 0)
            {
                for (int i = listPath.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        path = path + listPath[i];
                    }
                    else
                    {
                        path = path + listPath[i] + "/";
                    }
                }
            }

            return path;
        }
        private string GetSpriteNameFromList(string path, List<PrefabSpriteInfo> infoList)
        {
            string spriteName = "";
            //Debuger.Log("infoList.Count=========" + infoList.Count);
            //Debuger.Log("path=====" + path);
            foreach (PrefabSpriteInfo info in infoList)
            {
                //Debuger.Log("info.ImageTransPath------" + info.ImageTransPath);
                //Debuger.Log("info.SpriteName----------" + info.SpriteName);
                if (info.ImageTransPath == path)
                {
                    spriteName = info.SpriteName;
                    return spriteName;
                }
            }
            return spriteName;
        }


    }
}
