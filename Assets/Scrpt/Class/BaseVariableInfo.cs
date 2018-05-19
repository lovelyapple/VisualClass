using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVariableInfo : FieldObjectDataBase
{
    public override FIELD_OBJECT_TYPE FieldObjectType { get { return FIELD_OBJECT_TYPE.BaseVariable; } }
    public void SetupParentClass(BaseClassInfo parent)
    {
        this.ParentObjectData = parent;
    }
}
