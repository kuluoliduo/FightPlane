//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

//namespace ClashGame
//{
//    public class WarningSign : MonoBehaviour
//    {
//        private Slider sliderBloodBar = null;
//        private RectTransform recttImage = null;
//        private Transform transTarget = null;
//        uint uiCreatureID = 0;
//        //private RtsObjBase targetCreature = null;


//        // Use this for initialization
//        void Start()
//        {
//            transTarget = transform.GetComponent<AddHUDToTarget>().target;

//            //targetCreature = GameApp.m_WorldManager.GetRtsObj(uiCreatureID);

//        }

//        // Update is called once per frame
//        void Update()
//        {
//            if (transTarget == null || transTarget.gameObject.activeInHierarchy == false)
//            {
//                BloodBarMgr.mInstance.RemoveWarningSign(transTarget);
//                Destroy(gameObject);
//                return;
//            }
//        }

//        public void WarningSignEnable(bool enable)
//        {
//            gameObject.SetActive(enable);
//        }

//    }
//}
