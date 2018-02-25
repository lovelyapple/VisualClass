using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClassContentType
{
	Variable,
	Function,
}
public class ClassContentInfo : FieldObjectBase
{
	ClassContentType contentType;
	public string contentName;
	public ClassContentInfo(ClassContentType type,string name)
	{
		this.contentType = type;
		this.contentName = name;
	}
}
