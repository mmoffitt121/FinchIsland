using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonPlanWrapper : MonoBehaviour
{
    public LessonPlan lessonPlan;

    public void DeleteLessonPlan()
    {
        Destroy(this.gameObject);
    }

    public void EditLessonPlan()
    {

    }
}
