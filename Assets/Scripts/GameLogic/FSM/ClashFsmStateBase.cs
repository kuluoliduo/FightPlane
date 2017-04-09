using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClashGame
{
    public class LinkState
    {
        public int m_EventId = 0;                                                                         // 事件id;
        public string m_StateName = "";                                                                   // 新状态ID；
        public Dictionary<string, List<string>> m_AllConditions = new Dictionary<string, List<string>>(); // 所有的扭转条件和参数；
    }

    public abstract class ClashFsmStateBase
    {
        protected  GameObjBase m_GameObject = null;                        // 拥有者
        protected string m_StateName = "no name";                         // 状态名==实际的状态类名
        public int StateID { get; set; }                                  // id,为兼容消息数据结构；
        public List<LinkState> m_LinkStateList = new List<LinkState>();   // 有连接的状态。
        protected int m_DefActionID;                                      // 默认动作ID；
        public float m_LiveTime { get; set; }                             // 生命周期；

        public ClashFsmStateBase()
        {
            m_LiveTime = -100;
            StateID = 0;
        }

        public virtual void Update(float deltatime)
        {
            if (m_GameObject == null)
                return;

            if (m_LiveTime > -90.0f)
            {
                if (m_LiveTime > 0)
                {
                    m_LiveTime -= deltatime;
                }
                else
                {
                    m_LiveTime = -100.0f;
                    Event_FSM_EF_Event gevent = new Event_FSM_EF_Event(eFsmEvent.FE_LiveTimeEnd);
                    GameDispatch.DispatchEvent(gevent, m_GameObject);
                }
            }
        }

        public virtual void OnBegin(Event_FSM_EF_Event edata)
        {
            InitData(edata);


                    Debug.Log(m_StateName + " OnBegin");
           
        }

        public virtual void OnEnd()
        {

                Debug.Log(m_StateName + " OnEnd");
        }

        public virtual bool OnEvent(Event_FSM_EF_Event edata)
        {
            switch (edata.m_FsmEventID)
            {
                case  eFsmEvent.FE_StateDealData:
                    {
                        DealDataFromNet(edata);
                    } break;
            }
            return true;
        }

        protected virtual void InitData(Event_FSM_EF_Event edata)
        {
            if (edata == null)
                return;

            // FMS_EVENT_ChangeState和FMS_EVENT_StateDealData是网络事件包
            // FMS_EVENT_StateDealData包不会导致状态切换，而是在OnEvent中直接处理
            if (edata.m_FsmEventID ==  eFsmEvent.FE_ChangeState)
            {
                InitDataFromNet(edata);
            }
            else
            {
                InitDataFromLocal(edata);
            }

        }

        public virtual string GetNextState(Event_FSM_EF_Event edata)
        {
            //Debug.Log("GetNextState-----------" + edata.m_FsmEventID);
            for (int i = 0; i < m_LinkStateList.Count; i++)
            {
                if ((int)edata.m_FsmEventID == m_LinkStateList[i].m_EventId)
                {
                    //输入扭转事件，在扭转条件都成立的情况下，扭转到新状态。
                    if (DBManager.m_DBFsm.m_BnBFsmConditions.AllConditionPass(m_LinkStateList[i], edata))
                    {
                        return m_LinkStateList[i].m_StateName;
                    }
                }
            }
            return null;
        }

        public virtual void SetAction(int actionid)
        {
            m_DefActionID = actionid;
        }

        public virtual int GetActionID()
        {
            return m_DefActionID;
        }
   
        public string GetStateName()
        {
            return m_StateName;
        }

        public GameObjBase GetGameObject()
        {
            return m_GameObject;
        }

        public virtual void SetGameObject(GameObjBase obj)
        {
            m_GameObject = obj;
        }

        public GameObjBase GetOwner()
        {
            return m_GameObject as GameObjBase;
        }

        // 本地切换状态时需要的数据
        protected virtual void InitDataFromLocal(Event_FSM_EF_Event edata) { }

        // 由网络包切换状态时需要的数据
        protected virtual void InitDataFromNet(Event_FSM_EF_Event edata) { }

        // 处理该状态下的同步包
        protected virtual void DealDataFromNet(Event_FSM_EF_Event edata) { }

        // 切换状态时，生成状态同步数据
        public virtual string BuildSyncData() { return null; }

        // 这个状态下，服务器是否可以控制，怪物用的
        public virtual bool IsUnderServerControl() { return true; }

        // 状态能否被打断
        public virtual bool CanBreak() { return true; }
        // 本状态下能否移动
        public virtual bool CanMove() { return true; }
        // 本状态下能否放技能
        public virtual bool CanSkill() { return true; }
        // Can be attacked or not
        public virtual bool CanBeAttack() { return true; }

    }

}