using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ClashGame
{
    public class LoginWnd : UIBaseWnd
    {
        private Transform transCreateRole;
        private InputField inputName;
        protected override void OnOpen()
        {
            base.OnOpen();
            transCreateRole = WndRootGameobject.transform.Find("CreateRole").transform;
            inputName = WndRootGameobject.transform.Find("CreateRole/NameInputField").GetComponent<InputField>();
            transCreateRole.gameObject.SetActive(false);
            AddEventListener();
        }

        private void AddEventListener()
        {
            GameObject loginBtn = WndRootGameobject.transform.Find("EnterBut").gameObject;
            if (loginBtn != null)
            {
                EventTriggerListener.Get(loginBtn).onClick = OnLoginBtnClick;
            }
            GameObject CreateBtn = WndRootGameobject.transform.Find("CreateRole/CreateBtn").gameObject;
            if (CreateBtn != null)
            {
                EventTriggerListener.Get(CreateBtn).onClick = OnCreateBtnClick;
            }
        }

        void OnLoginBtnClick(GameObject go)
        {
            Debuger.Log("OnLoginBtnClick----------------");

            Application.LoadLevelAsync("SceneLevel0");
            GameApp.m_UIManager.OpenWnd("MainHUD");
        }
        void OnCreateBtnClick(GameObject go)
        {
            Debuger.Log("OnCreateBtnClick----------------");
            if(!string.IsNullOrEmpty(inputName.text))
            {
                //GameApp.m_NetHandler.CreateRoleReq(inputName.text, 0);
            }else
            {
                Debug.Log("input name first!~!!");
            }
        }
        public void OnConnectBack()
        {
            string RobotID = GameApp.RobotID;
            Debug.Log("RobotID========" + RobotID);
            //if (RobotID != null)
                //GameApp.m_NetHandler.SendLoginReq(RobotID);
        }
        //public void SendLoginAck(PKT_CLILS_LOGIN_ACK p)
        //{
        //    if (p.AckType == (byte)eAckType.SUCCESS)
        //    {          
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
        public void ToCreateRole()
        {
            transCreateRole.gameObject.SetActive(true);
        }
        //public void CreateRoleAck(PKT_CLIDB_CREATEROLE_ACK p)
        //{
        //    if (p.ErrCode == (byte)eAckType.SUCCESS)
        //    {
        //        Debuger.Log("创建角色成功");
        //        Debuger.Log("CreateRoleAck---SelectRoleReq===============" + Time.realtimeSinceStartup);
        //        GameApp.m_NetHandler.SelectRoleReq(0);
        //        transCreateRole.gameObject.SetActive(false);
        //    }
        //    else if (p.ErrCode == (byte)eAckType.FAIL)
        //    {
        //        //MsgBoxOk.text = "角色名不可用，请重新输入";
        //        //MsgBoxOk.onOkClicked = null;
        //        //MsgBoxOk.Pop();
        //        Debuger.Log("创建角色失败-----------");
        //    }
        //}

    }
}
