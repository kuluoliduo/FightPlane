//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

//namespace ClashGame
//{

//    public class BloodBarMgr : MonoBehaviour
//    {
//        static private BloodBarMgr bloodBarMgr;
//        static public BloodBarMgr mInstance
//        {
//            get
//            {
//                if (bloodBarMgr == null)
//                {
//                    bloodBarMgr = UICanvasMgr.mInstance.GetUICanvaRootsByLevel(UICanvasMgr.eUICanvasLevel.Level_1).AddComponent<BloodBarMgr>();
//                }
//                return bloodBarMgr;
//            }
//        }

//        private Dictionary<Transform, CoupleTransform> DicCP = new Dictionary<Transform, CoupleTransform>();
//        private Dictionary<Transform, CoupleTransform> DicWarningCP = new Dictionary<Transform, CoupleTransform>();

//        public GameObject AddBloodBar(uint targetID, bool blueBar,eObjSize size)
//        {
//            Transform target = GameApp.m_WorldManager.GetRtsObj(targetID).m_ObjInstance.transform;
//            Vector3 offset = new Vector3(0,2,0);//new Vector3(0, GameApp.m_WorldManager.GetRtsObj(targetID).m_ShowTalkOffset, 0);
//            if (DicCP.ContainsKey(target))
//            {
//                DicCP[target].bubble.GetComponent<BloodBar>().RefreshBloodBar(targetID, size);
//                return DicCP[target].bubble.gameObject;
//            }
//            else
//            {

//                string prefabPath = "NetRes/UI/Wnd/BloodBar/BloodBarRed";
//                if(blueBar)
//                {
//                    prefabPath = "NetRes/UI/Wnd/BloodBar/BloodBarBlue";
//                }
//                GameObject prefabObj = Resources.Load(prefabPath) as GameObject;

//                GameObject InstantiatePre = GameApp.m_UIManager.AddUI(prefabObj, target, offset, UICanvasMgr.eUICanvasLevel.Level_1);
               
//                BloodBar bb = InstantiatePre.GetComponent<BloodBar>();
//                if (bb == null)
//                {
//                    bb = InstantiatePre.AddComponent<BloodBar>();
//                }
//                bb.RefreshBloodBar(targetID, size);

//                CoupleTransform ct = new CoupleTransform();
//                ct.target = target;
//                ct.bubble = InstantiatePre.transform;
//                DicCP.Add(ct.target, ct);
//                return InstantiatePre;
//            }
//        }
//        public void RemoveBloodBar(Transform target)
//        {
//            if (DicCP.ContainsKey(target))
//            {
//                DicCP.Remove(target);
//            }
//        }

//        public GameObject AddWarningSign(uint targetID, string path,bool enable, int targetSize = 1)
//        {
//            Transform target = GameApp.m_WorldManager.GetRtsObj(targetID).m_ObjInstance.transform;
//            Vector3 offset = new Vector3(0, 2, 0);
//            if (DicWarningCP.ContainsKey(target))
//            {
//                DicWarningCP[target].bubble.GetComponent<WarningSign>().WarningSignEnable(enable);
//                return DicWarningCP[target].bubble.gameObject;
//            }
//            else
//            {
//                string prefabPath = path;//"NetRes/UI/Wnd/BloodBar/WarningSign";
//                GameObject prefabObj = Resources.Load(prefabPath) as GameObject;
//                if (prefabObj == null)
//                {
//                    Debug.LogError("prefabObj is null");
//                    return null;
//                }

//                GameObject InstantiatePre = GameApp.m_UIManager.AddUI(prefabObj, target, offset, UICanvasMgr.eUICanvasLevel.Level_1);
//                WarningSign bb = InstantiatePre.GetComponent<WarningSign>();
//                if (bb == null)
//                {
//                    bb = InstantiatePre.AddComponent<WarningSign>();
//                }
//                bb.WarningSignEnable(enable);

//                CoupleTransform ct = new CoupleTransform();
//                ct.target = target;
//                ct.bubble = InstantiatePre.transform;
//                DicWarningCP.Add(ct.target, ct);
//                return InstantiatePre;
//            }
//        }
//        public void RemoveWarningSign(Transform target)
//        {
//            if (DicWarningCP.ContainsKey(target))
//            {
//                Destroy(DicWarningCP[target].bubble);
//                DicWarningCP.Remove(target);               
//            }
//        }

//        void Start()
//        {

//        }

//        // Update is called once per frame
//        void Update()
//        {

//        }
//    }

//}
