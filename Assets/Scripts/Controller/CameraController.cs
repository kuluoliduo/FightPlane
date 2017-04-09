//using UnityEngine;
//using System.Collections;
//using ClashGame;
//using UnityEngine.EventSystems;
//using System.Collections.Generic;

//public class CameraController : MonoBehaviour 
//{
//    public float m_Acceleration
//    {
//        set { m_acceleration = value; }
//        private get { return m_acceleration; }
//    }
//    private float m_acceleration = 7f;

//    //摄像机到达的最大左上角
//    private Vector3 CameraTop
//    {
//        set;
//        get;
//    }

//    //摄像机到达的最大右下角
//    private Vector3 CameraBottom
//    {
//        set;
//        get;
//    }


//    private Vector3 CameraLeft
//    {
//        set;
//        get;
//    }

//    private Vector3 CameraRight
//    {
//        set;
//        get;
//    }

//    //阻力的设置 
//    public float Resistance
//    {
//        set;
//        private get;
//    }
//    private float mResistance = 0.01f;

//    void Start()
//    {
//        //GetSize();
//    }

//    void LateUpdate()
//    {
//        if (Ground == null)
//            return;
//        if (IsPointerOverUI())
//        {
//            onDragEnd(Input.mousePosition);
//        }
//        if (Input.GetMouseButtonDown(0)&&!IsPointerOverUI())
//        {
//            mfTimeDown = Time.realtimeSinceStartup;
           
//            onDragBegin(Input.mousePosition);
//        }
//        if (Input.GetMouseButtonUp(0))
//        {
//            onDragEnd(Input.mousePosition);

//            //if (mbRangeTag == true)
//            //{
//            //    OnMouseUp();
//            //}
//            if (Vector2.Distance(Input.mousePosition, StartPosition) < 4f)
//            {
//                //OnMouseClick();
//            }
//        }
//        onDrag(Input.mousePosition);
//        unDrag();

//        if(isDrag)
//        {
//            //if(mfTimeDownToUp < Time.realtimeSinceStartup - mfTimeDown)
//            //{
//            //    OnMouseDown();
//            //    mbRangeTag = true;
//            //}
//        }

//    }
//    /// <summary>
//    /// 拖动或者长按出现
//    /// </summary>
//    private void OnMouseDown()
//    {
//        if (mbRangeTag)
//            return;
//        Dictionary<uint, RtsBuilding> sbDic = GameApp.m_WorldManager.GetBuildingDic(eRtsGroupType.RGT_Self);
//        foreach (KeyValuePair<uint, RtsBuilding> kv in sbDic)
//        {
//            Transform rangeTag = kv.Value.m_ObjInstance.transform.FindChild("Range");//range 大小是否配表
//            if (rangeTag != null)
//            {
//                rangeTag.localScale = new Vector3(2, 1, 2);
//                rangeTag.gameObject.SetActive(true);
//            }
//        }
//    }
//    /// <summary>
//    /// 范围
//    /// </summary>
//    private void OnMouseUp()
//    {
//        Dictionary<uint, RtsBuilding> sbDic = GameApp.m_WorldManager.GetBuildingDic(eRtsGroupType.RGT_Self);
//        foreach (KeyValuePair<uint, RtsBuilding> kv in sbDic)
//        {
//            Transform rangeTag = kv.Value.m_ObjInstance.transform.FindChild("Range");//range 大小是否配表
//            if (rangeTag != null)
//            {
//                rangeTag.gameObject.SetActive(false);
//            }
//        }
//        mbRangeTag = false;
//    }
//    /// <summary>
//    /// 判断是不是点击
//    /// </summary>
//    //private void OnMouseClick()
//    //{
//    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//    //    RaycastHit hit;
//    //    if (Physics.Raycast(ray, out hit))
//    //    {
//    //        //Debug.Log(hit.transform.gameObject.name);
//    //        BuildingMark bm = hit.transform.GetComponent<BuildingMark>();
//    //        if (bm != null)
//    //        {
//    //            Debug.Log(bm.BuildingInsID);
//    //            HUD wnd = GameApp.m_UIManager.GetWnd("HUD") as HUD;
//    //            if (wnd != null)
//    //            {
//    //                wnd.m_iCurSelectBuliding = bm.BuildingInsID;
//    //            }
//    //        }     
//    //        //Debug.Log(hit.point);
//    //        //Debug.LogWarning(Input.mousePosition);
//    //        //Debug.DrawLine(ray.origin, hit.point, Color.red);       
//    //        DT_POS pos = new DT_POS();
//    //        pos.PosX = hit.point.x;
//    //        pos.PosY = hit.point.z;
//    //        if (GameApp.m_WorldManager.m_SceneGridManager.GridCanMove((int)pos.PosX, (int)pos.PosY))
//    //        {
//    //            HUD hud = GameApp.m_UIManager.GetWnd("HUD") as HUD;
//    //            if (hud != null && hud.IsOpen() && hud.SelectID != 0&&hud.CheckGold())
//    //            {
//    //                if ((ushort)hud.GetNumber()>0)
//    //                {
//    //                    GameApp.m_NetHandler.DispatchTroopsReq((uint)hud.SelectID, pos, (ushort)hud.GetNumber());
//    //                }
//    //                else
//    //                {
//    //                    OperationTipsUI.Instance().Pop(Localization.mInstance.GetText("HbUILocal", 4));
//    //                }
//    //                GameRes res = new GameRes("NetRes/Prefabs/RtsObj/Building/P-RallyPoint");
//    //                res = ResourcesManager.LoadObj(res);
//    //                GameObject obj = GameObject.Instantiate(res.m_Object) as GameObject;
//    //                obj.transform.position = hit.point;
//    //                obj.transform.localScale = new Vector3(0.5f, 2, 0.5f);
//    //                obj.name = "P-RallyPoint";
//    //                //Debug.LogWarning(obj.name);
//    //            }
//    //        }
//    //        else
//    //        {
//    //            Debug.LogWarning("can not put a troop in this grid!!!");
//    //        }
//    //    }

