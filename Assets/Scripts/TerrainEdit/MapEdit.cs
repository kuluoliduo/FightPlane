using UnityEngine;
using System.Collections.Generic;
using System.IO;
using LitJson;
using System.Text;
using ClashGame;

public class TwoInt
{
    public int x;
    public int y;
}
public class MapEdit : MonoBehaviour
{
    //static int m_iColumns = 72;
    //static int m_iRows = 48;
    static int xDirection = 48;//GDefine.SceneWidth;
    static int yDirection = 48;//GDefine.SceneHeight;
    public int m_iUnitLength = 1;
    private int[,] m_CellArray = new int[xDirection, yDirection];
    private int m_iCount = 0;
    private int m_iType = 7;
    public GUIStyle style;

    Vector3 mouseWorldPosition = Vector3.zero;
    TwoInt ti = new TwoInt();
    Color glColor = Color.white; 
    bool mbMouseLeftDrag = false;
    bool mbMouseRightDrag = false;

    static Material lineMaterial;
    void Start()
    {
        Camera cam = Camera.main;
        cam.orthographicSize = yDirection * 0.5f * m_iUnitLength;
        cam.transform.position = new Vector3(xDirection * 0.5f * m_iUnitLength, yDirection * 0.5f * m_iUnitLength, -10f);

        LoadArrayFromJson();
    }
   
    void Update()
    {
        GetInputKey();
        DragMouse();
    }

    void OnGUI()
    {
        string str = "当前类型：" + m_iCount;
        str += "\nS键保存";
        str += "\n0--9键改变类型";
        str += "\n0：自由-白";
        str += "\n1：主路-红";
        str += "\n2：水-蓝";
        str += "\n3：联动机关-灰";
        str += "\n4：资源点-黄";
        str += "\n5：基地-品红";
        str += "\n6：场景设施-绿";
        GUI.Box(new Rect(0, 0, 100, 80), str, style);
    }
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            var shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    // Will be called after all regular rendering is done
    public void OnRenderObject()
    {       
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);
        
        GL.PushMatrix();

        GL.Begin(GL.QUADS);


