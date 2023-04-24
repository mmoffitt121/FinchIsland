using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class LessonPlan
{
    public int id;
    public string name;
    public string description;
    public string createdDate;
    public List<LessonNode> nodes;

    public static implicit operator LessonPlan(LessonNode v)
    {
        throw new NotImplementedException();
    }

    public LessonPlan()
    {
        nodes = new List<LessonNode>();
    }
}
