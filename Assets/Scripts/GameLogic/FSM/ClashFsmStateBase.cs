using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClashGame
{
    public class LinkState
    {
        public int m_EventId = 0;                                                                         // �¼�id;
        public string m_StateName = "";                                                                   // ��״̬ID��
        public Dictionary<string, List<string>> m_AllConditions = new Dictionary<string, List<string>>(); // ���е�Ťת�����Ͳ�����
    }

    public abstract class ClashFsmStateBase
    {
        protected  GameObjBase m_GameObject = null;                        // ӵ����
        protected string m_StateName = "no name";                         // ״̬��==ʵ�ʵ�״̬����
        public int StateID { get; set; }                                  // id,Ϊ������Ϣ���ݽṹ��
        public List<LinkState> m_LinkStateList = new List<LinkState>();   // �����ӵ�״̬��
        protected int m_DefActionID;                                      // Ĭ�϶���ID��
        public float m_LiveTime { get; set; }                             // �������ڣ�

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

            // FMS_EVENT_ChangeState��FMS_EVENT_StateDealData�������¼���
            // FMS_EVENT_StateDealData�����ᵼ��״̬�л���������OnEvent��ֱ�Ӵ���
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
                    //����Ťת�¼�����Ťת����������������£�Ťת����״̬��
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

        // �����л�״̬ʱ��Ҫ������
        protected virtual void InitDataFromLocal(Event_FSM_EF_Event edata) { }

        // ��������л�״̬ʱ��Ҫ������
        protected virtual void InitDataFromNet(Event_FSM_EF_Event edata) { }

        // �����״̬�µ�ͬ����
        protected virtual void DealDataFromNet(Event_FSM_EF_Event edata) { }

        // �л�״̬ʱ������״̬ͬ������
        public virtual string BuildSyncData() { return null; }

        // ���״̬�£��������Ƿ���Կ��ƣ������õ�
        public virtual bool IsUnderServerControl() { return true; }

        // ״̬�ܷ񱻴��
        public virtual bool CanBreak() { return true; }
        // ��״̬���ܷ��ƶ�
        public virtual bool CanMove() { return true; }
        // ��״̬���ܷ�ż���
        public virtual bool CanSkill() { return true; }
        // Can be attacked or not
        public virtual bool CanBeAttack() { return true; }

    }

}