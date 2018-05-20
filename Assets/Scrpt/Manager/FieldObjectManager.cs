using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FieldObjectManager : SingleToneBase<FieldObjectManager>
{
    [SerializeField] GameObject fieldRoot;
    [SerializeField] UIClass classPrefab;
    [SerializeField] ContentSizeAnalyser contentSizeAnalyser;
    public ContentSizeAnalyser ContentSizeAnalyser
    {
        get
        {
            if (contentSizeAnalyser == null)
            {
                contentSizeAnalyser = this.gameObject.GetComponent<ContentSizeAnalyser>();

                if (contentSizeAnalyser == null)
                {
                    Debug.LogError("contentSizeAnalyser is null");
                }
            }

            return contentSizeAnalyser;
        }
    }
    int maxClass = 100;

    Dictionary<uint, BaseClassInfo> classInfoDict;
    Dictionary<uint, UIClass> fieldClassObjectDict;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        InitClassDict();
    }
    public void InitClassDict()
    {
        classInfoDict = new Dictionary<uint, BaseClassInfo>();
        for (int i = 0; i < maxClass; ++i)
        {
            classInfoDict.Add((uint)i, null);
        }

        if (fieldClassObjectDict != null && fieldClassObjectDict.Count > 0)
        {
            foreach (var id in fieldClassObjectDict.Keys)
            {
                var classObj = fieldClassObjectDict[id];

                if (classObj != null)
                {
                    Destroy(classObj);
                }
            }
        }

        fieldClassObjectDict = new Dictionary<uint, UIClass>();
    }
    /// 新しいクラス作成
    /// まずクラスレジスターの中に空き状況と空いてるIdを探す
    /// あれば、そのidのUIが作成済みかどうかをチェックし
    /// 済みなら、再登録、なければ新しくつくる
    public bool CreateClass(string title)
    {
        for (uint i = 0; i < maxClass; ++i)
        {
            BaseClassInfo info;
            classInfoDict.TryGetValue(i, out info);

            if (info == null)
            {
                info = new BaseClassInfo();
                info.Setup(title, FieldObjectDataBase.CreateBaseClassSerial(i));
                classInfoDict[i] = info;

                UIClass uiClass;

                if (!fieldClassObjectDict.TryGetValue(i, out uiClass))
                {
                    uiClass = UIUtility.InstantiateGetComponent<UIClass>(classPrefab.gameObject, fieldRoot.transform);
                    uiClass.SetUp(info);

                    fieldClassObjectDict.Add(i, uiClass);
                }
                else
                {
                    uiClass.SetUp(info);
                }

                UIUtility.SetActive(uiClass.gameObject, true);
                return true;
            }
        }
        Debug.LogError("作成できるクラスの最大数制限を超えた");
        return false;
    }
    public void DeleteClass(uint classId)
    {
        if (!fieldClassObjectDict.Keys.Contains(classId))
        {
            Debug.LogError("削除しようとしているクラス fieldClassObjectDict 存在しない");
        }
        else
        {
            fieldClassObjectDict[classId].SetUp(null);
            UIUtility.SetActive(fieldClassObjectDict[classId].gameObject, false);
        }

        if (!classInfoDict.Keys.Contains(classId))
        {
            Debug.LogError("削除しようとしているクラス classInfoDict 存在しない");
        }
        else
        {
            classInfoDict[classId].ClearClassInfo();
            classInfoDict[classId] = null;
        }
    }
}

