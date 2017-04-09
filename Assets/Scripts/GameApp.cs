using UnityEngine;
using System.Collections;
 

namespace ClashGame
{
    //游戏逻辑全局管理
    public class GameApp
    {
        
        //static public SceneManager m_SceneManager = null;
        //static public WorldManager m_WorldManager = null;
       
        static public UIManager m_UIManager = null;
        
        //static public AudioManager m_AudioManager = null;
        //static public CameraManager m_CameraManager = null;

        //static public NetHandler m_NetHandler = null;
        static public string RobotID = SystemInfo.deviceUniqueIdentifier;

        //static public Role MainRole=null;
     
        private static GameApp m_Instance;
        public static GameApp Instance()
        {
            if (m_Instance == null)
            {
                 new GameApp();
            }
            return m_Instance;
        }
        private static GameObject gameapp = null;
        public GameObject GetRoot()
        {
            return gameapp;
        }
       
        private GameApp()
        {
            if (m_Instance != null)
            {
                 return;
            }
            m_Instance = this;
        }

        public void PreInit()
        {
            gameapp = new GameObject("GameApp");
            Object.DontDestroyOnLoad(gameapp.transform);

            //m_NetHandler = gameapp.AddComponent<NetHandler>();
            AfterInit();
        }

        public void AfterInit()
        {
            DBManager.Init();
            m_UIManager = gameapp.AddComponent<UIManager>();
            m_UIManager.Init();
            //换场景
            //Application.LoadLevelAsync("SceneLevel0");
        }

        public void test()
        {
            Debug.Log("------------------------------------------------------------------------------");
        }

    }
}
