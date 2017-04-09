//using UnityEngine;
//using System;
//using System.IO;
//using System.Collections;
//using System.Collections.Generic;
//using LitJson;

//namespace ClashGame
//{
//    public class RtsObjData
//    {
//        public int             m_iID;
//        public string          m_strName;
//        public string          m_strModelName;
        
//        public List<int>       m_listTargetInOrder;//目标顺序
//        public int              m_iUnitType;//单位类型
//        public int              m_iBaseLevel;//对应基地等级
//        public string           m_sExplain;//说明
//        public string           m_sSkillExplain;//技能说明
//        public int              m_iNumber;//数量
//        public int              m_iATKNumber;//攻击力
//        public int              m_iATKRange;//攻击范围
//        public int              m_iATKRate;//攻速
//        public int              m_iMaxBlood;//血量
//        public int              m_iPrice;//造价
//        public int              m_iProTime;//生产时间
//        public float            m_fRad;//半径

    
//        public string           m_sParameter0;//扩充用
//    }
//    public class RtsArmyData : RtsObjData
//    {
//        public int              m_iSenseRange;//感应距离
//        public float            m_fMoveSpeed;//移速
//    }
//    public class RtsBuildingData : RtsObjData
//    {
//        //
//    }

//    public class DBRtsObj
//    {
//        protected Dictionary<int, RtsArmyData> m_dicArmyData = new Dictionary<int, RtsArmyData>();
//        protected Dictionary<int, RtsBuildingData> m_dicBuildingData = new Dictionary<int, RtsBuildingData>();

//        public Dictionary<int, RtsArmyData> GetArmyDataDic()
//        {
//            return m_dicArmyData;
//        }
//        public Dictionary<int, RtsBuildingData> GetBuildingDataDic()
//        {
//            return m_dicBuildingData;
//        }
//        public RtsObjData Get(int id, eRtsObjType objType)
//        {
            
//            if (objType == eRtsObjType.ROT_Creature)
//            {
//                RtsArmyData item = null;
//                if (m_dicArmyData.TryGetValue(id, out item) == true)
//                {
//                    return (item as RtsArmyData);
//                }
//                else
//                {
//                    Debuger.LogWarning("<m_dicArmyData.Get Failed> id not exist, id = " + id);
//                    return null;
//                }
//            }else
//            {
//                RtsBuildingData item = null;
//                if (m_dicBuildingData.TryGetValue(id, out item) == true)
//                {
//                    return (item as RtsBuildingData);
//                }
//                else
//                {
//                    Debuger.LogWarning("<m_dicBuildingData.Get Failed> id not exist, id = " + id);
//                    return null;
//                }
//            }
//        }

//        public bool LoadData(string strFilename)
//        {
//            string filepath = "NetRes/DB/Unit/army";
//            GameRes res = new GameRes(filepath);
//            res.m_eCatchType = GameRes.eCatchType.CT_None;
//            res = ResourcesManager.LoadObj(res);
//            TextAsset textAsset = (TextAsset)res.m_Object;
//            if (textAsset == null)
//            {
//                Debuger.LogError("Load dbrtsobj DB Failed!");
//                return false;
//            }
//            string strLine = textAsset.text;

//            //Debug.Log(strLine);
//            JsonData jd = JsonMapper.ToObject(strLine);
//            if (jd.IsArray)
//            {
//                for (int i = 0; i < jd.Count; i++)
//                {
//                    RtsArmyData bd = new RtsArmyData();
//                    bd.m_iID = StrToInt((string)jd[i]["id"]);
//                    //Debug.Log(bd.m_iID);
//                    bd.m_strName = (string)jd[i]["name"]; 
//                    bd.m_strModelName = (string)jd[i]["model"];
//                    bd.m_listTargetInOrder = StrToIntList((string)jd[i]["target"]); 
//                    bd.m_iUnitType = StrToInt((string)jd[i]["type"]);
//                    bd.m_iBaseLevel = StrToInt((string)jd[i]["baselevel"]); 
//                    bd.m_sExplain = (string)jd[i]["explain"]; 
//                    bd.m_sSkillExplain = (string)jd[i]["skill"]; 
//                    bd.m_iNumber = StrToInt((string)jd[i]["number"]);
//                    bd.m_iATKNumber = StrToInt((string)jd[i]["atkl"]); 
//                    bd.m_iATKRange = StrToInt((string)jd[i]["atkrange"]); 
//                    bd.m_iATKRate = StrToInt((string)jd[i]["atkrate"]); 
//                    bd.m_iMaxBlood = StrToInt((string)jd[i]["blood"]); 
//                    bd.m_iPrice = StrToInt((string)jd[i]["price"]); 
//                    bd.m_iProTime = StrToInt((string)jd[i]["protime"]);
//                    bd.m_fRad = StrToFloat((string)jd[i]["rad"]); 
//                    bd.m_iSenseRange = StrToInt((string)jd[i]["sense"]); 
//                    bd.m_fMoveSpeed = StrToFloat((string)jd[i]["speed"]);             
//                    if (!m_dicArmyData.ContainsKey(bd.m_iID))
//                        m_dicArmyData.Add(bd.m_iID,bd);
//                    //Debug.Log(bd.m_iID);
//                }
//            }

