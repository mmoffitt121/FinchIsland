using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonPlanUI : MonoBehaviour
{
    public GameObject lessonPlanPrefab;
    public GameObject lessonPlanList;
    public void CreateLessonPlan()
    {
        GameObject lp = Instantiate(lessonPlanPrefab);
        lp.transform.SetParent(lessonPlanList.transform, false);

    }
    public void DeleteLessonPlan() 
    {
        Destroy(this.gameObject);

    }
    public void EditLessonPlan(LessonPlan lp)
    {

    }
}
