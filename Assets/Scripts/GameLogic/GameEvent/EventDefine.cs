using UnityEngine;
using System.Collections;

namespace ClashGame
{

    public enum Event_APP
    {
        Start = 1000,
        AppBegin,
        ResUpdate,
        DBReady,
        End,
    };

    public enum Event_Scene
    {
        Start = 2000,
        End,
    };

    public enum Event_World
    {
        Start = 3000,
        ObjDestroy,
        End,
    };

    public enum Event_Player
    {
        Start = 4000,
        End,
    };

    public enum Event_Monster
    {
        Start = 5000,
        End,
    };

    public enum Event_NPC
    {
        Start = 6000,
        End,
    };

    public enum Event_FSM
    {
        Start = 7000,
        EF_Event,
        End,
    }

    public class Event_APP_AppBegin : GameEventData
    {
        public Event_APP_AppBegin()
        {
            m_iEventID = (int)Event_APP.AppBegin;
        }
    }

    public class Event_APP_ResUpdate : GameEventData
    {
        public double m_AllSize; //要更新文件的总大小；
        public double m_CurSize; //已经更新的文件总大小；
        public Event_APP_ResUpdate()
        {
            m_iEventID = (int)Event_APP.ResUpdate;
        }
    }

    public class Event_APP_DBReady : GameEventData
    {
        public Event_APP_DBReady()
        {
            m_iEventID = (int)Event_APP.DBReady;
        }
     }

    public class Event_FSM_EF_Event:GameEventData
    {
       public  eFsmEvent m_FsmEventID = eFsmEvent.FE_Begin;//状态机切换事件
       private ArrayList m_EventArgs;

       public Event_FSM_EF_Event(eFsmEvent FsmEvent)
       {
          m_iEventID = (int)Event_FSM.EF_Event;
          m_FsmEventID = FsmEvent;
           m_EventArgs = new ArrayList();
       }

       public void PushUserData<T>(T data)
       {
           m_EventArgs.Add(data);
       }

       public T GetUserData<T>(int index)
       {
           if (index >= m_EventArgs.Count)
           {
               Debug.LogError("Event GetUserData Error:" + index.ToString());
               return default(T);
           }
           return (T)m_EventArgs[index];
       }
        public Event_FSM_EF_Event()
        {
            m_iEventID = (int)Event_FSM.EF_Event;
        }
    }
}