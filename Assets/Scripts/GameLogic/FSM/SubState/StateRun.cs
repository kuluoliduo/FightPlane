using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClashGame
{
    public class StateRun : ClashFsmStateBase
    {
     //   protected eMoveStates m_LastMoveStates = eMoveStates.MS_None; 
        public StateRun()
        {
            m_StateName = "StateRun";
            m_DefActionID = -1;
        }

        public override void SetGameObject(GameObjBase obj)
        {
            base.SetGameObject(obj);
        }

        public override void OnBegin(Event_FSM_EF_Event gevent)
        {
            base.OnBegin(gevent);
            //GetCreaturetOwner().m_CompWeapon.ClearAttackList();
            //GetOwner().SetAnimation("move");
            //if (GetOwner().animator != null)
            //{
            //    GetOwner().animator.SetBool("move", true);
            //}
            //Debug.Log(GetOwner().m_Property.m_RtsObjData.m_strName+GetOwner().m_InstanceID+" begin run");
        }

        public override void OnEnd()
        { 
            base.OnEnd();

            //if (GetOwner().animator != null)
            //{
            //    GetOwner().animator.SetBool("move", false);
            //}
            //Debug.Log(GetOwner().m_Property.m_RtsObjData.m_strName + GetOwner().m_InstanceID + " end run");
        }

        //private void ChangeAnima(eMoveStates cms,bool bsentsync=false)
        //{
            
        //    if (m_LastMoveStates == cms)
        //        return;
        //    m_LastMoveStates = cms;
        //    switch (m_LastMoveStates)
        //    {
        //        case eMoveStates.MS_Up:
        //            {
        //                ((Creature)m_GameObject).m_AvatarComp.SetAction((int)ePlayerAnima.PA_run_up);
        //            } break;
        //        case eMoveStates.MS_Down:
        //            {
        //                ((Creature)m_GameObject).m_AvatarComp.SetAction((int)ePlayerAnima.PA_run_down);
        //            } break;
        //        case eMoveStates.MS_Left:
        //            {
        //                ((Creature)m_GameObject).m_AvatarComp.SetAction((int)ePlayerAnima.PA_run_left);
        //            } break;
        //        case eMoveStates.MS_Right:
        //            {
        //                ((Creature)m_GameObject).m_AvatarComp.SetAction((int)ePlayerAnima.PA_run_right);
        //            } break;
        //    }
           
        // //   m_Creature.m_SyncProtocalComp.SendSyncData(((int)m_LastMoveStates).ToString(), false);
        //}
        //protected override void DealDataFromNet(Event_FSM_EF_Event edata)
        //{
        //    m_LastMoveStates = eMoveStates.MS_None; //强制改变动作
        //    List<string> data = edata.GetUserData<List<string>>(1);
            
        //    int mstate = int.Parse(data[0]);
        //    ChangeAnima((eMoveStates)mstate);
        //    m_Creature.m_MoveComp.SetMoveState((eMoveStates)mstate);
        //}

        //protected override void InitDataFromLocal(Event_FSM_EF_Event edata)
        //{
        //    eMoveStates mstate = edata.GetUserData<eMoveStates>(0);
        //    ChangeAnima(mstate);
        //   // Debug.Log("InitDataFromLocal time="+Time.realtimeSinceStartup);
        //}

        //protected override void InitDataFromNet(Event_FSM_EF_Event edata)
        //{
        //    m_LastMoveStates = eMoveStates.MS_None; //强制改变动作
        //    int mstate  = edata.GetUserData<int>(1);
        //    ChangeAnima((eMoveStates)mstate);
        //}

        //public override string BuildSyncData()
        //{
        //    return SyncProtocalComp.BuildData((int)m_LastMoveStates);
        //}

        //private void TickMainPlayer()
        //{
        //    if (GameApp.m_UIManager.m_Controller.GetCurMoveState() == eMoveStates.MS_None)
        //    {
        //        m_LastMoveStates = eMoveStates.MS_None;
        //        Event_FSM_EF_Event fsevent = new Event_FSM_EF_Event(eFsmEvent.FE_IDLE);
        //        GameDispatch.DispatchEvent(fsevent, m_GameObject);
        //    }
        //    else 
        //    {
        //        ChangeAnima(GameApp.m_UIManager.m_Controller.GetCurMoveState());
        //       // m_Creature.m_SyncProtocalComp.SendIntervalData();
        //    }
        //}

        //private void TickNetPlayer()
        //{
        //    ChangeAnima(m_Creature.m_MoveComp.GetLastMoveState());
        //}


        //private void TickNpcPlayer()
        //{
        //    ChangeAnima(m_Creature.m_MoveComp.GetLastMoveState());
        //}

        //private void TickMonster()
        //{
        //    ChangeAnima(m_Creature.m_MoveComp.GetLastMoveState());
        //}

        //private void TickPet()
        //{
        //    ChangeAnima(m_Creature.m_MoveComp.GetLastMoveState());
        //}
       public override void Update(float deltatime)
         {
             //if (GetCreaturetOwner().m_CompBaseMovement.IsStop() && GetCreaturetOwner().m_CompBaseMovement.m_bNetStop)
             //{
             //    Event_FSM_EF_Event fsevent = new Event_FSM_EF_Event(eFsmEvent.FE_IDLE);
             //    GameDispatch.DispatchEvent(fsevent, GetCreaturetOwner());
             //}
        //    base.Update(deltatime);

        //    if (m_GameObject is Player)
        //    {
        //        TickMainPlayer();
        //    }
        //    else if (m_GameObject is NetPlayer)
        //    {
        //        TickNetPlayer();
        //    }
        //    else if (m_GameObject is NpcPlayer)
        //    {
        //        TickNpcPlayer();
        //    }
        //    else if (m_GameObject is Monster)
        //    {
        //        TickMonster();
        //    }
        //    else if(m_GameObject is Pet)
        //    {
        //        TickPet();
        //    }
         }

        //public override void OnEnd()
        //{
        //    base.OnEnd();
        //    m_LastMoveStates = eMoveStates.MS_None;
        //}
    }
}
