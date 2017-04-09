/*
 * 子弹
 */
using UnityEngine;
using System.Collections;

public class Bullet : GameObjBase
{
    public override void Update(float fTime)
    {
        //Debug.Log(m_CompList.Count);
        base.Update(fTime);
        if( (m_ObjInstance.transform.position.y > 4) || (m_ObjInstance.transform.position.y < -4) )
        {
            BattleMgr.Instance.RemoveBullet(this);
            Release();
        }
    }
    public void Create(int dbID, uint instanceID, eGameObjType egot, eCampType ect)
    {
        base.Create(dbID, instanceID);
        //Debug.Log("Create GameObjBase");
        m_InstanceID = instanceID;
        m_ObjType = egot;
    }
    protected override void InitProperty(int dbID)
    {
        base.InitProperty(dbID);
    }
    protected override void InitPrefabPath()
    {
        m_sPrefabPath = "NetRes/Prefabs/ObjPrefab/Bullet_1";
    }
    protected override void InstantiateObj()
    {
        base.InstantiateObj();
        //Debug.LogError("m_sPrefabPath---------" + m_sPrefabPath);
        m_ObjInstance = GameObject.Instantiate(Resources.Load(m_sPrefabPath)) as GameObject;
    }
    protected override void AddBaseComp()
    {
        base.AddBaseComp();
        MoveComp comp = AddComponent("MoveComp") as MoveComp;
        comp.Init(this,0.02f);
    }
    public override void Release()
    {
        base.Release();
    }
}
