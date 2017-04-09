using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 组件基类
/// </summary>
public class ObjComponent:IEventHandle
{
    //挂靠的对象
    public GameObjBase m_Owner = null;
    public virtual void AddListener()
    {

    }
    public virtual void RemoveListener()
    {

    }
    public virtual void Init(GameObjBase go)
    {
        m_Owner = go;
    }
    public virtual void Update(float fTime)
    {
        //
    }
    public virtual void Release()
    {
        RemoveListener();
    }
}
