using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eLayerType
{
    LT_Bullet_Blue = 13,
    LT_Bullet_Red = 14,
    LT_Plane_Blue = 15,
    LT_Plane_Red = 16,
}
public class Plane : GameObjBase
{
    public PropertyBase m_Property;
    public eCampType m_ObjCamp = eCampType.CT_Neutral;

    private Transform transRackPoint_0 = null;
    private Transform transRackPoint_1 = null;
    private Transform transRackPoint_2 = null;
    
    public Dictionary<uint, WeaponRack> m_DicWeapon = new Dictionary<uint, WeaponRack>();

    public override void AddListener()
    {
        base.AddListener();
        GameDispatch.AddEventListener(1, DispatchTesh, this);
    }
    public override void RemoveListener()
    {
        base.RemoveListener();
        GameDispatch.RemoveEventListener(1, DispatchTesh, this);
    }
    private void DispatchTesh(GameEventData edata)
    {
        Debug.Log("DispatchTesh");
    }
    public override void Update(float fTime)
    {
        base.Update(fTime);

        foreach (KeyValuePair<uint, WeaponRack> kv in m_DicWeapon)
        {
            kv.Value.Update(fTime);
        }
    }
    public virtual void Create(int dbID, uint instanceID, Vector3 posV3)
    {
        base.Create(dbID, instanceID);
        Debug.Log("Create Plane");
        m_InstanceID = instanceID;
        m_ObjType = eGameObjType.GOT_Plane;
        m_ObjInstance.transform.position = posV3;


        transRackPoint_0 = m_ObjInstance.transform.Find("Point_0");
        transRackPoint_1 = m_ObjInstance.transform.Find("Point_1");
        transRackPoint_2 = m_ObjInstance.transform.Find("Point_2");
    }
    protected override void InitProperty(int dbID)
    {
        base.InitProperty(dbID);
    }
    protected override void InitPrefabPath()
    {
        m_sPrefabPath = "NetRes/Prefabs/ObjPrefab/Plane_1";
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
    }
    public override void Release()
    {
        base.Release();
    }
    public Transform GetRackPoint(int id)
    {
        if(id == 0)
        {
            if(transRackPoint_0 == null)
            {
                Debug.LogError("transRackPoint_0 is null");
            }
            return transRackPoint_0;
        }
        if (id == 1)
        {
            if (transRackPoint_1 == null)
            {
                Debug.LogError("transRackPoint_1 is null");
            }
            return transRackPoint_1;
        }
        if (id == 2)
        {
            if (transRackPoint_2 == null)
            {
                Debug.LogError("transRackPoint_2 is null");
            }
            return transRackPoint_2;
        }
        Debug.LogError("Have Not A RackPoint====" + id);
        return null;
    }
}
