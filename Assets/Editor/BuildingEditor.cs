using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class BuildingEditor : MonoBehaviour 
{
    [MenuItem("SceneEditor/SaveBuilding")]
    static private void SaveBuilding()
    {
        GameObject SceneEditor = GameObject.Find("SceneEditor");
        SceneEdit edit = SceneEditor.GetComponent<SceneEdit>();
        if (edit != null)
        {
            edit.SaveBuilding();
            SceneEditor.transform.localPosition = new Vector3(SceneEditor.transform.localPosition.x + 1, SceneEditor.transform.localPosition.y, SceneEditor.transform.localPosition.z);
        }
    }
    [MenuItem("SceneEditor/LoadBuilding")]
    static private void LoadBuilding()
    {
        GameObject SceneEditor = GameObject.Find("SceneEditor");
        SceneEdit edit = SceneEditor.GetComponent<SceneEdit>();
        if (edit != null)
        {
            edit.LoadBuilding();
            SceneEditor.transform.localPosition = new Vector3(SceneEditor.transform.localPosition.x + 1, SceneEditor.transform.localPosition.y, SceneEditor.transform.localPosition.z);
        }
    }
}
