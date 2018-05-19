using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 全てのオブジェクトのDataおやクラス
 serialId構成：
 * 987 65 4321*
 987:親クラスID
 65:オブジェクトタイプ
 4321:親クラス内部のシリアル親Id
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

    // その他 50~
    BaseArgument = 50,  //引き数
}
public class FieldObjectDataBase
{
    public ulong? SerialId { get; set; }//Field上に一意性を持つID
    public virtual FIELD_OBJECT_TYPE FieldObjectType { get { return FIELD_OBJECT_TYPE.None; } }
    public ulong? FriendSerialId { get; set; }//フレンドObjectID
    public FieldObjectDataBase ParentObjectData;//親情報
    public FieldObjectDataBase ChildObjectData;//子情報
    public List<ulong> ConnectFromList = new List<ulong>();//引用元
    public List<ulong> ConnectToList;//引用先
    public string ObjectName;
    public const int ClassIdArea = 1000000;
    public const int FieldObjTypeIDArea = 10000;

    public uint? ClassId
    {
        get
        {
            if (!SerialId.HasValue)
            {
                Debug.LogError("Serifal ないぞ");
                return null;
            }
            return (uint)(SerialId.Value / ClassIdArea);
        }
    }

    public uint? FieldObjectTypeId
    {
        get
        {
            if (!SerialId.HasValue)
            {
                Debug.LogError("Serifal ないぞ");
                return null;
            }
            return (uint)(SerialId.Value % ClassIdArea / FieldObjTypeIDArea);
        }
    }

    public uint? FieldSerialId
    {
        get
        {
            if (!SerialId.HasValue)
            {
                Debug.LogError("Serifal ないぞ");
                return null;
            }
            return (uint)(SerialId.Value % FieldObjTypeIDArea);
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
    public virtual void Setup(string name, ulong serial)
    {
        this.ObjectName = name;
        this.SerialId = serial;
    }

    //
    // world serial に変換
    //
    public static ulong ConvertToSerial(BaseClassInfo classInfo, FIELD_OBJECT_TYPE type, uint idx)
    {
        if (classInfo == null || !classInfo.SerialId.HasValue)
        {
            return 0;
        }

        return classInfo.SerialId.Value + FieldTypeToWorldSerial(type) + idx;
    }
    public static ulong ConvertToSerial(ulong classSerial, FIELD_OBJECT_TYPE type, uint idx)
    {
        return classSerial + FieldTypeToWorldSerial(type) + idx;
    }
    public static ulong CreateBaseClassSerial(uint idx)
    {
        return idx * ClassIdArea + (uint)FIELD_OBJECT_TYPE.BaseClass * FieldObjTypeIDArea;
    }

    //
    // worldSerialの変換
    //
    public static ulong ClassIdToWorldSerial(uint classId)
    {
        return classId * ClassIdArea;
    }
    public static ulong FieldTypeToWorldSerial(uint fieldTypeId)
    {
        return fieldTypeId * FieldObjTypeIDArea;
    }
    public static ulong FieldTypeToWorldSerial(FIELD_OBJECT_TYPE type)
    {
        return FieldTypeToWorldSerial((uint)type);
    }
    public static uint ConverToLocalIndex(ulong serial)
    {
        return (uint)serial % ClassIdArea;
    }
}
