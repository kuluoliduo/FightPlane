using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ClashGame
{
    public class LoadingWnd : UIBaseWnd
    {
        private Slider slider;
        private Text txtJinDu;
        protected override void OnOpen()
        {
            base.OnOpen();
            slider = WndRootGameobject.transform.Find("Slider").GetComponent<Slider>();
            txtJinDu = WndRootGameobject.transform.Find("JinDu").GetComponent<Text>(); 

            AddEventListener();
        }

        private void AddEventListener()
        {
            
        }

        public void SetSliderValue(int curNum, int maxNum)
        {
            float jindu = curNum / (float)maxNum;
            if(slider != null)
                slider.value = jindu;
            if(txtJinDu != null)
                txtJinDu.text = (int)(jindu * 100) + "%";
        }

        //public void StartGameNtf(PKT_CLIGS_START_GAME_NTF p)
        //{
        //    // 
        //    CloseWnd();
        //    GameApp.m_SceneManager.ChangeScene(10);
        //}
        //public void LoadSceneNtf(PKT_CLIGS_LOAD_SCENE_NTF p)
        //{
        //    //load 
        //    //加载场景   
        //    GameApp.MainRole.GoldNumber = (int)p.GoldNum;
        //    GameApp.MainRole.CurPopulation = p.CurPopulation;
        //    GameApp.MainRole.MaxPopulation = p.MaxPopulation;
        //    GameApp.m_CameraManager.SetCameraWH();
        //    GameApp.MainRole.m_eCamp = (eCampType)p.Team;
        //    for (int i = 0; i < p.ItemNum; i++)
        //    {
        //        Debug.Log("p.ItemList[i].ItemID========" + p.ItemList[i].ItemID);
        //        Debug.Log("p.ItemList[i].ObjID========" + p.ItemList[i].ObjID);
        //        eRtsGroupType groupType = eRtsGroupType.RGT_Self;
        //        if((eCampType)p.ItemList[i].Team == eCampType.CT_Neutral)
        //        {
        //            groupType = eRtsGroupType.RGT_None;
        //        }else
        //        {
        //            if ((eCampType)p.ItemList[i].Team == GameApp.MainRole.m_eCamp)
        //            {
        //                groupType = eRtsGroupType.RGT_Self;
        //            }else
        //            {
        //                groupType = eRtsGroupType.RGT_Enemy;
        //            }
        //        }
        //        RtsObjBase obj = GameApp.m_WorldManager.CreateRtsObj((int)p.ItemList[i].ItemID, p.ItemList[i].ObjID, groupType, eRtsObjType.ROT_Building);
        //        obj.m_ObjInstance.transform.localPosition = new Vector3(p.ItemList[i].CurPos.PosX, obj.m_ObjInstance.transform.localPosition.y, p.ItemList[i].CurPos.PosY);

        //        SetSliderValue(i, p.ItemNum);
        //    }
        //    //创建完角色
        //    GameApp.m_CameraManager.ResetCamera();
        //    GameApp.m_NetHandler.LoadSceneRpt();
        //    Debug.Log("LoadSceneRpt");
        //    //load over send Rpt包
        //}

        //public void GameBeginNtf(PKT_CLIGS_GAME_BEGIN_NTF p)
        //{
        //    //战斗正式开始了
            
        //    LoadingWnd wnd = GameApp.m_UIManager.GetWnd("LoadingWnd") as LoadingWnd;
        //    if (wnd != null && wnd.IsOpen())
        //    {
        //        wnd.CloseWnd();
        //    }
        //}
    }
}
