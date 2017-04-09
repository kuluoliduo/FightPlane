using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ClashGame
{
    public class MainHUD : UIBaseWnd
    {
        private Text txtTimeClick;
        protected override void OnOpen()
        {
            base.OnOpen();
            txtTimeClick = WndRootGameobject.transform.Find("TimeText").GetComponent<Text>();

            AddEventListener();
        }

        private void AddEventListener()
        {
            GameObject SearchBtn = WndRootGameobject.transform.Find("SearchBtn").gameObject;
            if (SearchBtn != null)
            {
                EventTriggerListener.Get(SearchBtn).onClick = OnSearchBtnClick;
            }
            GameObject RenJiBtn = WndRootGameobject.transform.Find("RenJiBtn").gameObject;
            if (RenJiBtn != null)
            {
                EventTriggerListener.Get(RenJiBtn).onClick = OnRenJiBtnClick;
            }
        }

        void OnSearchBtnClick(GameObject go)
        {
            Debuger.Log("SearchBtn----------------");

            //GameApp.m_NetHandler.StartGameReq(1);//0单人 1多人
        }
        void OnRenJiBtnClick(GameObject go)
        {
            Debuger.Log("OnRenJiBtnClick----------------");

            //GameApp.m_NetHandler.StartGameReq(0);//0单人 1多人
            BattleMgr.Instance.CreateMainPlayer(1);
            CloseWnd();
        }
        //public void StartGameAck(PKT_CLIGS_START_GAME_ACK p)
        //{
        //    if (p.Ack == (byte)eAckType.SUCCESS)
        //    {
        //        //开始计时
        //        MiniClock clock = txtTimeClick.gameObject.GetComponent<MiniClock>();
        //        if (clock == null)
        //        {
        //            clock = txtTimeClick.gameObject.AddComponent<MiniClock>();
        //        }
        //        clock.beginTime = 0;
        //        clock.m_bTimeType = 2;
        //        clock.m_bCountDown = false;
        //        clock.Init();
        //    }
        //    else if (p.Ack == (byte)eAckType.FAIL)
        //    {
        //        //MsgBoxOk.text = "";
        //        //MsgBoxOk.onOkClicked = null;
        //        //MsgBoxOk.Pop();
        //    }
        //}


        //public void SearchBtnAck(PKT_CLILS_LOGIN_ACK p)
        //{
        //    if (p.AckType == (byte)eAckType.SUCCESS)
        //    {
        //        CloseWnd();
        //        GameApp.m_SceneManager.ChangeScene(10);
        //        //GameApp.m_UIManager.OpenWnd("RoleSelectWnd");
        //        //登陆成功
        //        //下一步操作，在收到服务器列表后再继续
        //    }
        //    else if (p.AckType == (byte)eAckType.FAIL)
        //    {
        //        //MsgBoxOk.text = "登录失败";
        //        //MsgBoxOk.onOkClicked = null;
        //        //MsgBoxOk.Pop();

        //        //登陆失败
        //    }
        //    else
        //    {
        //        //MsgBoxOk.text = "重复登陆";
        //        //MsgBoxOk.onOkClicked = null;
        //        //MsgBoxOk.Pop();
        //        //重复登陆
        //    }


        //}

    }
}
