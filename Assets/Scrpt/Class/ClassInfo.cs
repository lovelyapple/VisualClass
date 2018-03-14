using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClassInfo : FieldObjectDataBase
{
    public override FIELD_OBJECT_TYPE FieldObjectType { get { return FIELD_OBJECT_TYPE.BaseClass; } }
    public List<ClassContentInfo> contentList;
    public void ClearClassInfo()
    {
        //todo 中身にそれぞれの変数の登録先を解除する必要がある
    }
}
