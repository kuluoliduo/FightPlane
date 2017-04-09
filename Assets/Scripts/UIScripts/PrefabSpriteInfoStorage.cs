using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;


[Serializable]  //必须添加序列化特性
public class PrefabSpriteInfo
{
    public int uiElementType = 0;//0sprite,1text...
    public string ImageTransPath;
    public string SpriteName;
}
public class PrefabSpriteInfoStorage : MonoBehaviour
{
    public string PrefabName;
    public List<PrefabSpriteInfo> PSIList = new List<PrefabSpriteInfo>();

}
