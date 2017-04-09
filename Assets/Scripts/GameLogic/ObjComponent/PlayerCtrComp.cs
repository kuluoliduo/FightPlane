/*
 * 移动组件
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerCtrComp : ObjComponent
{

    public float moveSpeed;

    private float m_fStartTime = 0;
    private float m_fRate = 0.2f;
    public void Init(GameObjBase go, float speed)
    {
        m_Owner = go;
        moveSpeed = speed;
    }
    public override void Update(float fTime)
    {
        //Debug.Log("PlayerCtrComp");
        if (Input.GetKey(KeyCode.W))
        {
            if (m_Owner.m_ObjInstance.transform.position.y < 4)
                m_Owner.m_ObjInstance.transform.Translate(Vector3.up * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (m_Owner.m_ObjInstance.transform.position.y > -4)
                m_Owner.m_ObjInstance.transform.Translate(Vector3.up * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (m_Owner.m_ObjInstance.transform.position.x < 2.5f)
                m_Owner.m_ObjInstance.transform.Translate(Vector3.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GameEventData dt = new GameEventData(1);
            GameDispatch.DispatchEvent(dt, this.m_Owner);
            if (m_Owner.m_ObjInstance.transform.position.x > -2.5f)
                m_Owner.m_ObjInstance.transform.Translate(Vector3.right * -moveSpeed);
        }
        //if (Input.GetKey(KeyCode.Space))
        //{
            if (Time.realtimeSinceStartup - m_fStartTime > m_fRate)
            {
                m_fStartTime = Time.realtimeSinceStartup;
                Bullet bullet = new Bullet();
                bullet.Create(1, BattleMgr.Instance.GetInstanceID(), eGameObjType.GOT_Bullet, eCampType.CT_Blue);
                bullet.m_ObjInstance.transform.position = BattleMgr.Instance.m_Player.GetRackPoint(0).transform.position;
                BattleMgr.Instance.AddBullet(bullet);
            }
        //}
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log(BattleMgr.Instance.m_BulletList.Count);
        }
    }

}

