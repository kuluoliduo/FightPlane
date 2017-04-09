using UnityEngine;
using System.Collections;

namespace ClashGame
{
    public class StateFire : ClashFsmStateBase
    {
        private bool  m_bLoop = true;
        private float m_fEndTime = 0;
        private float m_fStartAtkTime = 0;
        private float m_fAtkInterval = 0;
        private Animator mAnimator;

         public StateFire()  : base()
        {
            m_StateName = "StateFire";
            
        }

        public override void OnBegin(Event_FSM_EF_Event edata)
        {
            base.OnBegin(edata);
          //  Debug.Log("OnBegin(Event_FSM_EF_Event edata)");
            float alength = 0;
            //alength=GetOwner().SetAnimation("attack");
            //mAnimator = GetOwner().animator;
            //m_fAtkInterval = GetOwner().m_Property.m_RtsObjData.m_iATKRate * 0.001f;
            //m_fStartAtkTime = GetCreaturetOwner().GetRtsArmyData().m_fStartAtkTime;
            //m_fEndTime = Time.realtimeSinceStartup + m_fAtkInterval + m_fStartAtkTime;
            //GetCreaturetOwner().m_CompWeapon.AddBulletTime(m_fStartAtkTime);
            //Debug.Log("m_fStartAtkTime " + m_fStartAtkTime + " interval " + m_fAtkInterval);
            if (mAnimator != null)
            {
                //Debug.Log(GetOwner().m_ObjInstance.name + "fire begin");
                mAnimator.SetBool("attack", true);
            }
          
            //m_bLoop = edata.GetUserData<bool>(0);
            //m_fEndTime = Time.realtimeSinceStartup +alength+ 0.1f;

            //Debug.Log(GetOwner().m_Property.m_RtsObjData.m_strName + GetOwner().m_InstanceID + " begin fire");
        }
         
        public override void OnEnd()
        {
             base.OnEnd();

             if (mAnimator != null)
             {
                 //Debug.Log(GetOwner().m_ObjInstance.name + "fire end");
                 mAnimator.SetBool("attack", false);
                 mAnimator.SetBool("attackidle", false);
             }
             //Debug.Log(GetOwner().m_Property.m_RtsObjData.m_strName + GetOwner().m_InstanceID + " end fire"+Time.realtimeSinceStartup);
        }

        public override void Update(float deltatime)
        {
            base.Update(deltatime);

            if (Time.realtimeSinceStartup > m_fEndTime && mAnimator != null)
            {
                mAnimator.SetBool("attackidle", false);
                //GetCreaturetOwner().m_CompWeapon.AddBulletTime();
                m_fEndTime = Time.realtimeSinceStartup + m_fAtkInterval;
            }
            //if(!m_bLoop)
            //{
            //    if(Time.realtimeSinceStartup>m_fEndTime)
            //    {
            //        Debug.Log("dispatch idle");
            //        Event_FSM_EF_Event fsevent = new Event_FSM_EF_Event(eFsmEvent.FE_IDLE);
            //        GameDispatch.DispatchEvent(fsevent, GetCreaturetOwner());
            //    }
            //}
        }


        public override bool CanBreak() { return false; }
        // 本状态下能否移动
        public override bool CanMove() { return false; }
        // 本状态下能否放技能
        public override bool CanSkill() { return true; }
        // Can be attacked or not
        public override bool CanBeAttack() { return true; }
    }
}
