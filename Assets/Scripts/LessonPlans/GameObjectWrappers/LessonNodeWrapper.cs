using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LessonNodeWrapper : MonoBehaviour
{
    public LessonNode node;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public static int maxTitleLength = 100;
    public static int maxDescLength = 300;
    public void SetNode(LessonNode node)
    {
        this.node = node;
        switch (node.type)
        {
            case NodeType.Article:
                title.text = "Article";
                description.text =
                    (node.articleTitle.Length < maxTitleLength ? node.articleTitle : node.articleTitle.Substring(0, maxTitleLength) + "...")
                    + "\n\n" +
                    (node.articleContent.Length < maxDescLength ? node.articleContent : node.articleContent.Substring(0, maxDescLength) + "...");
                break;
            case NodeType.Simulation:
                title.text = "Simulation";
                description.text =
                    "Runs: " + node.runs;
                break;
            case NodeType.Quiz:
                title.text = "Quiz";
                break;
        }
    }

    public void DeleteNode()
    {
        FindObjectOfType<LessonPlanUI>().DeleteNode(node);
    }

    public void EditNode()
    {
        FindObjectOfType<LessonPlanUI>().EditNode(node);
    }
}
