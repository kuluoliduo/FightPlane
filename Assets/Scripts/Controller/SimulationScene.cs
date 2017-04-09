using UnityEngine;
using System.Collections.Generic;
using System.IO;
using LitJson;
using System.Text;
using ClashGame;

public class SimulationScene : MonoBehaviour
{
    static int m_iColumns = GDefine.SceneWidth;
    static int m_iRows = GDefine.SceneHeight;
    static int xDirection = m_iColumns;
    static int yDirection = m_iRows;
    public int m_iUnitLength = 1;
    private int[,] m_CellArray = new int[xDirection, yDirection];
    private int m_iCount = 0;
    private int m_iType = 7;


    Vector3 mouseWorldPosition = Vector3.zero;
    TwoInt ti = new TwoInt();
    Color glColor = Color.white;
    bool mbMouseLeftDrag = false;
    bool mbMouseRightDrag = false;

    static Material lineMaterial;
    void Start()
    {
        LoadArrayFromJson();
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

        GL.MultMatrix(transform.localToWorldMatrix);

        GL.Begin(GL.QUADS);


        for (int i = 0; i < xDirection; i++)
        {
            for (int j = 0; j < yDirection; j++)
            {
                switch (m_CellArray[i, j])
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
        //if (jd.IsArray)
        //{
        //    for (int i = 0; i < jd.Count; i++)
        //    {
        //        if (jd[i]["row"].IsArray)
        //        {
        //            //Debug.Log(jd[i]);
        //            for (int j = 0; j < jd[i]["row"].Count; j++)
        //            {
        //                //m_CellArray[i, j] = (int)jd[i][j][i + "_" + j];
        //                m_CellArray[i, j] = (int)jd[i]["row"][j]["type"];
        //            }
        //        }
        //    }
        //}
        //Debug.Log(jd);
        if (jd.IsArray)
        {
            Debug.Log(jd.Count);
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

}