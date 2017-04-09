using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ClashGame
{
    public class ClashFsmConditions
    {
        private delegate bool FsmCondition(Event_FSM_EF_Event edata, List<string> param);
        private Dictionary<string, FsmCondition> m_FsmConditionDic = new Dictionary<string, FsmCondition>();

        public void Init()
        {
            //m_FsmConditionDic["testCondition"] = this.testCondition;
            //m_FsmConditionDic["CheckSpeed"] = this.CheckSpeed;
              m_FsmConditionDic["CanMove"] = this.CanMove;
            //m_FsmConditionDic["CanNotMove"] = this.CanNotMove;
            //m_FsmConditionDic["CanSkill"] = this.CanSkill;
            //m_FsmConditionDic["CanBreak"] = this.CanBreak;
        }

        public bool AllConditionPass(LinkState linkstate, Event_FSM_EF_Event fsmevent)
        {
            foreach (KeyValuePair<string, List<string>> child in linkstate.m_AllConditions)
            {
                string ConditionName = child.Key;
                List<string> Params = child.Value;

                /*
                // For test
                bool bTrace;
                if(ConditionName == "QteEnd")
                    bTrace = true;
                */

                if (m_FsmConditionDic.ContainsKey(ConditionName))
                {
                    if (!(m_FsmConditionDic[ConditionName](fsmevent, Params)))
                    {
                        return false;
                    }
                }
                else
                {
                    Debug.LogWarning("Don't find " + ConditionName);
                }
            }
            return true;
        }

        private bool testCondition(Event_FSM_EF_Event ml2event, List<string> param)
        {
            return true;
        }

        // ��ǰ״̬�ܷ񱻴��
        //private bool CanBreak(GameEventData ml2event, List<string> param)
        //{
        //    if (ml2event.Receiver != null)
        //        return ((CharacterBase)ml2event.Receiver).m_BnBFsmComp.GetCurState().CanBreak();
        //    return true;
        //}

        //// �жϽ�ɫ�Ƿ����ٶȣ�ҡ���Ƿ��£���������ʵ��������ϣ��򷵻�true
        //private bool CheckSpeed(GameEventData ml2event, List<string> param)
        //{
        //    if (ml2event.Receiver is Player)
        //    {
        //        if (ml2event.Receiver == null || ((Player)ml2event.Receiver).m_InputComp == null)
        //            return false;

        //        bool value = Convert.ToBoolean(param[0]);
        //        bool hasSpeed = (((Player)ml2event.Receiver).m_InputComp.Dir2 != Vector2.zero);
        //        return (value == hasSpeed);
        //    }
        //    return true;
        //}

        // �ܹ��ƶ�
        private bool CanMove(Event_FSM_EF_Event edata, List<string> param)
        {
           //if (ml2event.Receiver != null)
               // return ((CharacterBase)ml2event.Receiver).CanMove();
            return true;
        }

        //// ���ܹ��ƶ�
        //private bool CanNotMove(GameEventData ml2event, List<string> param)
        //{
        //    return !CanMove(ml2event, param);
        //}

        //// �Ƿ��ܹ�ʹ�ü���
        //private bool CanSkill(GameEventData ml2event, List<string> param)
        //{
        //    if (ml2event.Receiver != null)
        //        return ((CharacterBase)ml2event.Receiver).CanSkill();
        //    return true;
        //}


    }
}
