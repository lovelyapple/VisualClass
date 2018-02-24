using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassInfo : FieldObjectBase
{
    public ClassInfo parentInfo { get; private set; }
    public ClassInfo childInfo { get; private set; }
    public string className;
    public List<ClassContentInfo> contentList;
	
}
