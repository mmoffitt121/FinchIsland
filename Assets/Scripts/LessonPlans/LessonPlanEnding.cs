using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LessonPlanEnding : MonoBehaviour
{
    // Content we are updating and displaying to the user
    public TextMeshProUGUI pageContent;

    // Function to navigate the user to the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        pageContent.text = "Congratulations!\n\n" +
            "You have completed:\n\n" + 
            LessonPlanScriptor.lessonPlan.name + ".\n\n" + 
            "Click below to return to the main menu.";
    }
}
