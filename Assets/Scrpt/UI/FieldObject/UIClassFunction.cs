using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIClassFunction : UIFieldObjectBase
{
    [SerializeField] Text functionNameLabel;
    [SerializeField] Text fuctionReturnLabel;
    [SerializeField] Text fuctionArgumentLabel;
    public override FIELD_OBJECT_TYPE FieldObjectType
    {
        get
        {
            if (functionInfo == null)
            {
                return FIELD_OBJECT_TYPE.BaseFunction;
            }

            return functionInfo.FieldObjectType;
        }
    }
    public BaseFunctionInfo functionInfo;
    public void Setup(BaseFunctionInfo info)
    {
        this.functionInfo = info;

        if (info == null)
        {
            UIUtility.SetActive(this.gameObject, false);
            return;
        }

        functionNameLabel.text = info.ObjectName;
        showingSerial = info.SerialId.Value;
        UIUtility.SetActive(this.gameObject, true);
    }
    public void ChangeName(string newName)
    {
        if (functionInfo == null) { return; }

        functionInfo.ObjectName = newName;
        functionNameLabel.text = newName;
    }
    public void OnClickName()
    {
        UIFieldMenu.Get().SelectFuction(this);
    }
    public void OnClickArgument()
    {
    }
    public void OnClickReturn()
    {

    }
}
