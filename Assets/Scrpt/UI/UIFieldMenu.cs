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

        CommonManager.OpenInputWindow("新しいクラスの名前を入力してください", (str) =>
        {
            if (!string.IsNullOrEmpty(str))
            {
                FieldObjectManager.Get().CreateClass(str);
            }
        });
    }
    //
    // 下のメニュー
    //
    enum ActionMode
    {
        Noth,
        Class,
        Variable,
        Function,
    }
    ActionMode actionMode;
    [SerializeField] Text slelectedObjName;

    [SerializeField] GameObject classActionButonGroup;
    [SerializeField] GameObject variableActionButtonGroup;
    [SerializeField] GameObject functionActionButtonGroup;

    public UIClass selectedUIClass { get; private set; }
    public UIClassContent selectetedContent { get; private set; }
    public bool IsSelectedCLass { get { return selectedUIClass != null; } }
    public bool IsSelectedContent { get { return selectetedContent != null; } }
    static public UIFieldMenu Get()
    {
        return WindowManager.GetWindow(WindowIndex.FieldMenu) as UIFieldMenu;
    }
    void OnEnable()
    {
        ReleaseSelected();
    }
    public void SelectClass(UIClass uiClass)
    {
        ReleaseSelected();
        slelectedObjName.text = uiClass.classInfo.ObjectName;
        selectedUIClass = uiClass;
        actionMode = ActionMode.Class;
        UIUtility.SetActive(classActionButonGroup.gameObject, true);
        UIUtility.SetActive(variableActionButtonGroup.gameObject, false);
        UIUtility.SetActive(functionActionButtonGroup.gameObject, false);
    }
    public void SelectClassVariable(UIClassContent uiContent)
    {
        ReleaseSelected();
        selectetedContent = uiContent;
        actionMode = ActionMode.Variable;
        UIUtility.SetActive(classActionButonGroup.gameObject, false);
        UIUtility.SetActive(variableActionButtonGroup.gameObject, true);
        UIUtility.SetActive(functionActionButtonGroup.gameObject, false);
    }
    public void ReleaseSelected()
    {
        selectedUIClass = null;
        selectetedContent = null;
        actionMode = ActionMode.Noth;
        slelectedObjName.text = "noth selected";
        UIUtility.SetActive(classActionButonGroup.gameObject, false);
        UIUtility.SetActive(variableActionButtonGroup.gameObject, false);
        UIUtility.SetActive(functionActionButtonGroup.gameObject, false);
    }
    public void OnClickChangeName()
    {
        var titleStr = string.Empty;
        System.Action<string> strUpdate = null;
        switch (actionMode)
        {
            case ActionMode.Class:
                titleStr = "クラスの名前を入力してください。";
                strUpdate = (s) =>
                {
                    selectedUIClass.ChangeName(s);
                    slelectedObjName.text = s;
                };
                break;
            case ActionMode.Variable:
                titleStr = "変数の名前を入力してください。";
                break;
            case ActionMode.Function:
                titleStr = "メソッドの名前を入力して下さい。";
                break;
        }
        CommonManager.OpenInputWindow(titleStr, (str) =>
        {
            if (strUpdate != null)
            {
                strUpdate(str);
            }
        });
    }
    public void OnClickAddNewVariable()
    {
        if (selectedUIClass == null)
        {
            return;
        }

        
    }
    public void OnClickAddNewFunction()
    {

    }
}
