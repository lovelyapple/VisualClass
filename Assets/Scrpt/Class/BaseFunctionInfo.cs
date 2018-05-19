using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFunctionInfo : FieldObjectDataBase
{
    public override FIELD_OBJECT_TYPE FieldObjectType { get { return FIELD_OBJECT_TYPE.BaseFunction; } }
    public FieldObjectDataBase returnData;
    public void Setup(BaseClassInfo classInfo, ulong serial, string name)
    {
        this.ParentObjectData = classInfo;
        this.ObjectName = name;
        this.SerialId = serial;
    }
}
