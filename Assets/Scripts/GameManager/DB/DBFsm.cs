using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace ClashGame
{
    public enum eFsmEvent
    {
        FE_Begin = 1000,
        FE_ChangeState,         // 网络事件包，通知FSM跳转到指定状态
        FE_StateDealData,        // 网络事件包，通知CurStae处理数据包
        //游戏状态扭转事件，用于控制主角MainPlayer
        FE_LiveTimeEnd,          // 生命期到了事件
    
        FE_IDLE,                 // 切换为StateIdle状态
        FE_RUN,                  // 切换为StateRun状态
        FE_Fire,                 // 开火 
     
        FE_OnDie,                // 被弄死
        FE_Hurt,            //受伤
        FE_End
    }

   // FSM配置表
    public class ClashFsmStateMap
    {
        public class Condition
        {
            public string Name = "";
            public List<string> Params = new List<string>();
        }

        public class Dest
        {
            public string Name = "";
            public int EventID = 0;
            public List<Condition> Conditions = new List<Condition>();
        }

        public class State
        {
            public string Name = "";
            public List<Dest> Dests = new List<Dest>();
        }

        public List<State> States = new List<State>();
    }

    // FSM.DB
    public class DBFsm  
    {
        private Dictionary<string, ClashFsmStateMap> BnBFsmStateMapDic = new Dictionary<string, ClashFsmStateMap>();
        public ClashFsmConditions m_BnBFsmConditions = null;
        private List<string> Files = new List<string>();

        public void Init()
        {
            m_BnBFsmConditions = new ClashFsmConditions();
            m_BnBFsmConditions.Init();

            ReadFSMFilename();
            foreach (string file in Files)
            {
                LoadData(file);
            }
        }

        public ClashFsmStateMap GetFsmStateMap(string strFilename)
        {
            ClashFsmStateMap stateMap = null;
            if (BnBFsmStateMapDic.TryGetValue(strFilename, out stateMap))
                return stateMap;
            else
                return null;
        }

        private void ReadFSMFilename()
        {
            XmlDocument xmlDoc = new XmlDocument();
            TextAsset textAsset = (TextAsset)Resources.Load("NetRes/DB/FSM/FSM_Files", typeof(TextAsset));
            if (textAsset == null)
            {
                Debug.LogError("<Load FSM DB Failed>:filename = NetRes/DB/FSM/FSM_Files");
                return;
            }
            xmlDoc.Load(new StringReader(textAsset.text));
            XmlElement xmlRoot = xmlDoc.DocumentElement;

            Files.Clear();
            XmlNodeList nodeList = xmlRoot.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                XmlElement fileElem = (XmlElement)node;
                string filename = fileElem.GetAttribute("FileName");
                Files.Add(filename);
            }
        }

        private void LoadData(string strFilename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            TextAsset textAsset = (TextAsset)Resources.Load(strFilename, typeof(TextAsset));  
            if (textAsset == null)
            {
                Debug.LogError("<Load FSM DB Failed>:filename = " + strFilename + "SDFF");
                return;
            }
            xmlDoc.Load(new StringReader(textAsset.text));
            XmlElement xmlRoot = xmlDoc.DocumentElement;
            ClashFsmStateMap newFsm = new ClashFsmStateMap();

            // <State>...
            XmlNodeList stateNodes = xmlRoot.ChildNodes;
            foreach (XmlNode stateNode in stateNodes)
            {
                // <State>
                if (!(stateNode is XmlElement))
                    continue;
                XmlElement xmlElem = (XmlElement)stateNode;
                ClashFsmStateMap.State newState = new ClashFsmStateMap.State();

                // <State.Name>
                newState.Name = xmlElem.GetAttribute("Name");

                // <State.Dest> <State.Dest> <State.Dest>
                XmlNodeList destNodes = xmlElem.ChildNodes;
                foreach (XmlNode destNode in destNodes)
                {
                    // <State.Dest>
                    if (!(destNode is XmlElement))
                        continue;
                    XmlElement xmlDest = (XmlElement)destNode;
                    ClashFsmStateMap.Dest newDest = new ClashFsmStateMap.Dest();

                    // <State.Dest.Name>
                    newDest.Name = xmlDest.GetAttribute("Name");
                    // <State.Dest.EventID>
                    string eventID = xmlDest.GetAttribute("EventID");
                    eFsmEvent enumEventID = (eFsmEvent)Enum.Parse(typeof(eFsmEvent), eventID);
                    if (eventID != "")
                        //newDest.EventID = XmlConvert.ToInt32(eventID);
                        newDest.EventID = (int)enumEventID;
                    else
                        Debug.LogError("<FSM error>:State.Dest.Event can not be empty");

                    //<State.Dest.Conditions>
                    XmlElement xmlConditions = (XmlElement)xmlDest.SelectSingleNode("Conditions");
                    if (xmlConditions != null)
                    {
                        // <State.Dest.Conditions.Condition>...
                        XmlNodeList condNodes = xmlConditions.ChildNodes;
                        foreach (XmlNode condNode in condNodes)
                        {
                            // <State.Dest.Conditions.Condition>
                            if (!(condNode is XmlElement))
                                continue;
                            XmlElement xmlCond = (XmlElement)condNode;
                            ClashFsmStateMap.Condition newCond = new ClashFsmStateMap.Condition();

                            // <State.Dest.Conditions.Condition.Name>
                            newCond.Name = xmlCond.GetAttribute("Name");

                            // <State.Dest.Conditions.Condition.Param>...
                            XmlNodeList paramNodes = xmlCond.ChildNodes;
                            foreach (XmlNode paramNode in paramNodes)
                            {
                                // <State.Dest.Conditions.Condition.Param>
                                if (!(paramNode is XmlElement))
                                    continue;
                                XmlElement xmlParam = (XmlElement)paramNode;

                                string Value = xmlParam.GetAttribute("Value");
                                newCond.Params.Add(Value);
                            }
                            newDest.Conditions.Add(newCond);
                        }
                    }
                    newState.Dests.Add(newDest);
                }
                newFsm.States.Add(newState);
            }
            string fsmName = strFilename.Substring(strFilename.LastIndexOf('/') + 1, strFilename.Length - strFilename.LastIndexOf('/') - 1);
            BnBFsmStateMapDic.Add(fsmName, newFsm);
        }
    }
}
