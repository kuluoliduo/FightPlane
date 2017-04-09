using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleMgr : MonoBehaviour
{
    static private GameObject battleMgr;
    static private BattleMgr _instance;

    //
    public Player m_Player = null;
    public Dictionary<int, Plane> m_EnemyDic = new Dictionary<int, Plane>();
    public List<Bullet> m_BulletList = new List<Bullet>();
    private uint instanceIDRoot  = 0;

    static public BattleMgr Instance
    {
      get 
      { 
          if(_instance == null)
          {
              battleMgr = new GameObject("_BattleMgr");
              DontDestroyOnLoad(battleMgr);
              _instance = battleMgr.AddComponent<BattleMgr>();
          }
          return _instance;
      }
    }

    public void BattleBegin()
    {
        //
    }
    public void BattleEnd()
    {
        //
    }
    void Update()
    {
        m_Player.Update(0);
        foreach(KeyValuePair<int, Plane> kv in m_EnemyDic)
        {
            kv.Value.Update(0);
        }
        for (int i = 0; i < m_BulletList.Count;i++ )
        {
            m_BulletList[i].Update(0);
        }
    }
    public void CreateMainPlayer(int dbID)
    {
        //Plane plane = new Plane();
        //plane.Create(dbID, (uint)instanceIDRoot++, Vector3.one * 2);
        m_Player = new Player();
        m_Player.Create(dbID, GetInstanceID(), Vector3.zero);
    }
    public void CreateEnemy(int dbID)
    {
        //
    }
	public uint GetInstanceID()
    {
        return ++instanceIDRoot;
    }
    public void AddBullet(Bullet bu)
    {
        for (int i = 0; i < m_BulletList.Count; i++)
        {
            if(m_BulletList[i].m_InstanceID == bu.m_InstanceID)
            {
                Debug.LogError("instanceID is same");
                return;
            }
        }
        m_BulletList.Add(bu);
    }
    public void RemoveBullet(Bullet bu)
    {
        for (int i = 0; i < m_BulletList.Count; i++)
        {
            if (m_BulletList[i].m_InstanceID == bu.m_InstanceID)
            {
                m_BulletList.RemoveAt(i);
            }
        }
    }
}
