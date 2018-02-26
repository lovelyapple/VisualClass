using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class UICommonInputWIndow : WindowBase
{
    [SerializeField] Text titleLabel;
    [SerializeField] InputField inputField;
    Action<string> action_Ok;
    Action action_Cancel;
    public void Setup(string title, Action<string> aOk, Action aCancel = null)
    {
        this.titleLabel.text = title;
        this.action_Ok = aOk;
        this.action_Cancel = aCancel;
    }
    void OnEnable()
    {
        inputField.text = string.Empty;
    }
    public void OnClickOk()
    {
        if (action_Ok != null)
        {
            action_Ok(inputField.text);
            action_Ok = null;
        }

        Close();
    }
    public void OnCliclCancel()
    {
        if (action_Cancel != null)
        {
            action_Cancel();
            action_Cancel = null;
        }

        Close();
    }

}
