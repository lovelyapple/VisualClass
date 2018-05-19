using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClassObjectDataContainer<T>
{
    FIELD_OBJECT_TYPE type;
    public uint maxIdx { get; set; }
    public ulong baseSerialId { get; set; }
    Dictionary<uint, T> objectDataDict = new Dictionary<uint, T>();
    public void AddNewObjectData(ref T obj, Action<ulong> onAdded)
    {
        ulong worldSerial = 0;
        for (uint i = 0; i < maxIdx; i++)
        {
            if (objectDataDict.ContainsKey(i))
            {
                continue;
            }

            objectDataDict.Add(i, obj);
            worldSerial = FieldObjectDataBase.ConvertToSerial(this.baseSerialId, this.type, i);
            Debug.Log("新しいObj Id は " + i + " worldserial は " + worldSerial + " だった");
            break;
        }

        if (onAdded != null)
        {
            onAdded(worldSerial);
        }
    }
    public ClassObjectDataContainer(FIELD_OBJECT_TYPE type, ulong classId)
    {
        this.type = type;
        this.baseSerialId = classId;

        switch (type)
        {
            case FIELD_OBJECT_TYPE.BaseClass:
            case FIELD_OBJECT_TYPE.SubClass:
            case FIELD_OBJECT_TYPE.PartialClass:
                maxIdx = 10;
                break;
            case FIELD_OBJECT_TYPE.BaseVariable:
            case FIELD_OBJECT_TYPE.BaseFunction:
                maxIdx = 10;
                break;
            case FIELD_OBJECT_TYPE.BaseArgument:
                maxIdx = 9;
                break;
            default:
                maxIdx = 0;
                break;
        }
    }


}
