// Include external libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonPlanBrowseUI : MonoBehaviour
{
    // Premade lesson plans UI
    public GameObject premadePlans;
    // User-created lesson plans UI
    public GameObject planBrowse;

    // Create function to switch between browse and premade lesson plans
    public void SwitchBetweenBrowseAndPremade(bool isPremade)
    {
        // If the premade lesson plan button is clicked, go to the premade lesson page and siplay the premade lessons
        if (isPremade)
        {
            premadePlans.SetActive(true);
            planBrowse.SetActive(false);
        }
        // Else, go to the user created (teacher) lesson plans and display the lessons which have been created
        else
        {
            premadePlans.SetActive(false);
            planBrowse.SetActive(true);
        }
    }
}
