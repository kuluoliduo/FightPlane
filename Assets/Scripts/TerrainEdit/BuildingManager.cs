using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using System;
using System.Text;
using ClashGame;

public enum eCampType
{
    CT_Blue = 0,
    CT_Red,
    CT_Neutral
}
public class BuildingData
{
    public eCampType Camp;
    public int DBobjID;
    public float PosX;
    public float PosY;
    public float PosZ;
    public float RotY;
}
public class BuildingManager 
{
    private List<BuildingData> bdList = new List<BuildingData>();

    public void SaveFile(List<BuildingData> list)
    {
        string path = Application.dataPath + "/Resources/NetRes/DB/Scene";
        string filepath = path + "/map001_building.json";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);

        writer.WriteArrayStart();

        for (int i = 0; i < list.Count; i++)
        {
            writer.WriteObjectStart();

            writer.WritePropertyName("Camp");
            writer.Write((int)list[i].Camp);
            writer.WritePropertyName("DBobjID");
            writer.Write(list[i].DBobjID);
            writer.WritePropertyName("PosX");
            writer.Write(list[i].PosX);
            writer.WritePropertyName("PosY");
            writer.Write(list[i].PosY);
            writer.WritePropertyName("PosZ");
            writer.Write(list[i].PosZ);
            writer.WritePropertyName("RotY");
            writer.Write(list[i].RotY);

            writer.WriteObjectEnd();
        }

        writer.WriteArrayEnd();

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        File.WriteAllBytes(filepath, bytes);
        Debug.Log("save json OK");
    }
    public List<BuildingData> LoadFile()
    {
        bdList.Clear();
        //string filepath = Application.dataPath + "/Resources/NetRes/DB/Scene" + "/map001_building.json";
        //if (!File.Exists(filepath))
        //{
        //    return null;
        //}
        //StreamReader sr = new StreamReader(filepath ,System.Text.Encoding.UTF8);
        //string strLine = sr.ReadToEnd();

        string filepath = "NetRes/DB/Scene" + "/map001_building.json";
        TextAsset textAsset = (TextAsset)Resources.Load(filepath, typeof(TextAsset));
        //GameRes res = new GameRes(filepath);
        //res.m_eCatchType = GameRes.eCatchType.CT_None;
        //res = ResourcesManager.LoadObj(res);
        //TextAsset textAsset = (TextAsset)res.m_Object;
        string strLine = textAsset.text;

        Debug.Log(strLine);
        JsonData jd = JsonMapper.ToObject(strLine);
        if (jd.IsArray)
        {
            for (int i = 0; i < jd.Count; i++)
            {
              
                    BuildingData bd = new BuildingData();
                    bd.Camp = (eCampType)(int)jd[i]["Camp"];
                    bd.DBobjID = (int)jd[i]["DBobjID"];
                    bd.PosX = (float)(double)jd[i]["PosX"];
                    bd.PosY = (float)(double)jd[i]["PosY"];
                    bd.PosZ = (float)(double)jd[i]["PosZ"];
                    bd.RotY = (float)(double)jd[i]["RotY"];
                    bdList.Add(bd);
                }
            
        }
        Debug.Log("Load json OK");
        return bdList;
    }
}
