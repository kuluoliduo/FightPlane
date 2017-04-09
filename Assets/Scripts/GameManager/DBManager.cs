using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ClashGame
{
    public class DBManager  
    {
       

        public static DBUIWnd m_DBUIWnd;
        public static DBFsm m_DBFsm;

        public static void Init()
        {
           // List<GameRes> dbList = new List<GameRes>();
            
            //GameRes dbres = new GameRes();
            //dbres.m_sResName = "NetRes/DB/Scene/Scene";
            //dbList.Add(dbres);
               
            m_DBUIWnd = new DBUIWnd();
            m_DBUIWnd.LoadData("NetRes/DB/UI/UIWnd");

            m_DBFsm = new DBFsm();
            m_DBFsm.Init();



        }

    }
}