        for (int i = 0; i < xDirection; i++)
        {
            for (int j = 0; j < yDirection; j++)
            {    
                switch(m_CellArray[i, j])
                {
                    case 0: glColor = Color.white; break;
                    case 1: glColor = Color.red; break;
                    case 2: glColor = Color.blue; break;
                    case 3: glColor = Color.gray; break;
                    case 4: glColor = Color.yellow; break;
                    case 5: glColor = Color.magenta; break;
                    case 6: glColor = Color.green; break;
                } 
                GL.Color(glColor);
                GL.Vertex3(i * m_iUnitLength, j * m_iUnitLength, 0);//顺时针顺序或者逆时针
                GL.Vertex3(i * m_iUnitLength, (j + 1) * m_iUnitLength, 0);
                GL.Vertex3((i + 1) * m_iUnitLength, (j + 1) * m_iUnitLength, 0);
                GL.Vertex3((i + 1) * m_iUnitLength, j * m_iUnitLength, 0);
            }
        } 
        GL.End();
        GL.PopMatrix();

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
        for (int i = 0; i <= xDirection; i++)
        {
            GL.Color(Color.green);
            GL.Vertex3(i, 0, 0);
            GL.Vertex3(i, yDirection * m_iUnitLength, 0);
        }
        for (int j = 0; j <= yDirection; j++)
        {
            GL.Color(Color.green);
            GL.Vertex3(0, j, 0);
            GL.Vertex3(xDirection * m_iUnitLength, j, 0);
        }
        GL.End();
        GL.PopMatrix();   
    }

    void RefreshMousePos()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ti.x = (int)(mouseWorldPosition.x / m_iUnitLength);
        ti.y = (int)(mouseWorldPosition.y / m_iUnitLength);
    }
    void DragMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mbMouseLeftDrag = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mbMouseLeftDrag = false;
        }
        if (mbMouseLeftDrag)
        {
            RefreshMousePos();
            if (((ti.x >= 0) && (ti.x < xDirection)) && ((ti.y >= 0) && (ti.y < yDirection)))
            {
                m_CellArray[ti.x, ti.y] = m_iCount;
            }
        }
        //if (Input.GetMouseButtonDown(1))
        //{
        //    mbMouseRightDrag = true;
        //}
        //if (Input.GetMouseButtonUp(1))
        //{
        //    mbMouseRightDrag = false;
        //}
        //if (mbMouseRightDrag)
        //{
        //    RefreshMousePos();
        //    if (((ti.x >= 0) && (ti.x < xDirection)) && ((ti.y >= 0) && (ti.y < yDirection)))
        //    {
        //        m_CellArray[ti.y, ti.x] = m_iCount;
        //    }
        //}
    }
    private int m_iOtherMap = 0;
    void GetInputKey()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_iOtherMap = 0;
            SaveAarryAsJson();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            m_iOtherMap++;
            SaveAarryAsJson();
        }
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    if (m_iCount < m_iType - 1)
        //    {
        //        m_iCount++;
        //    }
        //    else
        //    {
        //        m_iCount = 0;
        //    }
        //}
        if (Input.GetKey(KeyCode.Alpha0))
        {
            if (m_iType > 0)
            {
                m_iCount = 0;
            }
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (m_iType > 1)
            {
                m_iCount = 1;
            }
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (m_iType > 2)
            {
                m_iCount = 2;
            }
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (m_iType > 3)
            {
                m_iCount = 3;
            }
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            if (m_iType > 4)
            {
                m_iCount = 4;
            }
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            if (m_iType > 5)
            {
                m_iCount = 5;
            }
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            if (m_iType > 6)
            {
                m_iCount = 6;
            }
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            if (m_iType > 7)
            {
                m_iCount = 7;
            }
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            if (m_iType > 8)
            {
                m_iCount = 8;
            }
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            if (m_iType > 9)
            {
                m_iCount = 9;
            }
        }
    }
    void SaveAarryAsJson()
    {
        string path = Application.dataPath + "/Resources/NetRes/DB/Scene";
        string filepath = path + "/map001.json";
        if (m_iOtherMap == 0)
        {
            filepath = path + "/map001.json";
        }else
        {
            filepath = path + "/map001" + m_iOtherMap + ".json";
        }


        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);

        writer.WriteArrayStart();

        for (int i = 0; i < xDirection; i++)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("row");

            writer.WriteArrayStart();
            for (int j = 0; j < yDirection; j++)
            {
                writer.WriteObjectStart();
                //Debug.Log("i==" + i + "====j====" + j);
                writer.WritePropertyName(i+"_"+j);
                writer.Write(m_CellArray[i, j]);
                writer.WriteObjectEnd();
            }
            writer.WriteArrayEnd();

            writer.WriteObjectEnd();
        }

        writer.WriteArrayEnd();
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        File.WriteAllBytes(filepath, bytes);

        Debug.Log("save " + filepath + " OK");
    }
    void LoadArrayFromJson()
    {
        string filepath = Application.dataPath + "/Resources/NetRes/DB/Scene" + "/map001.json";
        if (!File.Exists(filepath))
        {
            return;
        }
        StreamReader sr = new StreamReader(filepath, System.Text.Encoding.UTF8);
        string strLine = sr.ReadToEnd();
        JsonData jd = JsonMapper.ToObject(strLine);
        //Debug.Log(jd);
        if (jd.IsArray)
        {
            //Debug.Log(jd.Count);
            for (int i = 0; i < jd.Count; i++)
            {
                if (jd[i]["row"].IsArray)
                {
                    //Debug.Log(jd[i]);
                    for (int j = 0; j < jd[i]["row"].Count; j++)
                    {
                        m_CellArray[i, j] = (int)jd[i]["row"][j][i + "_" + j];
                    }
                }
            }
        }

    }
    public int[,] GetArray()
    {
        LoadArrayFromJson();
        return m_CellArray;
    }

}