//            string filepath1 = "NetRes/DB/Unit/building";
//            GameRes res1 = new GameRes(filepath1);
//            res1.m_eCatchType = GameRes.eCatchType.CT_None;
//            res1 = ResourcesManager.LoadObj(res1);
//            TextAsset textAsset1 = (TextAsset)res1.m_Object;
//            string strLine1 = textAsset1.text;

//            //Debug.Log(strLine1);
//            jd = JsonMapper.ToObject(strLine1);
//            if (jd.IsArray)
//            {
//                for (int i = 0; i < jd.Count; i++)
//                {
//                    RtsBuildingData bd = new RtsBuildingData();
//                    bd.m_iID = StrToInt((string)jd[i]["id"]);
//                    bd.m_strName = (string)jd[i]["name"];
//                    bd.m_strModelName = (string)jd[i]["model"];
//                    bd.m_listTargetInOrder = StrToIntList((string)jd[i]["target"]);
//                    bd.m_iUnitType = StrToInt((string)jd[i]["type"]);
//                    bd.m_iBaseLevel = StrToInt((string)jd[i]["baselevel"]);
//                    bd.m_sExplain = (string)jd[i]["explain"];
//                    bd.m_sSkillExplain = (string)jd[i]["skill"];
//                    bd.m_iNumber = StrToInt((string)jd[i]["number"]);
//                    bd.m_iATKNumber = StrToInt((string)jd[i]["atk"]);
//                    bd.m_iATKRange = StrToInt((string)jd[i]["atkrange"]);
//                    bd.m_iATKRate = StrToInt((string)jd[i]["atkrate"]);
//                    bd.m_iMaxBlood = StrToInt((string)jd[i]["blood"]);
//                    bd.m_iPrice = StrToInt((string)jd[i]["price"]);
//                    bd.m_iProTime = StrToInt((string)jd[i]["protime"]);
//                    bd.m_fRad = StrToFloat((string)jd[i]["rad"]);
//                    if (!m_dicBuildingData.ContainsKey(bd.m_iID))
//                        m_dicBuildingData.Add(bd.m_iID, bd);
//                }
//            }

//            return true;
//        }

//        int StrToInt(string str)
//        {
//            int tempInt = 0;
//            if(str == "/")
//            {
//                tempInt = -1;
//            }else
//            {
//                tempInt = Convert.ToInt32(str);
//            }
//            return tempInt;
//        }
//        float StrToFloat(string str)
//        {
//            float tempfloat = 0;
//            if (str == "/")
//            {
//                tempfloat = -1;
//            }
//            else
//            {
//                tempfloat = (float)Convert.ToDouble(str);
//            }
//            return tempfloat;
//        }
//        List<int> StrToIntList(string str)
//        {
//            string enter = "";
//            List<int> iList = new List<int>();   
//            if(str == "/")
//            {
//                iList.Add(-1);
//                return iList;
//            }
            
//            for(int i=0;i<str.Length;i++)
//            {
//                string temp = str.Substring(i, 1);
//                if(temp != "/")
//                {
//                    enter += temp;
//                }else
//                {
//                    //Debug.Log(enter);
//                    iList.Add(Convert.ToInt32(enter));
//                    enter = "";                    
//                }
//                if(i== str.Length-1)
//                {
//                    //Debug.Log(enter);
//                    iList.Add(Convert.ToInt32(enter));
//                    enter = "";
//                }
      
//            }

//            return iList;
//        }
    
//    }
//}
