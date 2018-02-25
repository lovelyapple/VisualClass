using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUtility
{
    static public void SetActive(GameObject obj, bool state)
    {
        if (obj == null) { return; }
        if (obj.activeSelf == state) { return; }

        obj.SetActive(state);
    }
    static public T InstantiateGetComponent<T>(GameObject prefab, Transform parent) where T : class
    {
        var go = GameObject.Instantiate(prefab, parent);

        if (go == null) { return null; }

        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        var compo = go.GetComponent<T>();

        if (compo == null)
        {
            Debug.LogError("no " + typeof(T).ToString() + " in bobject");
            return null;
        }

        return compo;
    }
}
