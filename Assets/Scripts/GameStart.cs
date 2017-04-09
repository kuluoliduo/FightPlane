using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ClashGame
{
    //游戏启动入口
    public class GameStart:MonoBehaviour
    {
        public int ResLocal = 0; //0正常，1本地资源，2从文件中取。
        public int  TestScene = 0;
        public float AudioVolume = 0f;
        public string HttpAddress = "";
        public bool ShowDebug = true;
        private GameObject m_objSlider = null;
        private Slider     m_UpdateSlider = null;
        private Text       m_UpdateText = null;
        private float m_time;

        public Transform MainCameraRoot = null;


        // Use this for initialization
        void Start()
        {

            Debuger.EnableLog = ShowDebug;

            GameConfig.m_bResLocal = ResLocal;
            GameConfig.m_iTestScene = TestScene;
            GameConfig.m_Volume = AudioVolume;
            GameConfig.m_HttpAddress = HttpAddress;

            DontDestroyOnLoad(MainCameraRoot);
            GameApp.Instance().PreInit();
            Localization.mInstance.PreInit();

            m_time = Time.time + 1;
        }

        // Update is called once per frame
        private bool bupdate = false;
        void Update()
        {
            if(!bupdate && Time.time>m_time)
            {
                bupdate = true;
            }
        }

    }


}