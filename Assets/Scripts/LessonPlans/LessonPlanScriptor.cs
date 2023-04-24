using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LessonPlanScriptor
{
    // Whether or not we are in a lesson plan
    public static bool inLessonPlan = false;
    // What lesson plan we are currently using
    public static LessonPlan lessonPlan;
    // What node in the lesson plan we are at
    public static int lpProgress;

    public static int simulationRuns = 0;

    public static bool IsInLessonPlan()
    {
        return inLessonPlan && lessonPlan != null && lessonPlan.nodes != null && lpProgress < lessonPlan.nodes.Count;
    }

    public static void ResumeLessonPlan()
    {

    }

    public static LessonNode GetCurrentNode()
    {
        return lessonPlan.nodes[lpProgress];
    }

    public static void StartLessonPlan(LessonPlan lp)
    {
        lessonPlan = lp;
        inLessonPlan = true;
        lpProgress = 0;

        if (lp != null && lp.nodes != null && lp.nodes.Count > 0 && !(lpProgress >= lessonPlan.nodes.Count))
        {
            switch (lessonPlan.nodes[lpProgress].type)
            {
                case NodeType.Article:
                    SceneManager.LoadScene(12);
                    break;
                case NodeType.Simulation:
                    SceneManager.LoadScene(6);
                    break;
                case NodeType.Quiz:
                    SceneManager.LoadScene(13);
                    break;
            }
        }
        else if (lpProgress >= lessonPlan.nodes.Count)
        {
            lessonPlan = null;
            inLessonPlan = false;
            SceneManager.LoadScene(11);
        }
        else
        {
            lessonPlan = null;
            inLessonPlan = false;
            SceneManager.LoadScene(1);
            return;
        }

        
    }

    public static void HandleAdvance()
    {
        if (!inLessonPlan)
        {
            SceneManager.LoadScene(1);
            return;
        }

        lpProgress += 1;
        if (lpProgress >= lessonPlan.nodes.Count)
        {
            SceneManager.LoadScene(11);
            return;
        }

        switch(lessonPlan.nodes[lpProgress].type)
        {
            case NodeType.Article:
                SceneManager.LoadScene(12);
                break;
            case NodeType.Simulation:
                SceneManager.LoadScene(6);
                break;
            case NodeType.Quiz:
                SceneManager.LoadScene(13);
                break;
        }
    }

    public static void StartSandbox()
    {
        lessonPlan = null;
        inLessonPlan = false;
        simulationRuns = 0;
    }
}
