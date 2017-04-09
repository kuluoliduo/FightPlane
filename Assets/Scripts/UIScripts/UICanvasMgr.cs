using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ClashGame
{
    /// <summary>
    /// 游戏中Canvas管理器
    /// </summary>
    public class UICanvasMgr : MonoBehaviour
    {
        private static GameObject uiRoot;
        private static float KuanGaoBi = 1.5f; //960/640
        //Canvas层级关系，反序*100对应Canvas的Plane Distance
        //注意Canvas的Plane Distance不能超过Camera的Clipping Planes(0.3--1500)
        public enum eUICanvasLevel
        {
            Level_1 = 1,      //BloodBar,Name,DamageNum
            Level_2 = 2,      //Conversio Bubble
            Level_3 = 3,      //NormalWindow,全屏对话
            Level_4 = 4,      //ObtainGoodsWindow
            Level_5 = 5,      //ItemInfoBox
            Level_6 = 6,      //MessageBox
            Level_7 = 7,      //Publish Broadcast
            MaxLevel,
        }

        static UICanvasMgr mInst;
        static public UICanvasMgr mInstance
        {
            get
            {
                if (mInst == null)
                {
                    mInst = Object.FindObjectOfType(typeof(UICanvasMgr)) as UICanvasMgr;
                    {
                        if (mInst == null)
                        {
                            GameObject go = new GameObject("_UICanvasMgr");
                            DontDestroyOnLoad(go);
                            mInst = go.AddComponent<UICanvasMgr>();
                        }
                    }
                }
                return mInst;
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
        }

        void OnDestroy()
        { }

        public Dictionary<int, GameObject> UICanvasDic = new Dictionary<int, GameObject>();
        public Dictionary<int, List<GameObject>> UIAddedDic = new Dictionary<int, List<GameObject>>();

        public Canvas GetCanvasByLevel(eUICanvasLevel level)
        {
            GameObject go;
            Canvas canvas = null;
            if (UICanvasDic.TryGetValue((int)level, out go))
            {
                canvas = (Canvas)go.GetComponentInChildren<Canvas>();
            }
            return canvas;
        }

        public GameObject GetUICanvaRootsByLevel(eUICanvasLevel level)
        {
            GameObject go = null;
            GameObject root = null;
            //return UICanvasDic.TryGetValue((int)level, out go) ? go : null;
            if (UICanvasDic.TryGetValue((int)level, out go))
            {
                if (go != null)
                {
                    root = go.transform.gameObject;
                }
            }
            if (root == null)
                Debuger.LogError("GetUICanvaRootsByLevel is fail, return root is null");
            return root;
        }

        public void InitCanvas()
        {
            float SheBeiKuanGaoBi = Screen.width / (float)Screen.height;
            //Debuger.Log("8888888888888==" + SheBeiKuanGaoBi);
            if (uiRoot == null)
            {
                //GameRes res = new GameRes("NetRes/UI/Wnd/UIRoot/UIRoot", GameRes.eCatchType.CT_None);
                //res = ResourcesManager.LoadObj(res);
                //uiRoot = GameObject.Instantiate(res.m_Object) as GameObject;
                uiRoot = GameObject.Instantiate(Resources.Load("NetRes/UI/Wnd/UIRoot/UIRoot")) as GameObject;
                if (uiRoot == null)
                {
                    Debuger.LogError("instantiate uiRoot fail!");
                    return;
                }
            }
            DontDestroyOnLoad(uiRoot);
            for (int i = 1; i < (int)eUICanvasLevel.MaxLevel; i++)
            {
                GameObject canvas = uiRoot.transform.Find("Canvas_" + i).gameObject;
                if (canvas == null)
                {
                    Debuger.LogWarning("cant find out Canvas_" + i);
                    continue;
                }
                if(SheBeiKuanGaoBi < KuanGaoBi)
                {
                    canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
                }else
                {
                    canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
                }
                UICanvasDic.Add(i, canvas);
            }
        }

        public GameObject GetUIRoot()
        {
            return uiRoot;
        }

        /// <summary>
        /// 清除所有canvas下所有GameObject
        /// </summary>
        public void ClearAllGameObjects()
        {
            foreach (KeyValuePair<int, List<GameObject>> kv in UIAddedDic)
            {
                for (int i = 0; i < kv.Value.Count; i++)
                {
                    GameObject.Destroy(kv.Value[i]);
                }
            }
            UIAddedDic.Clear();
        }

    }

}
