using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIClassContent : MonoBehaviour
{
    [SerializeField] Text titleLabel;
    ClassContentInfo info;
    public void Setup(ClassContentInfo info)
    {
        this.info = info;
        if (info != null)
        {
            titleLabel.text = info.contentName;
        }
    }
}