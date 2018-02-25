using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClass : MonoBehaviour
{
    [SerializeField] ScrollRect scrollView;
    [SerializeField] GameObject uiGroup;
    [SerializeField] GameObject uiClassContentPrefab;
    [SerializeField] GameObject arrowDown;
    [SerializeField] GameObject arrowUp;

    [SerializeField] Text classNameLabel;
    [SerializeField] Image crossArrowImage;
    List<UIClassContent> uiContentList;
    public ClassInfo classInfo { get; private set; }
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
    public void OnDragMoveButton()
    {
        var mousePos = CameraInputManeger.Get().GetSingleTouchPostition();
        var centerOffset = crossArrowImage.transform.position - transform.position;
        transform.position = mousePos - centerOffset;
    }

    public void SetUp(ClassInfo info)
    {
        this.classInfo = info;

        if (info == null) { return; }

        classNameLabel.text = info.className;
    }
    // void OnGUI()
    // {
    // }
    // void Update()
    // {


    // }
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
                CreateOpenField();
                fieldState = FieldStats.Open;
                break;
            case FieldStats.Open:
                DestoryCloseField();
                fieldState = FieldStats.Close;
                break;
        }
        UpdateOpenCLoseArrow();
    }
    void CreateOpenField()
    {
        if (uiContentList != null && uiContentList.Count > 0)
        {
            DestoryCloseField();
        }

        uiContentList = new List<UIClassContent>();
        UIUtility.SetActive(scrollView.gameObject, true);

        if (classInfo == null || classInfo.contentList == null) { return; }

        foreach (var content in classInfo.contentList)
        {
            var uiContent = UIUtility.InstantiateGetComponent<UIClassContent>(uiClassContentPrefab, uiGroup.transform);
            uiContent.Setup(content);
            uiContentList.Add(uiContent);
        }
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
}
