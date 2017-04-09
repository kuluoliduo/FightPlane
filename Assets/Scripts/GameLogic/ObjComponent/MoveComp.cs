/*
 * 移动组件
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveComp : ObjComponent 
{

    private float moveSpeed = 0.05f;
    public void Init(GameObjBase go, float speed)
    {
        m_Owner = go;
        moveSpeed = speed;
    }
    public override void Update(float fTime)
    {
        //Debug.Log(m_Owner.transform.position);
        m_Owner.m_ObjInstance.transform.Translate(Vector3.up * moveSpeed);
    }

}
