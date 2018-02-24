using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIFieldClassObject : MonoBehaviour
{
    [SerializeField] ScrollRect scrollView;
    [SerializeField] GameObject uiGroup;
    [SerializeField] GameObject uiClassFieldContentPrefab;
    [SerializeField] GameObject arrowDown;
    [SerializeField] GameObject arrowUp;
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
        UIUtility.SetActive(scrollView.gameObject, true);
        var i = 3;
        while (i > 0)
        {
            GameObject.Instantiate(uiClassFieldContentPrefab, uiGroup.transform);
            i--;
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
