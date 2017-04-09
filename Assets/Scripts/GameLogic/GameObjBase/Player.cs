using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : Plane
{

    public override void Update(float fTime)
    {
        base.Update(fTime);
    }
    public override void Create(int dbID, uint instanceID, Vector3 posV3)
    {
        base.Create(dbID, instanceID,posV3);
        Debug.Log("Create Player");
        m_ObjCamp = eCampType.CT_Blue;
        //m_ObjInstance.transform.position = posV3;
        PlayerCtrComp comp = (PlayerCtrComp)AddComponent("PlayerCtrComp");
        comp.Init(this, 0.05f);

    }
    protected override void InitProperty(int dbID)
    {
        base.InitProperty(dbID);
    }
    protected override void InitPrefabPath()
    {
        base.InitPrefabPath();
    }
    protected override void InstantiateObj()
    {
        base.InstantiateObj();
        //m_ObjInstance = GameObject.Instantiate(Resources.Load(m_sPrefabPath)) as GameObject;
    }
    protected override void AddBaseComp()
    {
        base.AddBaseComp();
        //
    }
}
