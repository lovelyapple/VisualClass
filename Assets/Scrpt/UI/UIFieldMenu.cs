using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIFieldMenu : WindowBase
{

    //
    //上のメニュー
    //
    public void OnClickAddNewClass()
    {
        FieldObjectManager.Get().DebugCreateClass();
    }
    //
    // 下のメニュー
    //
    [SerializeField] Text activeClassNameLabel;
    [SerializeField] Button btnAddNewVariable;
    [SerializeField] Button btnAddNewFunc;
    public UIClass selectedUIClass { get; private set; }
    public bool IsSelectedOneCLass { get { return selectedUIClass != null; } }
    public void SelectClass(UIClass uiClass)
    {
        selectedUIClass = uiClass;
        UpdateActiveClassUI();
    }
    public void UnSelectedClass()
    {
        selectedUIClass = null;
        UpdateActiveClassUI();
    }
    public void UpdateActiveClassUI()
    {
        activeClassNameLabel.text = IsSelectedOneCLass ? selectedUIClass.classInfo.className : "noth Selected";
        btnAddNewVariable.enabled = IsSelectedOneCLass;
        btnAddNewFunc.enabled = IsSelectedOneCLass;
    }
    static public UIFieldMenu Get()
    {
        return WindowManager.GetWindow(WindowIndex.FieldMenu) as UIFieldMenu;
    }
}
