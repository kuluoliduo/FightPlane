//游戏对象状态机。
using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace ClashGame
{
    public class ClashFsm : ObjComponent
    {

        public ClashFsmStateBase CurState
        {
            get
            {
                return m_CurState;
            }
        }

        //public Creature Owner
        //{
        //    get
        //    {
        //        return (Creature)m_Owner;
        //    }
        //}

        protected Dictionary<string, ClashFsmStateBase> m_StateDic = new Dictionary<string, ClashFsmStateBase>();  //所有状态；
        private ClashFsmStateBase m_CurState = null;                                                                //当前状态；
        public bool IsDebug = false;
        public string statename = "";

        public override void AddListener()
        {
            base.AddListener();
            GameDispatch.AddEventListener((int)Event_FSM.EF_Event, OnEvent, m_Owner);
        }

        public override void RemoveListener()
        {
            base.RemoveListener();
            GameDispatch.RemoveEventListener((int)Event_FSM.EF_Event, OnEvent, m_Owner);
        }
       
        public override void Init(GameObjBase gobj)
        {
            base.Init(gobj);
            Assembly asm = Assembly.GetExecutingAssembly();
            string statemapname = "FSM_" + gobj.GetType().Name;
            if (gobj.GetType().Name == "NpcPlayer"||gobj.GetType().Name =="Pet")
            {
                statemapname = "FSM_" + "NetPlayer";
            }
            ClashFsmStateMap stateMap = DBManager.m_DBFsm.GetFsmStateMap(statemapname);
          //  Debug.Log("class nema===================================================================================================" + statemapname);
            if (stateMap == null)
            {
                Debug.LogError("<FSM not exist>:FSM.name = " + statemapname);
                return;
            }

            foreach (ClashFsmStateMap.State state in stateMap.States)
            {
                ClashFsmStateBase newState = (ClashFsmStateBase)asm.CreateInstance(GDefine.m_GameNamespace + state.Name);
                //Debug.Log("class nemastate name=" + newState.GetType().Name);
                if (newState == null)
                {
                    Debug.LogError("<FSM error>:state not exist: fsm.name = " + statemapname + "state.name = " + state.Name);
                    continue;
                }
                newState.SetGameObject(m_Owner);

                foreach (ClashFsmStateMap.Dest dest in state.Dests)
                {
                    LinkState newLink = new LinkState();
                    newLink.m_StateName = dest.Name;
                    newLink.m_EventId = dest.EventID;
                    // TODO Add Conditions
                    foreach (ClashFsmStateMap.Condition cond in dest.Conditions)
                    {
                        if (!newLink.m_AllConditions.ContainsKey(cond.Name))
                            newLink.m_AllConditions.Add(cond.Name, cond.Params);
                        else
                            Debug.LogError("<FSM error>: same condition can't be added in one dest twice!");
                    }

                    newState.m_LinkStateList.Add(newLink);
                }
                AddState(newState);
            }
      }

        public override void Update(float dtime)
        {
            base.Update(dtime);
            
            if (m_CurState != null)
            {
                //Debug.Log(m_CurState.GetStateName());
                m_CurState.Update(dtime);
                statename = GetStateName(m_CurState.StateID);
            }
        }

        void OnEvent(GameEventData edata)
        {
          
            Event_FSM_EF_Event fsmEvent = (Event_FSM_EF_Event)edata;
            
            //Debug.Log(fsmEvent.m_FsmEventID);
            //if (m_CurState != null && m_CurState.GetGameObject() is NetPlayer)
            //{
            //    //  Debug.Log(" mfs OnEvent" + ml2event.m_iEventID);
            //}
           if (m_CurState == null || edata == null)
                Debug.LogWarning("Warning,BnBFsm is null!");

           switch (fsmEvent.m_FsmEventID)
            {
                case eFsmEvent.FE_ChangeState:
                    {
                        int stateid = ((Event_FSM_EF_Event)edata).GetUserData<int>(0);
                        string statename = GetStateName(stateid);
                        Debug.Log("FMS_EVENT_ChangeState==" + statename + "/stateid=" + stateid);
                        if (statename != null)
                            SetState(statename, fsmEvent);
                      
                    }
                    break;
                case  eFsmEvent.FE_StateDealData:
                    {
                        if (m_CurState != null)
                            m_CurState.OnEvent(fsmEvent);
                    }
                    break;
                default:
                    {
                        if (fsmEvent.m_FsmEventID <=  eFsmEvent.FE_Begin || fsmEvent.m_FsmEventID >=  eFsmEvent.FE_End)
                            break;
                       // if 
                            //(((CharacterBase)m_ML2GameObject).MoveComp.OwnerIsType(EOwnerType.MainPlayer)
                            //|| ((CharacterBase)m_ML2GameObject).MoveComp.OwnerIsType(EOwnerType.MasterMonster)
                            //|| ((CharacterBase)m_ML2GameObject).MoveComp.OwnerIsType(EOwnerType.Pet)
                            //|| (CharacterBase)m_ML2GameObject is UIPlayer
                            //|| ((CharacterBase)m_ML2GameObject).MoveComp.OwnerIsType(EOwnerType.AutoPlayer))
                        {
                            ClashFsmStateBase tempState = null;
                            tempState = GetNextState(fsmEvent);
                            if (tempState == null)
                            {
                                //if (IsDebug == true || ((CharacterBase)m_ML2GameObject).MoveComp.OwnerIsType(EOwnerType.AutoPlayer))
                                // Debug.LogWarning("OnEvent() DestState==null; SourceState = " + m_CurState.GetStateName() + ";Event =" + fsmEvent.m_FsmEventID);
                                return;
                            }

                            //Debug.Log("change from " + m_CurState.GetStateName() + " to " + fsmEvent.m_FsmEventID.ToString());
                            m_CurState.OnEnd();
                            m_CurState = tempState;
                            m_CurState.OnBegin(fsmEvent);
                            
                        }
                    }
                    break;
            }
            
        }

        private ClashFsmStateBase GetNextState(Event_FSM_EF_Event edata)
        {
            if (m_CurState == null)
            {
                Debug.LogWarning("CurState is null!");
                return null;
            }

            string statename = m_CurState.GetNextState(edata);
            if (statename == null)
            {
               // Debug.LogWarning("m_CurState="+m_CurState.GetStateName()+"  statename is null!");
                return null;
            }
            //Debug.Log("GetNextState name=" + statename);
            if (m_StateDic.ContainsKey(statename))
            {
                return m_StateDic[statename];
            }
            else
            {
                Debug.LogWarning("don't find this state:" + statename);
            }
            return null;
        }

        // 设置初始状态,或改网络玩家状态；
        public bool SetState(string statename, Event_FSM_EF_Event edata = null)
        {
            //if (m_CurState != null && m_CurState.GetGameObject() is NetPlayer)
            //{
            //    //  Debug.Log("SetState" + statename);
            //}
            if (m_StateDic.ContainsKey(statename))
            {
                if (m_CurState != null)
                {
                    m_CurState.OnEnd();
                }

                m_CurState = m_StateDic[statename];
                m_CurState.OnBegin(edata);
                return true;
            }
            else
            {
                Debug.LogWarning("SetCurState statename don't find!"+statename);
            }
            return false;
        }

        public ClashFsmStateBase GetCurState()
        {
            return m_CurState;
        }

        private void AddState(ClashFsmStateBase state)
        {
            if (state == null)
                return;
            if (!m_StateDic.ContainsKey(state.GetStateName()))
            {
                m_StateDic[state.GetStateName()] = state;
                state.StateID = m_StateDic.Count;
                //Debug.Log("StateID:"+state.StateID.ToString()+" StateName:"+state.GetStateName());
            }
            else
                Debug.LogError("<FSM error>" + state.GetStateName() + "contained twice!");
        }

        public string GetStateName(int stateid)
        {
            foreach (KeyValuePair<string, ClashFsmStateBase> child in m_StateDic)
            {
                string statename = child.Key;
                ClashFsmStateBase Params = child.Value;
                if (Params.StateID == stateid)
                {
                    return statename;
                }
            }
            return null;
        }

        public int GetStateID(string statename)
        {
            foreach (KeyValuePair<string, ClashFsmStateBase> child in m_StateDic)
            {
                int stateid = -1;
                ClashFsmStateBase Params = child.Value;
                if (child.Key == statename)
                {
                    return Params.StateID;
                }
            }
            return -1;
        }
    }
}
