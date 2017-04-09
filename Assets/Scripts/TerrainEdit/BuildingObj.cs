using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

[AddComponentMenu("Scene/SceneObj")]
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class BuildingObj : MonoBehaviour
{
    public int m_ObjDBID;    //类型ID; 
    public uint m_InstanceID;
}
