using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ClashGame
{
    public class BattleOverWnd : UIBaseWnd
    {
        private Image imgWin;
        private Image imgLose;
        protected override void OnOpen()
        {
            base.OnOpen();
            imgWin = WndRootGameobject.transform.Find("WinImg").GetComponent<Image>();
            imgLose = WndRootGameobject.transform.Find("LoseImg").GetComponent<Image>();
            imgWin.gameObject.SetActive(false);
            imgLose.gameObject.SetActive(false);

            AddEventListener();
        }

        private void AddEventListener()
        {
            GameObject BackBtn = WndRootGameobject.transform.Find("BackBtn").gameObject;
            if (BackBtn != null)
            {
                EventTriggerListener.Get(BackBtn).onClick = OnBackBtnClick;
            }
        }

        void OnBackBtnClick(GameObject go)
        {
            GameApp.m_UIManager.OpenWnd("MainHUD");
        }


        //public void GameEndNtf(PKT_CLIGS_GAME_END_NTF p)
        //{
        //    OpenWnd();
        //    GameApp.m_WorldManager.Pause();
        //    if (p.Type == 0)//正常结束，1是非正常结束
        //    {
        //        if (p.Loser == (int)GameApp.MainRole.m_eCamp)//gamebegin 的Team
        //        {
        //            imgLose.gameObject.SetActive(true);
        //        }
        //        else
        //        {
        //            imgWin.gameObject.SetActive(true);
        //        }
        //    }
        //}

    }
}
