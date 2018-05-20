using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(RectTransform))]
public class ContentSizeAnalyser : MonoBehaviour
{
    struct rectInfo
    {
        public Vector2 pos;
        public Vector2 size;
    };
    [SerializeField] RectTransform baseRect;
    [SerializeField] Vector2 baseSize;
    [SerializeField] RectTransform[] childTransform;

    void Awake()
    {
        this.baseRect = GetComponent<RectTransform>();
        this.baseSize = baseRect.sizeDelta;
    }
    void GetChildRectTransform()
    {
        var childrenCnt = this.transform.childCount;
        var childList = new List<RectTransform>();
        for (int i = 0; i < childrenCnt; i++)
        {
            var rect = this.transform.GetChild(i).GetComponent<RectTransform>();
            if (rect != null)
            {
                childList.Add(rect);
            }
        }

        childTransform = childList.ToArray();
        Debug.Log(childTransform.Length);
    }
    public void OnUpdateRectTransforAll()
    {
        GetChildRectTransform();
        var dSize = baseRect.sizeDelta;
        var dPos = baseRect.localPosition;
        rectInfo[] infos = childTransform.Select(x => new rectInfo() { size = x.sizeDelta, pos = x.localPosition }).ToArray();

        Vector2 size = Vector2.zero;
        for (int i = 0; i < infos.Length; i++)
        {
            var info = infos[i];
            var top = Mathf.Abs(info.pos.y + info.size.y / 2);
            var down = Mathf.Abs(info.pos.y - info.size.y / 2);
            var left = Mathf.Abs(info.pos.x - info.size.x / 2);
            var right = Mathf.Abs(info.pos.x + info.size.x / 2);

            if (top > size.y)
            {
                size.y = top;
            }

            if (down > size.y)
            {
                size.y = down;
            }

            if (left > size.x)
            {
                size.x = left;
            }

            if (right > size.x)
            {
                size.x = right;
            }
        }

        if (dSize.x < size.x * 2)
        {
            dSize.x = size.x * 2;
        }

        if (dSize.y < size.y * 2)
        {
            dSize.y = size.y * 2;
        }

        baseRect.sizeDelta = dSize;
    }

    public void UpdateRectSingle(RectTransform rect)
    {
        var info = new rectInfo() { size = rect.sizeDelta, pos = rect.localPosition };
        var top = Mathf.Abs(info.pos.y + info.size.y / 2);
        var down = Mathf.Abs(info.pos.y - info.size.y / 2);
        var left = Mathf.Abs(info.pos.x - info.size.x / 2);
        var right = Mathf.Abs(info.pos.x + info.size.x / 2);
        var dSize = baseRect.sizeDelta;

        if (top * 2 > dSize.y)
        {
            dSize.y = top * 2;
        }

        if (down * 2 > dSize.y)
        {
            dSize.y = down * 2;
        }

        if (left * 2 > dSize.x)
        {
            dSize.x = left * 2;
        }

        if (right * 2 > dSize.x)
        {
            dSize.x = right * 2;
        }

        baseRect.sizeDelta = dSize;
    }
}
