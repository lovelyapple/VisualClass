using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 全てのオブジェクトのDataおやクラス
 serialId構成：
 *987654321*
 987:親クラスID
 65:オブジェクトタイプ
 4321:親クラス内部のシリアルId
 */
public enum FIELD_OBJECT_TYPE
{
    None = 0,
    //インターフェイス区分 1 ~ 10
    //クラス区分 11 ~ 20
    BaseClass = 11,
    SubClass = 12,
    PartialClass = 13,

    // 変数区分 21~30
    BaseVariable = 21,

    // 函数区分31~
    BaseFunction = 31,

}
public class FieldObjectDataBase
{
    [SerializeField] ulong serialIdShowing;
    public ulong? SerialId { get; set; }//Field上に一意性を持つID
    public ulong? FriendSerialId { get; set; }//フレンドObjectID
    public FieldObjectDataBase ParentObjectData;//親情報
    public FieldObjectDataBase ChildObjectData;//子情報
    public virtual FIELD_OBJECT_TYPE FieldObjectType { get { return FIELD_OBJECT_TYPE.None; } }
    public List<ulong> ConnectFromList;//引用元
    public List<ulong> ConnectToList;//引用先
    public string ObjectName;
    public const int classIdArea = 1000000;
    public const int fieldObjTypeIDArea = 10000;

    public uint? ClassId
    {
        get
        {
            if (!SerialId.HasValue) { return null; }
            return (uint)(SerialId.Value / classIdArea);
        }
    }
    public uint? FieldSerialId
    {
        get
        {
            if (!SerialId.HasValue) { return null; }
            return (uint)(SerialId.Value % fieldObjTypeIDArea);
        }
    }
    public void SetupName(string name)
    {
        this.ObjectName = name;
    }
    public void SetupSerialId(ulong id)
    {
        this.SerialId = id;
    }

    public static ulong ConvertToSerial(BaseClassInfo classInfo, FIELD_OBJECT_TYPE type, uint idx)
    {
        if (classInfo == null || !classInfo.SerialId.HasValue)
        {
            return 0;
        }

        return classInfo.SerialId.Value + (uint)type * fieldObjTypeIDArea + idx;
    }
}
