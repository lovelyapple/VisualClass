using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClassInfo : FieldObjectDataBase
{
    int maxVariableAmount = 10;
    int maxFunctionAmoount = 10;
    int maxAccesaryAmount = 9;
    public override FIELD_OBJECT_TYPE FieldObjectType { get { return FIELD_OBJECT_TYPE.BaseClass; } }
    public ClassObjectDataContainer<BaseVariableInfo> variableContainer;// = new ClassObjectDataContainer<BaseVariableInfo>(FIELD_OBJECT_TYPE.BaseVariable,);
    public ClassObjectDataContainer<BaseFunctionInfo> functionContainer;// = new ClassObjectDataContainer<BaseFunctionInfo>();

    public override void Setup(string name, ulong serial)
    {
        base.Setup(name, serial);
        InitializeClassInfo();
    }
    private void InitializeClassInfo()
    {

        variableContainer = new ClassObjectDataContainer<BaseVariableInfo>(FIELD_OBJECT_TYPE.BaseVariable, this.SerialId.Value);
        functionContainer = new ClassObjectDataContainer<BaseFunctionInfo>(FIELD_OBJECT_TYPE.BaseFunction, this.SerialId.Value);
    }

    //これって、毎回idxを検索しているので、csvから生成されるときに、面倒いな
    public void AddNewBaseVariable(string name, Action<BaseVariableInfo> onCreated)
    {
        var newVariable = new BaseVariableInfo();
        newVariable.SetupName(name);
        variableContainer.AddNewObjectData(ref newVariable, (id) =>
        {
            if (id != 0)
            {
                newVariable.SetupSerialId(id);

                if (onCreated != null)
                {
                    onCreated(newVariable);
                }
            }
            else
            {
                Debug.LogError("変数作られなかった name " + name);
            }
        });
    }
    //これって、毎回idxを検索しているので、csvから生成されるときに、面倒いな
    public void AddNewBaseFuction(string name, Action<BaseFunctionInfo> onCreated)
    {
        var newFuction = new BaseFunctionInfo();
        newFuction.SetupName(name);
        functionContainer.AddNewObjectData(ref newFuction, (id) =>
        {
            if (id != 0)
            {
                newFuction.SetupSerialId(id);

                if (onCreated != null)
                {
                    onCreated(newFuction);
                }
            }
            else
            {
                Debug.LogError("変数作られなかった name " + name);
            }
        });
    }
    public void RemoveObjectInfo(FieldObjectDataBase info)
    {
    }
    public void ClearClassInfo()
    {
    }
}
