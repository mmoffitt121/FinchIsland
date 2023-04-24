using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class LessonPlanJSONWrapper
{
    public List<LessonPlan> lessonPlans;

    public LessonPlanJSONWrapper(List<LessonPlan> lessonPlans)
    {
        this.lessonPlans = lessonPlans;
    }

    public LessonPlanJSONWrapper(LessonPlan[] lessonPlans)
    {
        this.lessonPlans = lessonPlans.ToList();
    }
}
