using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using ClashGame;


[ExecuteInEditMode]
public class SceneEdit : MonoBehaviour
{
    static int m_iColumns = 48;//GDefine.SceneWidth;
    static int m_iRows = 48;//GDefine.SceneHeight;
    static int xDirection = m_iColumns;
    static int yDirection = m_iRows;
    public int m_iUnitLength = 1;
    private int[,] m_CellArray = new int[xDirection, yDirection];
    private int m_iCount = 0;
    private int m_iType = 7;

    public Color m_LineColor = Color.green;
    public bool m_bEditorMode = true;
    public bool m_bLine = false;

    private static bool bMouseClick = false;
    private static UnityEngine.GameObject tempObj = null;

    private static BuildingManager bdm = null;
    private MapEdit mapEdit = null;

    // Use this for initialization
    void Start()
    {
#if UNITY_EDITOR
        init();
#else
        gameObject.SetActive(false);
#endif
    }

#if UNITY_EDITOR
    void OnDestroy()
    {
        //m_CellManager = null;
    }
    void init()
    {
        if (Application.isPlaying)
        {
            gameObject.SetActive(false);
            return;
        }
        mapEdit = gameObject.GetComponent<MapEdit>();
        if (mapEdit == null)
            mapEdit = gameObject.AddComponent<MapEdit>();
        m_CellArray = mapEdit.GetArray();
    }
#endif

    // Update is called once per frame
    void Update()
    {

    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            return;
        }
        init();
        Debuger.EnableLog = true;

        if (!m_bLine)
        {
            for (int i = 0; i < xDirection; i++)
            {
                for (int j = 0; j < yDirection; j++)
                {
                    switch (m_CellArray[i, j])
                    {
                        case 0: Gizmos.color = Color.white; break;
                        case 1: Gizmos.color = Color.red; break;
                        case 2: Gizmos.color = Color.blue; break;
                        case 3: Gizmos.color = Color.gray; break;
                        case 4: Gizmos.color = Color.yellow; break;
                        case 5: Gizmos.color = Color.magenta; break;
                        case 6: Gizmos.color = Color.green; break;
                    }
                    Gizmos.DrawCube(new Vector3((i + 0.5f) * m_iUnitLength, 0, (j + 0.5f) * m_iUnitLength), new Vector3(m_iUnitLength, 0.01f, m_iUnitLength));
                }
            }
        }
        else
        {
            Gizmos.color = m_LineColor;
            for (int i = 0; i <= xDirection; i++)
            {
                Gizmos.DrawLine(new Vector3(i * m_iUnitLength, 0, 0), new Vector3(i * m_iUnitLength, 0, yDirection * m_iUnitLength));
            }

            for (int i = 0; i <= yDirection; i++)
            {
                Gizmos.DrawLine(new Vector3(0, 0, i * m_iUnitLength), new Vector3(xDirection * m_iUnitLength, 0, i * m_iUnitLength));
            }
        }

        OnInput();

    }

    void OnInput()
    {
        if (!m_bEditorMode)
            return;
        if (Event.current.button == 0 && Event.current.isMouse)
        {
            // Debuger.Log(Event.current.ToString());
            if (!bMouseClick && Event.current.type == EventType.MouseUp)
            {
                bMouseClick = true;
                //OnLeftClick();
            }
        }
        else if (Event.current.button == 1)
        {
            if (!bMouseClick)
            {
                bMouseClick = true;
                //OnRightClick();
            }
        }
        else
        {
            bMouseClick = false;
        }

    }



    void RemoveObj(Vector2 cellindex)
    {
        tempObj = null;
    }



    public void SaveBuilding()
    {
        //EditorApplication.SaveScene();
        List<BuildingData> bdList = new List<BuildingData>();
        Transform building = GameObject.Find("Eve/Building").transform;
        if(building == null)
        {
            Debug.LogError("buliding is null");
            return;
        }
        foreach(Transform child in building)
        {
            BuildingData bd = new BuildingData();
            bd.Camp = (eCampType)int.Parse(child.name.Substring(child.name.Length - 1,1));
            bd.PosX = child.position.x;
            bd.PosY = child.position.y;
            bd.PosZ = child.position.z;
            bd.RotY = child.position.y;
            bd.DBobjID = child.GetComponent<BuildingObj>().m_ObjDBID;
            bdList.Add(bd);
        }
        if(bdm == null)
            bdm = new BuildingManager();
        bdm.SaveFile(bdList);
    }
    public void LoadBuilding()
    {
        if (bdm == null)
            bdm = new BuildingManager();
        bdm.LoadFile();
    }
#endif
}