//    //}

//    /// <summary>
//    /// 解析是否点击到UI
//    /// </summary>
//    /// <returns></returns>
//    public bool IsPointerOverUI()
//    {
//        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
//        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
//        List<RaycastResult> results = new List<RaycastResult>();
//        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
//        return results.Count > 0;
//    }


//    /// <summary>
//    /// 拖拽开始
//    /// </summary>
//    /// <param name="startpos"></param>
//    private void onDragBegin(Vector2 startpos)
//    {
//        Ray ray = Camera.main.ScreenPointToRay(new Vector3(startpos.x, startpos.y, 1f));
//        RaycastHit hit = new RaycastHit();
//        if (Physics.Raycast(ray, out hit))
//        {
//            isDrag = true;
//            mLastPostion = startpos;
//            StartPosition = startpos;
//        }
//    }

//    /// <summary>
//    /// 拖拽结束
//    /// </summary>
//    /// <param name="endpos"></param>
//    private void onDragEnd(Vector2 endpos)
//    {
//        isDrag = false;
//    }


//    /// <summary>
//    /// 在拖拽状态
//    /// </summary>
//    /// <param name="startpos"></param>
//    private void onDrag(Vector2 startpos)
//    {
//        if (!isDrag)
//            return;
//      //  startpos.x =  mLastPostion.x;
//        Vector3 GroudLastPosition = GetPostion(mLastPostion);
//        Vector3 GroundNowPosition = GetPostion(startpos);
//        Vector3 Mistake = GroudLastPosition - GroundNowPosition;
//        Mistake.y = 0;
//       // Mistake.x = Mistake.z;
//        transform.position += Mistake;
//        float delatime = Time.unscaledDeltaTime;
//        newVelocity = Mistake / delatime;
//        mVelocity = Vector3.Lerp(mVelocity, newVelocity, delatime * 10 * m_Acceleration);
//        mLastPostion = startpos;
//        ReSetPosition();
//    }

//    /// <summary>
//    /// 获取相对地面的位置
//    /// </summary>
//    /// <param name="position"></param>
//    /// <returns></returns>
//    private Vector3 GetPostion(Vector2 position)
//    {
//        Ray ray = Camera.main.ScreenPointToRay(new Vector3(position.x, position.y, 1f));
//        RaycastHit hit = new RaycastHit();
//        if (Physics.Raycast(ray, out hit))
//        {
//            return hit.point;
//        }
//        return Vector3.zero;
//    }


//    /// <summary>
//    /// 未拖拽的运动
//    /// </summary>
//    private void unDrag()
//    {
//        if (!isDrag && mVelocity != Vector3.zero)
//        {
//            Vector3 position = transform.position;
//            for (int i = 0; i < 3; i++)
//            {
//                mVelocity[i] *= Mathf.Pow(0.01f, Time.unscaledDeltaTime);
//                if (Mathf.Abs(mVelocity[i]) < 1)
//                    mVelocity[i] = 0;
//                position[i] += mVelocity[i] * Time.unscaledDeltaTime;

//            }
//            transform.position = position;
//            position = ReSetPosition();
//        }
//    }

