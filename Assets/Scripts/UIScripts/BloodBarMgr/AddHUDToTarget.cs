using UnityEngine;
using System.Collections;

namespace ClashGame
{
    /// <summary>
    /// 为3d对象添加UI
    /// </summary>
    public class AddHUDToTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offSet;
        Transform mTrans;
        Camera mGameCam;
        Camera mUICam;
        Vector3 mPos;
        bool mVisible = true;

        void Start()
        {
            if (target == null) { Destroy(gameObject); return; }
            mTrans = transform;

            mGameCam = Camera.main;
            mUICam = GameApp.m_UIManager.uiCamera;

            UpdatePos();
        }

        void Update()
        {
            UpdatePos();
        }

        void UpdatePos()
        {
            if (target == null) { Destroy(gameObject); return; }

            if (mGameCam == null)
                mGameCam = Camera.main;
            if (mGameCam == null)
                return;
            //Debuger.Log("target.position" + target.position);
            mPos = mGameCam.WorldToViewportPoint(target.position + offSet);

            //bool visible = (mPos.z > 0f && mPos.x > 0f && mPos.x < 1f && mPos.y > 0f && mPos.y < 1f);

            mPos = mUICam.ViewportToWorldPoint(mPos);
            mPos.z = 700f;
            mTrans.position = mPos;
            //Debuger.Log("Screen.width" + Screen.width + "Screen.height" + Screen.height);
            //mTrans.GetComponent<RectTransform>().localPosition = new Vector3(960 * (mPos.x - 0.5f), 640* (mPos.y - 0.5f), 0);
        }
    }
}
