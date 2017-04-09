using UnityEngine;
using System.Collections;

namespace ClashGame
{
    public class RallyPointCtr : MonoBehaviour
    {
        private float fStartTime;
        public float m_fExistTime = 2;
        // Use this for initialization
        void Start()
        {
            fStartTime = Time.realtimeSinceStartup;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.realtimeSinceStartup - fStartTime > m_fExistTime)
                Destroy(gameObject);
        }
    }
}
