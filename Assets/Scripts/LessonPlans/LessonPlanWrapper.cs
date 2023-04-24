using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LessonPlanWrapper : MonoBehaviour
{
    public LessonPlan lessonPlan;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI statisticsText;

    public GameObject editButton;
    public GameObject deleteButton;
    public GameObject startButton;

    public void DeleteLessonPlan()
    {
        FindObjectOfType<LessonPlanUI>().DeleteLessonPlan(lessonPlan);
    }

    public void EditLessonPlan()
    {
        FindObjectOfType<LessonPlanUI>().EditLessonPlan(lessonPlan);
    }

    public void StartLessonPlan()
    {
        LessonPlanScriptor.StartLessonPlan(lessonPlan);
    }

    public void SetLessonPlan(LessonPlan lessonPlan)
    {
        this.lessonPlan = lessonPlan;
        nameText.text = lessonPlan.name;
        descriptionText.text = lessonPlan.description;
        statisticsText.text = "Created: " + lessonPlan.createdDate + "\n"
            + "Articles: " + lessonPlan.nodes?.Where(n => n.type == NodeType.Article).Count() + "\n"
            + "Quizzes: " + lessonPlan.nodes?.Where(n => n.type == NodeType.Quiz).Count() + "\n";

        if (FindObjectOfType<LessonPlanBrowseUI>() == null)
        {
            startButton.SetActive(false);
        }
        else
        {
            deleteButton.SetActive(false);
            editButton.SetActive(false);
        }
    }
}
