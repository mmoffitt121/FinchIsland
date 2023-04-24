using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LessonNode
{
    public string name;
    public int id;

    // Determines the type of this node
    public NodeType type;

    // Simulation Node Items

    // Quiz Node Items
    public QuizQuestion[] questions;

    // Article Node Items
    public string articleTitle;
    public string articleContent;
}
