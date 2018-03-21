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
    [SerializeField] Text selectedObjName;

    [SerializeField] GameObject classActionButonGroup;
    [SerializeField] GameObject variableActionButtonGroup;
    [SerializeField] GameObject functionActionButtonGroup;

    public UIClass selectedUIClass { get; private set; }
    public bool IsSelectedClass { get { return selectedUIClass != null; } }
    public UIClassVariable selectedUIClassVariable { get; private set; }
    public bool IsSelectedClassVariable { get { return selectedUIClassVariable != null; } }
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
        selectedObjName.text = uiClass.classInfo.ObjectName;
        selectedUIClass = uiClass;
        actionMode = ActionMode.Class;
        UIUtility.SetActive(classActionButonGroup.gameObject, true);
        UIUtility.SetActive(variableActionButtonGroup.gameObject, false);
        UIUtility.SetActive(functionActionButtonGroup.gameObject, false);
    }
    public void SelectVariable(UIClassVariable uiVariable)
    {
        ReleaseSelected();
        selectedObjName.text = uiVariable.variableInfo.ObjectName;
        selectedUIClassVariable = uiVariable;
        actionMode = ActionMode.Variable;
        UIUtility.SetActive(classActionButonGroup.gameObject, false);
        UIUtility.SetActive(variableActionButtonGroup.gameObject, true);
        UIUtility.SetActive(functionActionButtonGroup.gameObject, false);
    }
    public void ReleaseSelected()
    {
        selectedUIClass = null;
        actionMode = ActionMode.Noth;
        selectedObjName.text = "noth selected";
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
                    selectedObjName.text = s;
                };
                break;
            case ActionMode.Variable:
                titleStr = "変数の名前を入力してください。";
                strUpdate = (s) =>
                {
                    selectedUIClassVariable.ChangeName(s);
                    selectedObjName.text = s;
                };
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

        CommonManager.OpenInputWindow("新しい変数の名前を入力してください", (str) =>
        {
            selectedUIClass.CreateBaseVariable(str);
        });
    }
    public void OnClickAddNewFunction()
    {

    }
}
