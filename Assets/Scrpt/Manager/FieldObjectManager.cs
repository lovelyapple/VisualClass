﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObjectManager : SingleToneBase<FieldObjectManager>
{
    [SerializeField] GameObject fieldRoot;
    [SerializeField] UIClass classPrefab;
    public void CreateClass(string title)
    {

    }

    public void DebugCreateClass(string name)
    {
        var classInfo = new ClassInfo();
        classInfo.className = name;

        classInfo.contentList = new List<ClassContentInfo>
            {
                {new ClassContentInfo(ClassContentType.Variable,"content 01")},
                {new ClassContentInfo(ClassContentType.Variable,"content 02")},
                {new ClassContentInfo(ClassContentType.Variable,"content 03")},
                {new ClassContentInfo(ClassContentType.Variable,"content 04")},
                {new ClassContentInfo(ClassContentType.Variable,"content 05")},
            };

        var uiClass = UIUtility.InstantiateGetComponent<UIClass>(classPrefab.gameObject, fieldRoot.transform);
        uiClass.SetUp(classInfo);
    }
}

