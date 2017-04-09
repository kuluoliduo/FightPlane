using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public enum eGameObjType
{
    GOT_None = 0,
    GOT_Plane,
    GOT_Bullet
}
public abstract class GameObjBase:IEventHandle
{
    public GameObject m_ObjInstance = null;
    public uint m_InstanceID = 0;
    public eGameObjType m_ObjType = eGameObjType.GOT_None;
    public string m_sPrefabPath = null;
    public List<ObjComponent> m_CompList = new List<ObjComponent>();
    private Dictionary<int, List<Delegate>> m_DicEvent = new Dictionary<int, List<Delegate>>();
    public Vector3 Position
    {
        get { return m_ObjInstance.transform.position;}
        set { m_ObjInstance.transform.position = value; }
    }
    /// <summary>
    /// 统一注册取消消息
    /// </summary>
    public virtual void AddListener()
    {

    }
    public virtual void RemoveListener()
    {

    }
    public virtual void Update(float fTime)
    {
        //Debug.Log(m_CompList.Count);
        for(int i =0;i<m_CompList.Count;i++)
        {
            m_CompList[i].Update(fTime);
            
        }
    }
    protected void Create(int dbID, uint instanceID)
    {
        InitProperty(dbID);
        InitPrefabPath();
        InstantiateObj();
        AddBaseComp();
        //Debug.Log("Create GameObjBase");
        //m_InstanceID = instanceID;
        AddListener();
    }
    protected virtual void InitProperty(int dbID)
    {
        //Debug.Log("virtual void InitProperty");
    }
    protected abstract void InitPrefabPath();
    protected virtual void InstantiateObj()
    {
        //Debug.Log("virtual void InstantiateObj");
    }
    protected virtual void AddBaseComp()
    {
        //Debug.Log("virtual void AddBaseComp");
    }
    public virtual void Release()
    {
        //Debug.LogWarning("Release id=" + m_InstanceID);
        //GameEventData dt = new GameEventData(1);
        //GameDispatch.DispatchEvent(dt, this);

        for (int i = 0; i < m_CompList.Count; i++)
        {
            m_CompList[i].Release();
        }
        m_CompList.Clear();

        RemoveListener();

        m_InstanceID = 0;
        if (m_ObjInstance != null)
        {
            //GameApp.m_WorldManager.m_RtsRtsPoolManager.DestoryInstance(m_ObjInstance, m_iPool);
            GameObject.Destroy(m_ObjInstance);
            m_ObjInstance = null;
        }
    }
    public ObjComponent AddComponent(string ComponentName)
    {
        if (m_ObjInstance != null)
        {
            ObjComponent comp = Assembly.GetExecutingAssembly().CreateInstance(ComponentName) as ObjComponent;
            if (comp == null)
            {
                Debug.LogError("AddComponent Error comp=null!!!");
                return null;
            }
            comp.m_Owner = this;
            m_CompList .Add(comp);
            //Debug.Log("AddComponent ok");
            return comp;
        }
        else
        {
            Debug.LogError("AddComponent Error m_ObjInstance=null!!!");
        }
        return null;
    }
    /// <summary>
    /// 消息处理
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="handler"></param>
    public void AddEventListener(int eventID, Action<GameEventData> handler)
    {
        if (m_DicEvent.ContainsKey(eventID))
        {
            List<Delegate> ld = m_DicEvent[eventID];
            if (ld.Contains(handler))
            {
                Debug.LogError("EventListener Actionhandler Error!");
            }
            else
            {
                //Debug.Log(this.GetType().FullName+"    AddEventListener eventID=" + eventID);
                ld.Add(handler);
            }
        }
        else
        {
            // Debug.Log(this.GetType().FullName+"    AddEventListener eventID=" + eventID);
            List<Delegate> ld = new List<Delegate>();
            ld.Add(handler);
            m_DicEvent.Add(eventID, ld);
        }
    }
    public void RemoveEventListener(int eventID, Action<GameEventData> handler)
    {
        if (m_DicEvent.ContainsKey(eventID))
        {
            List<Delegate> ld = m_DicEvent[eventID];
            ld.Remove(handler);
        }
    }
    public void DispatchEvent(GameEventData edata)
    {
        if (m_DicEvent.ContainsKey(edata.m_iEventID))
        {
            List<Delegate> ld = m_DicEvent[edata.m_iEventID];

            for (int i = ld.Count - 1; i >= 0; i--) //自己调删除报错。
            {
                Action<GameEventData> gameaction = (Action<GameEventData>)ld[i];
                gameaction(edata);
            }

        }
    }
}
