using UnityEngine;
using System.Collections;

namespace ClashGame
{
    public class StateDead : ClashFsmStateBase
    {
        private float nowtime = 0;
        private float m_delay = 1.5f;
        private bool NeedDelay = false;

        private float sinktime=0; //开始陷入地面的时间；
        private float destorytime=0;//销毁时间；
        private Vector3 sinkspeed=new Vector3(0,-0.8f,0) ;//下陷速度；
        public StateDead()
            : base()
        {
            m_StateName   = "StateDead";
            
        }

        public override void OnBegin(Event_FSM_EF_Event edata) 
        {
            base.OnBegin(edata);
            //GetCreaturetOwner().m_CompWeapon.ClearAttackList();
            //GetCreaturetOwner().m_CompBaseMovement.SetStop();
            //float alength = 0;
            //alength = GetOwner().SetAnimation("dead");
            //if (GetOwner().animator != null)
            //{
            //    GetOwner().animator.SetBool("dead", true);
            //}

            //sinktime = Time.time+ alength + 1f;
            //destorytime = sinktime + 2f;

            //BloodBarMgr.mInstance.RemoveBloodBar(GetOwner().m_InstanceID);
            //BloodBarMgr.mInstance.RemoveWarningSign(GetOwner().m_InstanceID);

            //Transform shadow = GetOwner().m_ObjInstance.transform.Find("P-SoldierShadow");
            //if(shadow != null)
            //{
            //    shadow.gameObject.SetActive(false);
            //}

            //if (GetOwner().m_Property.m_RtsObjData.m_iUnitType == 3 || GetOwner().m_Property.m_RtsObjData.m_iUnitType == 4) //飞机暂时直接删。
            //{
            //    GameApp.m_WorldManager.DestoryRtsObjLastUpdate(GetOwner().m_InstanceID);
            //}

        }

        public override void Update(float deltatime)
        {
            base.Update(deltatime);
            if(Time.time>sinktime)
            {
                //GetOwner().SetPosition(GetOwner().Position + sinkspeed * deltatime);
            }
            if(Time.time>destorytime)
            {
                //GameApp.m_WorldManager.DestoryRtsObjLastUpdate(GetOwner().m_InstanceID);
            }
        }


        public override bool CanBreak() { return false; }
        // 本状态下能否移动
        public override bool CanMove() { return false; }
        // 本状态下能否放技能
        public override bool CanSkill() { return false; }
        // Can be attacked or not
        public override bool CanBeAttack() { return false; }
    }
}
