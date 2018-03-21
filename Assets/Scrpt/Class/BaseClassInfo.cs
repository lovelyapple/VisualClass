using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClassInfo : FieldObjectDataBase
{
    int maxVariableAmount = 10;
    int maxFunctionAmoount = 10;
    public override FIELD_OBJECT_TYPE FieldObjectType { get { return FIELD_OBJECT_TYPE.BaseClass; } }
    public Dictionary<FIELD_OBJECT_TYPE, Dictionary<ulong, FieldObjectDataBase>> classFieldObjectInfoDict = new Dictionary<FIELD_OBJECT_TYPE, Dictionary<ulong, FieldObjectDataBase>>();
    public void SetUp(ulong serial, string name)
    {
        this.SerialId = serial;
        this.ObjectName = name;
    }

    //これって、毎回idxを検索しているので、csvから生成されるときに、面倒いな
    public BaseVariableInfo AddNewBaseVariable(string name)
    {
        if (!classFieldObjectInfoDict.ContainsKey(FIELD_OBJECT_TYPE.BaseVariable))
        {
            classFieldObjectInfoDict.Add(FIELD_OBJECT_TYPE.BaseVariable, new Dictionary<ulong, FieldObjectDataBase>());
        }

        for (uint i = 0; i < maxVariableAmount; ++i)
        {
            if (classFieldObjectInfoDict[FIELD_OBJECT_TYPE.BaseVariable].ContainsKey(i))
            {
                Debug.LogError("すでに含まれているidx " + i);
                continue;
            }

            var newInfo = new BaseVariableInfo();

            var serial = FieldObjectDataBase.ConvertToSerial(this, FIELD_OBJECT_TYPE.BaseVariable, i);
            newInfo.Setup(this, serial, name);
            classFieldObjectInfoDict[FIELD_OBJECT_TYPE.BaseVariable].Add(i, newInfo);

            return newInfo;
        }

        return null;
    }
    public bool RemoveObjectInfo(FieldObjectDataBase info)
    {
        var type = info.FieldObjectType;
        var fieldSerial = info.FieldSerialId.Value;

        if (!classFieldObjectInfoDict.ContainsKey(type))
        {
            Debug.LogError("RemoveObjectInfo そのtypeが登録されていない" + type);
            return false;
        }

        if (classFieldObjectInfoDict[type].ContainsKey(fieldSerial))
        {
            classFieldObjectInfoDict[type].Remove(fieldSerial);
            return true;
        }

        Debug.LogError("RemoveObjectInfo そのfieldSerialが登録されていない");
        return false;
    }
    public void ClearClassInfo()
    {
        //todo 中身にそれぞれの変数の登録先を解除する必要がある
    }
}
