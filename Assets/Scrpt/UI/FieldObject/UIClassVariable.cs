using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIClassVariable : UIFieldObjectBase
{
    [SerializeField] Text variableNameLabel;
    public override FIELD_OBJECT_TYPE FieldObjectType
    {
        get
        {
            if (variableInfo == null)
            {
                return FIELD_OBJECT_TYPE.BaseVariable;
            }

            return variableInfo.FieldObjectType;
        }
    }
    public BaseVariableInfo variableInfo;
    public void Setup(BaseVariableInfo info)
    {
        this.variableInfo = info;
        if (info == null)
        {
            UIUtility.SetActive(this.gameObject, false);
            return;
        }

        variableNameLabel.text = info.ObjectName;
        UIUtility.SetActive(this.gameObject, true);
    }
    public void ChangeName(string newName)
    {
        if (variableInfo == null) { return; }
        variableInfo.ObjectName = newName;
        variableNameLabel.text = newName;
    }
    public void OnClick()
    {
        UIFieldMenu.Get().SelectVariable(this);
    }
}
