using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageButtonPlus : Image  
{
    override public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        Debuger.Log("IsRaycastLocationValid=" + sp);
        return true;
    }

}
