using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVariableInfo : FieldObjectDataBase
{
    public override FIELD_OBJECT_TYPE FieldObjectType { get { return FIELD_OBJECT_TYPE.BaseVariable; } }
    public void Setup(BaseClassInfo classInfo, ulong serial, string name)
    {
        this.ParentObjectData = classInfo;
        this.ObjectName = name;
        this.SerialId = serial;
    }
}
