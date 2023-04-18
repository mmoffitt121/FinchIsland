using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LessonPlanUI : MonoBehaviour
{
    public GameObject lessonPlanPrefab;
    public GameObject lessonPlanList;
    public LessonPlan lessonPlan;

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
        lessonPlan = lp;
    }

    public void SaveLessonPlanList()
    {
        string json = JsonUtility.ToJson(lessonPlan);

        string filePath = Application.persistentDataPath + "/lessonplanlist.json";
        File.WriteAllText(filePath, json);
    }

    public void LoadLessonPlanList()
    {
        string filePath = Application.persistentDataPath + "/lessonplanlist.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            lessonPlan = JsonUtility.FromJson<LessonPlan>(json);
        }
        // If you're gonna save and load multiple lesson plans, put this in a foreach
        GameObject lp = GameObject.Instantiate(lessonPlanPrefab);
        lp.transform.SetParent(lessonPlanList.transform, false);
        lp.GetComponent<LessonPlanWrapper>().lessonPlan = lessonPlan; // Sets that lesson plan to the prefab object
    }
}

