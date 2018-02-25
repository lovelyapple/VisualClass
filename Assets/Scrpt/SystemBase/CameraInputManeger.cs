using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraInputManeger : SingleToneBase<CameraInputManeger>
{
    [SerializeField] Camera mainCamera;
    public Vector3 GetSingleTouchPostition()
    {
#if UNITY_STANDALONE
        return  Input.mousePosition;
#else
        var t = Input.GetTouch(0);
        return new Vector3(t.position.x, t.position.y, 0);
#endif
    }
}
