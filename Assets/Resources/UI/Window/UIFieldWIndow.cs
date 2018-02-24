using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFieldWIndow : WindowBase
{
    [SerializeField] GameObject uiFieldObjectRoot;
    [SerializeField] UIFieldClassObject classPrefab;
    public void OnClickAddNewClass()
    {
        GameObject.Instantiate(classPrefab.gameObject, uiFieldObjectRoot.transform);
    }
}
