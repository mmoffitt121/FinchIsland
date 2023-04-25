using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/*class lesson planwrapper */
public class LessonPlanWrapper : MonoBehaviour
{
    public LessonPlan lessonPlan;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI statisticsText;

    public GameObject editButton;
    public GameObject deleteButton;
    public GameObject startButton;

    /*function to delete a lesson plan*/
    public void DeleteLessonPlan()
    {
        FindObjectOfType<LessonPlanUI>().DeleteLessonPlan(lessonPlan);
    }

    /*function to edit a lesson plan*/
    public void EditLessonPlan()
    {
        FindObjectOfType<LessonPlanUI>().EditLessonPlan(lessonPlan);
    }

    /*function to start a lesson plan */
    public void StartLessonPlan()
    {
        LessonPlanScriptor.StartLessonPlan(lessonPlan);
    }

    /*function to display lesson plans on the screen */
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
