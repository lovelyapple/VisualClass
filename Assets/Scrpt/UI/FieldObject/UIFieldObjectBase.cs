using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFieldObjectBase : MonoBehaviour
{
    public ulong showingSerial;
    public virtual FIELD_OBJECT_TYPE FieldObjectType { get { return FIELD_OBJECT_TYPE.None; } }
}
