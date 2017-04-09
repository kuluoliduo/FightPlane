//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

//namespace ClashGame
//{
//    public enum eObjSize
//    {
//        OS_Small = 0,
//        OS_Normal,
//        OS_Large,
//        OS_Huge
//    }
//    public class CoupleTransform
//    {
//        public Transform target;
//        public Transform bubble;
//    }
//    public class BloodBar : MonoBehaviour
//    {
//        private Slider sliderBloodBar = null;
//        private RectTransform recttImage = null;
//        private Transform transTarget = null;
//        uint uiCreatureID = 0;
//        private RtsObjBase targetCreature = null;
//        private Transform transDamageNum = null;

//        private uint oriBloodNum;
//        private uint curBloodNum;

//        public void RefreshBloodBar(uint targetID, eObjSize size)
//        {
//            int barWight = 20;
//            uiCreatureID = targetID;
//            switch (size)
//            {
//                case eObjSize.OS_Small:
//                    barWight += 20;
//                    break;
//                case eObjSize.OS_Normal:
//                    barWight += 40;
//                    break;
//                case eObjSize.OS_Large:
//                    barWight += 60;
//                    break;
//                case eObjSize.OS_Huge:
//                    barWight += 80;
//                    break;
//            }
//            sliderBloodBar = transform.Find("Slider").GetComponent<Slider>();
//            sliderBloodBar.GetComponent<RectTransform>().sizeDelta = new Vector2(barWight, 12f);
//        }
//        // Use this for initialization
//        void Start()
//        {

//            transDamageNum = transform.FindChild("DamageNum");
//            transTarget = transform.GetComponent<AddHUDToTarget>().target;
//            //Debuger.Log("Time.time=====" + Time.time);
//            targetCreature = GameApp.m_WorldManager.GetRtsObj(uiCreatureID);
//            FreshBloodNum();
//            curBloodNum = (uint)targetCreature.m_Property.m_iCurHP;
//            oriBloodNum = curBloodNum;
//        }

//        // Update is called once per frame
//        void Update()
//        {
//            if (transTarget == null || transTarget.gameObject.activeInHierarchy == false)
//            {
//                BloodBarMgr.mInstance.RemoveBloodBar(transTarget);
//                Destroy(gameObject);
//                return;
//            }
//            FreshBloodNum();
//            //curBloodNum = (uint)targetCreature.m_Property.m_iCurHP;
//            //if (curBloodNum != oriBloodNum)
//            //{
//            //    ShowDamageNum((int)curBloodNum - (int)oriBloodNum);
//            //}
//            //oriBloodNum = curBloodNum;
//        }

//        //FreshBloodNum
//        void FreshBloodNum()
//        {
//            if (targetCreature != null)
//            {
//                sliderBloodBar.value = targetCreature.m_Property.m_iCurHP / (float)targetCreature.m_Property.m_RtsObjData.m_iMaxBlood;
//            }
//        }
//        void ShowDamageNum(int damage)
//        {
//            GameObject btn = GameObject.Instantiate(transDamageNum.gameObject);
//            btn.transform.SetParent(transform);
//            btn.transform.localPosition = new Vector3(0, 0, 0);

//            btn.transform.localScale = Vector3.one;
//            btn.GetComponent<RectTransform>().sizeDelta = Vector2.one;
//            string strDam = damage.ToString();
//            if (damage > 0)
//            {
//                strDam = "+" + strDam;
//                //btn.transform.Find("Text").GetComponent<Text>().material.color = new Color(68,68,233,255);修改除A通道外的其他值,会使UI崩溃
//                btn.transform.Find("BlueText").GetComponent<Text>().text = strDam;
//                btn.transform.Find("BlueText").gameObject.SetActive(true);
//                btn.transform.Find("RedText").gameObject.SetActive(false);
//            }
//            else
//            {
//                btn.transform.Find("RedText").GetComponent<Text>().text = strDam;
//                btn.transform.Find("RedText").gameObject.SetActive(true);
//                btn.transform.Find("BlueText").gameObject.SetActive(false);
//            }
//            btn.transform.Find("RedText").GetComponent<Text>().text = strDam;
//            btn.SetActive(true);
//        }

//    }
//}
