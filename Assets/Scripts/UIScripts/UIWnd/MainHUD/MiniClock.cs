using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ClashGame
{
    public class MiniClock : MonoBehaviour
    {

        public delegate void VoidDelegate();
        public VoidDelegate OnTimeOut;
        public VoidDelegate OnTimeOneEvent;
        public int m_bTimeOne = -1;//不用就不管，小于0就行
        public int beginTime = 0;
        private int showTime = 0;
        public int m_bTimeType = 2;//0是hms,1是hm,2是ms
        public bool m_bActive = false;
        public bool m_bCountDown = true;
        private string h;
        private string m;
        private string s;
        private int hour;
        private int min;
        private int sec;
        private float timeRate = 0.983f;
        private float timeStart = 0f;
        private Text txtTimeShow;

        public void Init()
        {
            txtTimeShow = this.transform.GetComponent<Text>();
            if (txtTimeShow == null)
            {
                Debuger.LogError("What this Script add to is not a UI Text!!");
                return;
            }
            m_bActive = true;
            timeStart = Time.realtimeSinceStartup;
            if (beginTime < 0)
            {
                Debuger.LogError("Are you sure? beginTime is below zero.......");
                beginTime = 0;
            }

            showTime = beginTime;
            ClockRun();
        }


        void Update()
        {

            ClockRun();
        }

        void ClockRun()
        {
            if (m_bActive)
            {
                JiShiQi();
                switch (m_bTimeType)
                {
                    case 1:
                        txtTimeShow.text = h + ":" + m;
                        break;
                    case 0:
                        txtTimeShow.text = h + ":" + m + ":" + s;
                        break;
                    case 2:
                        txtTimeShow.text = m + ":" + s;
                        break;
                    default:
                        txtTimeShow.text = h + ":" + m + ":" + s;
                        break;
                }
            }
        }

        void JiShiQi()
        {
            //Debuger.Log("beginTime=====" + beginTime);
            //if (Time.realtimeSinceStartup - timeStart >= timeRate)
            //{
            //    timeStart = Time.realtimeSinceStartup;
            //    if (beginTime > 0)
            //    {
            //        if (m_bCountDown && beginTime>0)
            //        {
            //            beginTime--;
            //        }
            //        if(!m_bCountDown)
            //        {
            //            beginTime++;
            //        }

            //        if (m_bTimeOne > 0)
            //        {
            //            if (beginTime == m_bTimeOne)
            //            {
            //                ThisOnTimeOneEvent();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        Debuger.Log("倒计时====结束==========================" + Time.time);
            //        beginTime = 0;
            //        ThisOnTimeOut();
            //        m_bActive = false;
            //        return;
            //    }   
            //}
            if (m_bCountDown)
            {
                if (Time.realtimeSinceStartup - timeStart - (beginTime - showTime) > timeRate)
                {
                    showTime = (int)(beginTime - (Time.realtimeSinceStartup - timeStart));
                    if (showTime < 0) showTime = 0;
                }
            }
            if (!m_bCountDown)
            {
                if (Time.realtimeSinceStartup - timeStart - showTime > timeRate)
                {
                    showTime = (int)(Time.realtimeSinceStartup - timeStart);
                    //-------------------------
                }
            }
            if (showTime == m_bTimeOne)
            {
                ThisOnTimeOneEvent();
            }
            hour = showTime / 60 / 60 % 24;
            min = showTime / 60 % 60;
            sec = showTime % 60;
            if (hour < 10)
            {
                h = "0" + hour.ToString();
            }
            else
            {
                h = hour.ToString();
            }
            if (min < 10)
            {
                m = "0" + min.ToString();
            }
            else
            {
                m = min.ToString();
            }
            if (sec < 10)
            {
                s = "0" + sec.ToString();
            }
            else
            {
                s = sec.ToString();
            }
        }

        void ThisOnTimeOut()
        {
            if (OnTimeOut != null)
            {
                OnTimeOut();
            }
            Debuger.Log("Time is Out");
        }

        void ThisOnTimeOneEvent()
        {
            if (OnTimeOneEvent != null)
            {
                OnTimeOneEvent();
            }
            Debuger.Log("OnTimeOneEvent");
        }
    }
}

