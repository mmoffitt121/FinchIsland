using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizView : MonoBehaviour
{
    // Returns the user to the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    // Continues to the next lesson node
    public void Continue()
    {
        LessonPlanScriptor.HandleAdvance();
    }
}
