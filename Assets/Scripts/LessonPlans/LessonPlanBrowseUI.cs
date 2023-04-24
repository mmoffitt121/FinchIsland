using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonPlanBrowseUI : MonoBehaviour
{
    // Premade lesson plans UI
    public GameObject premadePlans;
    // User-created lesson plans UI
    public GameObject planBrowse;

    public void SwitchBetweenBrowseAndPremade(bool isPremade)
    {
        if (isPremade)
        {
            premadePlans.SetActive(true);
            planBrowse.SetActive(false);
        }
        else
        {
            premadePlans.SetActive(false);
            planBrowse.SetActive(true);
        }
    }
}
