using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFieldWIndow : WindowBase
{
    public void OnClickAddNewClass()
    {
        //GameObject.Instantiate(classPrefab.gameObject, uiFieldObjectRoot.transform);
        FieldObjectManager.Get().DebugCreateClass();
    }
}
