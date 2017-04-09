using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ClashGame
{
    public class OperationTipsUI : MonoBehaviour
    {
        private GameObject TipsWithMask;    //有遮罩的tips（游戏暂停时等地方可用）
        private Text TipsWithMaskLabel;
        public bool needTips = true;       // 控制tips是否显示

        
        static private OperationTipsUI WndRoot;//本窗口root
        static public OperationTipsUI Instance()
        {
            if (WndRoot == null)
            {
                GameApp.m_UIManager.AddUI(Resources.Load("NetRes/UI/Wnd/OperationTips/OperationTips") as GameObject, UICanvasMgr.eUICanvasLevel.Level_7);
                WndRoot = UICanvasMgr.mInstance.GetCanvasByLevel(UICanvasMgr.eUICanvasLevel.Level_7).transform.GetComponentInChildren<OperationTipsUI>();
            }
            return WndRoot;
        }

        
        public Text[] Labels;
        public Transform[] Backs;

        private const int MaxLineCount = 4;
        private const float ShowTime = 1f;
        private int CurLineCount = 0;
        private LinkedList<string> buffs = new LinkedList<string>();

        private class COperationItem
        {
            public Text Label;
            public Transform Back;
            public float EndTime;
        }
        private COperationItem[] Items = new COperationItem[MaxLineCount];

        public void Pop(string str)
        {
            if (needTips == false)
                return;

            if (CurLineCount >= MaxLineCount)
            {
                buffs.AddLast(str);
            }
            else
            {
                Items[CurLineCount].Label.text = str;
                Items[CurLineCount].EndTime = Time.realtimeSinceStartup + ShowTime;
                Items[CurLineCount].Label.gameObject.SetActive(true);
                Items[CurLineCount].Back.gameObject.SetActive(true); 
                CurLineCount++;
            }
        }

        
        void Awake()
        {
            for (int i = 0; i < MaxLineCount; ++i)
            {
                Items[i] = new COperationItem();
                Items[i].Label = Labels[i];
                Items[i].Back = Backs[i];
                Items[i].EndTime = 0f;
                Labels[i].gameObject.SetActive(false);
                Backs[i].gameObject.SetActive(false);

            }
        }

        void Start()
        {

        }

        void Update()
        {
            if (CurLineCount > 0)
            {
                if (Time.realtimeSinceStartup > Items[0].EndTime)
                {
                    for (int i = 0; i < CurLineCount; ++i)
                    {
                        if (i != CurLineCount - 1)
                        {
                            Items[i].Label.text = Items[i + 1].Label.text;
                            Items[i].EndTime = Items[i + 1].EndTime;
                        }
                        else
                        {
                            if (i != MaxLineCount - 1)
                            {
                                Items[i].Label.gameObject.SetActive(false);
                                Items[i].Back.gameObject.SetActive(false); 
                                CurLineCount--;
                            }
                            else
                            {
                                if (buffs.Count > 0)
                                {
                                    Items[i].Label.text = buffs.First.Value;
                                    Items[i].EndTime = Time.realtimeSinceStartup + ShowTime;
                                    buffs.RemoveFirst();
                                }
                                else
                                {
                                    Items[i].Label.gameObject.SetActive(false);
                                    Items[i].Back.gameObject.SetActive(false);        
                                    CurLineCount--;
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}