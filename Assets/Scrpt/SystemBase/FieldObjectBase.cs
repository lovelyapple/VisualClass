using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class OjbectSerializer
{
    static uint objectIdx = 0;
    public static uint GetIdx()
    {
        objectIdx++;
        return objectIdx;
    }
}
public class FieldObjectBase
{
    private uint? objectID;
    public uint ObjectID
    {
        get
        {
            if (!objectID.HasValue)
            {
                objectID = OjbectSerializer.GetIdx();
            }

            return objectID.Value;
        }
    }
}
