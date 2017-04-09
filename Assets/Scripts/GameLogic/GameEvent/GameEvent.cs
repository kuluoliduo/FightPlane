using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class GameEventData
{
    public int m_iEventID = -1;
    public string m_sSender = "";

    public GameEventData()
    {

    }

    public GameEventData(int id)
    {
        m_iEventID = id;
    }
}

public interface IEventHandle
{
    void AddListener();
    void RemoveListener();
}

public class MonoEventHandle : MonoBehaviour, IEventHandle
{
    public virtual void AddListener()
    {

    }
    public virtual void RemoveListener()
    {

    }
}

public class GameDispatch
{
    public GameDispatch()
    {

    }

    private static Dictionary<int, List<Delegate>> m_DicEvent = new Dictionary<int, List<Delegate>>();

    //添加监听，eobj=null时为全局监听，！=null时挂在eobj上。
    public static void AddEventListener(int eventID, Action<GameEventData> handler, GameObjBase eobj = null)
    {
        if (eobj != null)
        {
            eobj.AddEventListener(eventID, handler);
        }
        else
        {
            if (GameDispatch.m_DicEvent.ContainsKey(eventID))
            {
                List<Delegate> ld = GameDispatch.m_DicEvent[eventID];
                if (ld.Contains(handler))
                {
                    Debug.LogError("EventListener Actionhandler Error!");
                }
                else
                {
                    ld.Add(handler);
                }
            }
            else
            {
                List<Delegate> ld = new List<Delegate>();
                ld.Add(handler);
                GameDispatch.m_DicEvent.Add(eventID, ld);
            }
        }
    }

    public static void RemoveEventListener(int eventID, Action<GameEventData> handler, GameObjBase eobj = null)
    {
        if (eobj != null)
        {
            eobj.RemoveEventListener(eventID, handler);
        }
        else
        {
            if (GameDispatch.m_DicEvent.ContainsKey(eventID))
            {
                List<Delegate> ld = GameDispatch.m_DicEvent[eventID];
                ld.Remove(handler);
            }
        }
    }

    //消息分发，eobj=null全局广播，！=null直接发给挂在eobj上的监听
    public static void DispatchEvent(GameEventData edata, GameObjBase eobj = null)
    {
        if (eobj != null)
        {
            eobj.DispatchEvent(edata);
        }
        else
        {
            //Debug.Log("edata.m_iEventID"+edata.m_iEventID);
            if (m_DicEvent.ContainsKey(edata.m_iEventID))
            {
                List<Delegate> ld = m_DicEvent[edata.m_iEventID];
                for (int i = ld.Count - 1; i >= 0; i--)
                {
                    // Debug.Log("ld.Count"+ld.Count);
                    Action<GameEventData> gameaction = (Action<GameEventData>)ld[i];
                    gameaction(edata);
                }
            }
        }
    }

}

