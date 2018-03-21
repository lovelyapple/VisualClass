using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClass : MonoBehaviour
{
    [SerializeField] ScrollRect scrollView;
    [SerializeField] GameObject uiGroup;
    [SerializeField] UIClassVariable baseVairablePrefab;
    [SerializeField] GameObject arrowDown;
    [SerializeField] GameObject arrowUp;

    [SerializeField] Text classNameLabel;
    [SerializeField] Image crossArrowImage;
    public BaseClassInfo classInfo { get; private set; }
    public Dictionary<ulong, UIFieldObjectBase> classFieldObjectDict = new Dictionary<ulong, UIFieldObjectBase>();
    enum FieldStats
    {
        Close,
        Open,
    }
    FieldStats fieldState = FieldStats.Close;
    void OnEnable()
    {
        UpdateOpenCLoseArrow();
    }
    public void SetUp(BaseClassInfo info)
    {
        this.classInfo = info;

        if (info == null) { return; }

        classNameLabel.text = info.ObjectName;
    }
    public void ChangeName(string newName)
    {
        if (classInfo == null) { return; }

        classInfo.ObjectName = newName;
        classNameLabel.text = newName;
    }
    void CreateOpenField()
    {
        UIUtility.SetActive(scrollView.gameObject, fieldState == FieldStats.Open);
    }
    void DestoryCloseField()
    {
        var childCount = uiGroup.transform.childCount;

        for (var idx = 0; idx < childCount; idx++)
        {
            var tran = uiGroup.transform.GetChild(idx);
            GameObject.Destroy(tran.gameObject);
        }
        UIUtility.SetActive(scrollView.gameObject, false);
    }
    void UpdateOpenCLoseArrow()
    {
        UIUtility.SetActive(arrowDown, fieldState == FieldStats.Close);
        UIUtility.SetActive(arrowUp, fieldState == FieldStats.Open);
    }
    //
    // 削除
    //
    public void DeleteClassFieldObject(FieldObjectDataBase obj)
    {
        UIFieldObjectBase deleteTarget;

        if (!classFieldObjectDict.TryGetValue(obj.SerialId.Value, out deleteTarget))
        {
            Debug.LogError("削除したいものが見つからない,見つからない");
            return;
        }

        if (!classInfo.RemoveObjectInfo(obj))
        {
            //donoth
        }
    }
    //
    // 追加
    //
    public void CreateBaseVariable(string name)
    {
        if (classInfo == null)
        {
            Debug.LogError("CreateBaseVariable clasInfo がnull");
            return;
        }

        var newVariable = classInfo.AddNewBaseVariable(name);

        if (newVariable == null)
        {
            Debug.LogError("newVariable = null");
            return;
        }

        var co = UIUtility.InstantiateGetComponent<UIClassVariable>(baseVairablePrefab.gameObject, uiGroup.transform);
        co.Setup(newVariable);

        classFieldObjectDict.Add(newVariable.SerialId.Value, co);
    }

    //
    //操作系
    //
    public void OnDragMoveButton()
    {
        var mousePos = CameraInputManeger.Get().GetSingleTouchPostition();
        var centerOffset = crossArrowImage.transform.position - transform.position;
        transform.position = mousePos - centerOffset;
    }
    public void OnClickClassTitle()
    {
        transform.SetAsLastSibling();
        UIFieldMenu.Get().SelectClass(this);

    }
    public void OnClickOpenCLoseField()
    {
        switch (fieldState)
        {
            case FieldStats.Close:
                fieldState = FieldStats.Open;
                break;
            case FieldStats.Open:
                fieldState = FieldStats.Close;
                break;
        }
        CreateOpenField();
        UpdateOpenCLoseArrow();
    }


}
