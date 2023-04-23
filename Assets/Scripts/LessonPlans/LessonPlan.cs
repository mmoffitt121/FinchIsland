using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class LessonPlan
{
    public LessonNode[] nodes;

    public static implicit operator LessonPlan(LessonNode v)
    {
        throw new NotImplementedException();
    }
}
