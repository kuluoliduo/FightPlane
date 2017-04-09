using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClashGame
{
    public class StateIdle : ClashFsmStateBase
    {
        public StateIdle()  : base()
        {
            m_StateName   = "StateIdle";
           // m_DefActionID = (int)ePlayerAnima.PA_idle;
        }

        public override void OnBegin(Event_FSM_EF_Event edata)
        {            
            base.OnBegin(edata);
         //   Debug.Log("StateIdle-----------------------");
            //GetOwner().SetAnimation("idle");
            //m_GameObject.MoveComp.VelDir.Stop();
            // m_GameObject.MoveComp.Action.ChangeAction(GetAction(), false); 
            //switch (m_Creature.m_MoveComp.GetLastMoveState())
            //{
            //    case eMoveStates.MS_Up:
            //        {
            //            ((Creature)m_GameObject).m_AvatarComp.SetAction((int)ePlayerAnima.PA_idle_up);
            //        } break;
            //    case eMoveStates.MS_Down:
            //        {
            //            ((Creature)m_GameObject).m_AvatarComp.SetAction((int)ePlayerAnima.PA_idle_down);
            //        } break;
            //    case eMoveStates.MS_Left:
            //        {
            //            ((Creature)m_GameObject).m_AvatarComp.SetAction((int)ePlayerAnima.PA_idle_left);
            //        } break;
            //    case eMoveStates.MS_Right:
            //        {
            //            ((Creature)m_GameObject).m_AvatarComp.SetAction((int)ePlayerAnima.PA_idle_right);
            //        } break;
            //}
            //Debug.Log(GetOwner().m_Property.m_RtsObjData.m_strName + GetOwner().m_InstanceID + " begin idle");
       }

        public override void OnEnd()
        {
            base.OnEnd();
            //Debug.Log(GetOwner().m_Property.m_RtsObjData.m_strName + GetOwner().m_InstanceID + " end idle");
        }

        protected override void InitDataFromNet(Event_FSM_EF_Event edata) 
        {
            //int mstate =  edata.GetUserData<int>(1);
            //m_Creature.m_MoveComp.SettLastMoveState((eMoveStates)mstate);
           // m_Creature.m_MoveComp.SetMoveState(eMoveStates.MS_None);
           // Debug.Log("InitDataFromNet=" + m_Creature.m_MoveComp.GetLastMoveState());
        }

        public override string BuildSyncData()
        {
            //Debug.Log("BuildSyncData idele="+m_Creature.m_MoveComp.GetLastMoveState());
            //return SyncProtocalComp.BuildData((int)m_Creature.m_MoveComp.GetLastMoveState());
            return null;
        }

        public override void Update(float deltatime)
        {
           //// Debug.Log(m_GameObject.m_NetInstanceID+"//"+m_GameObject.m_ObjInstance.name);
           //if (m_GameObject is Player)
           //{
             //  TickMainPlayer();
          // }
            //if (m_GameObject.MoveComp.OwnerIsType(EOwnerType.AutoPlayer))
            //{
            //    TickAutoPlayer();
            //}
            //else if (m_GameObject.MoveComp.OwnerIsType(EOwnerType.NetPlayer))
            //{
            //    TickNetPlayer();
            //}
            //else if (m_GameObject.MoveComp.OwnerIsType(EOwnerType.NetMonster) || m_GameObject.MoveComp.OwnerIsType(EOwnerType.MasterMonster))
            //{
            //    TickMonster();
            //}
            //if (!GetCreaturetOwner().m_CompBaseMovement.IsStop())
            //{
            //    Event_FSM_EF_Event fsevent = new Event_FSM_EF_Event(eFsmEvent.FE_RUN);
            //    GameDispatch.DispatchEvent(fsevent, GetCreaturetOwner());
            //}
            base.Update(deltatime);
        }

        private void TickMainPlayer()
        {
             // 如果检测到能够移动，并且按键按下，则切换为Run
            //if (GameApp.m_UIManager.m_Controller.GetCurMoveState()!=eMoveStates.MS_None)
            //{
            //    Event_FSM_EF_Event fsevent = new Event_FSM_EF_Event(eFsmEvent.FE_RUN);
            //    fsevent.PushUserData<eMoveStates>(GameApp.m_UIManager.m_Controller.GetCurMoveState());
            //    GameDispatch.DispatchEvent(fsevent,m_GameObject);
            //}
        }
    }
}