//    /// <summary>
//    /// 获取底板和摄像机的最左右上和左下的拖拽位置
//    /// </summary>
//    public void GetSize()
//    {
//        Ground = GameObject.Find("Eve/ground").transform;
//        if (Ground != null)
//        {
//            //地表的数据
//            Vector3 GroundPosition = Ground.position;
//            Vector3 Top = new Vector3(GroundPosition.x + Ground.localScale.x / 2, GroundPosition.y, GroundPosition.z + Ground.localScale.z / 2);
//            Vector3 Bottom = new Vector3(GroundPosition.x - Ground.localScale.x / 2, GroundPosition.y, GroundPosition.z - Ground.localScale.z / 2);
//            Vector3 Left = new Vector3(Bottom.x, GroundPosition.y, Top.z);
//            Vector3 Right = new Vector3(Top.x, GroundPosition.y, Bottom.z);

//            if (GameApp.MainRole.m_eCamp == eCampType.CT_Blue)
//            {
//                transform.position = transform.position + Top - GetPostion(new Vector2(Screen.width / 2, Screen.height / 2)) - new Vector3(0, 0, 15);
//                CameraTop = transform.localPosition;
//                transform.position = transform.position + Bottom - GetPostion(new Vector2(Screen.width / 2, Screen.height / 2)) + new Vector3(0, 0, 15);
//                CameraBottom = transform.localPosition;
//                transform.position = transform.position + Left - GetPostion(new Vector2(Screen.width / 2, Screen.height / 2)) + new Vector3(20, 0, 0);
//                CameraLeft = transform.localPosition;
//                transform.position = transform.position + Right - GetPostion(new Vector2(Screen.width / 2, Screen.height / 2)) - new Vector3(20, 0, 0);
//                CameraRight = transform.localPosition;
//            }
//            if (GameApp.MainRole.m_eCamp == eCampType.CT_Red)
//            {
//                transform.position = transform.position + Bottom - GetPostion(new Vector2(Screen.width / 2, Screen.height / 2)) + new Vector3(0, 0, 58);
//                CameraTop = transform.localPosition;
//                transform.position = transform.position + Top - GetPostion(new Vector2(Screen.width / 2, Screen.height / 2)) - new Vector3(0, 0,38);
//                CameraBottom = transform.localPosition;
//                transform.position = transform.position + Right - GetPostion(new Vector2(Screen.width / 2, Screen.height / 2)) - new Vector3(20, 0, 0);
//                CameraLeft = transform.localPosition;
//                transform.position = transform.position + Left - GetPostion(new Vector2(Screen.width / 2, Screen.height / 2)) + new Vector3(20, 0, 0);
//                CameraRight = transform.localPosition;
//            }
//            //重置摄像机的位置
//            ReSetCamerPosition();
//        }
//    }

//    /// <summary>
//    /// 如果拖拽的位置超出则重置
//    /// </summary>
//    private Vector2 ReSetPosition()
//    {
//        if (transform.localPosition.z >= CameraTop.z || transform.localPosition.z <= CameraBottom.z || transform.localPosition.x<=CameraLeft.x||transform.localPosition.x>=CameraRight.x)
//        {

//            if (transform.localPosition.z >= CameraTop.z)
//            {
//                transform.localPosition  = new Vector3(transform.localPosition.x, transform.localPosition.y, CameraTop.z);
//            }
//            if (transform.localPosition.z <= CameraBottom.z)
//            {       
//                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, CameraBottom.z);
//            }

//            if (transform.localPosition.x >= CameraRight.x)
//            {
//                transform.localPosition = new Vector3(CameraRight.x, transform.localPosition.y, transform.localPosition.z);
//            }

//            if (transform.localPosition.x <= CameraLeft.x)
//            {
//                transform.localPosition = new Vector3(CameraLeft.x, transform.localPosition.y, transform.localPosition.z);
//            }

//        }
//        Vector3 vec = transform.position;
//        return vec;
//    }


//    /// <summary>
//    /// 重置摄像机位置
//    /// </summary>
//    private void ReSetCamerPosition()
//    {
//        RtsObjBase mRtsObjBase = GameApp.m_WorldManager.m_Base;
//        if (mRtsObjBase!=null)
//        {
//            Vector3 nowPosition = GetPostion(new Vector2(Screen.width / 2, Screen.height / 2));
//            Vector3 distance = mRtsObjBase.Position - nowPosition;
//            distance = new Vector3(distance.x, 0, distance.z);
//            transform.position += distance;
//        }
//        ReSetPosition();
//    }


//    //是否在拖拽状态 
//    private bool isDrag = false;
//    //取全中后的速度
//    private Vector3 mVelocity = Vector2.zero;
//    //下一个速度
//    private Vector3 newVelocity = Vector2.zero;
//    //上一个点击位置
//    private Vector2 mLastPostion = Vector2.zero;
//    //底板
//    private Transform Ground = null;
//    //按下抬起间隔
//    private float mfTimeDownToUp = 0.2f;
//    //按下时间
//    private float mfTimeDown;
//    //Range显示标志位
//    private bool mbRangeTag = false;
//    //鼠标开始点击的位置
//    private Vector2 StartPosition = Vector2.zero;
//}
