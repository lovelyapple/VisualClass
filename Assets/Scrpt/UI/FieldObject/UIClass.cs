using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(RectTransform))]
public class UIClass : UIFieldObjectBase
{
    public override FIELD_OBJECT_TYPE FieldObjectType { get { return FIELD_OBJECT_TYPE.BaseClass; } }
    [SerializeField] RectTransform rectTransform;
    [SerializeField] ScrollRect fieldScrollView;
    [SerializeField] GameObject uiGroup;
    [SerializeField] UIClassVariable baseVairablePrefab;
    [SerializeField] UIClassFunction baseFunctionPrefab;
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
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
    public void SetUp(BaseClassInfo info)
    {
        this.classInfo = info;

        if (info == null)
        {
            return;
        }


        classNameLabel.text = info.ObjectName;
        showingSerial = info.SerialId.Value;
    }
    public void ChangeName(string newName)
    {
        if (classInfo == null) { return; }

        classInfo.ObjectName = newName;
        classNameLabel.text = newName;
    }
    void CreateOpenField()
    {
        UIUtility.SetActive(fieldScrollView.gameObject, fieldState == FieldStats.Open);
    }
    void DestoryCloseField()
    {
        var childCount = uiGroup.transform.childCount;

        for (var idx = 0; idx < childCount; idx++)
        {
            var tran = uiGroup.transform.GetChild(idx);
            GameObject.Destroy(tran.gameObject);
        }
        UIUtility.SetActive(fieldScrollView.gameObject, false);
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

        // if (!classInfo.RemoveObjectInfo(obj))
        // {
        //     //donoth
        // }
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

        classInfo.AddNewBaseVariable(name, (arg) =>
        {
            var co = UIUtility.InstantiateGetComponent<UIClassVariable>(baseVairablePrefab.gameObject, uiGroup.transform);
            co.Setup(arg);
            classFieldObjectDict.Add(co.variableInfo.SerialId.Value, co);
        });
    }
    public void CreateBaseFuction(string name)
    {
        if (classInfo == null)
        {
            Debug.LogError("CreateBaseVariable clasInfo がnull");
            return;
        }

        classInfo.AddNewBaseFuction(name, (arg) =>
        {
            var co = UIUtility.InstantiateGetComponent<UIClassFunction>(baseFunctionPrefab.gameObject, uiGroup.transform);
            co.Setup(arg);
            classFieldObjectDict.Add(arg.SerialId.Value, co);
        });
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
    public void OnDragOver()
    {
        FieldObjectManager.Get().ContentSizeAnalyser.UpdateRectSingle(this.rectTransform);
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
