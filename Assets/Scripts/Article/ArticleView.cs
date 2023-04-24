using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArticleView : MonoBehaviour
{
    // UI holding article title
    public TextMeshProUGUI title;
    // UI holding article content
    public TextMeshProUGUI content;

    private void Start()
    {
        // If we're not in a lesson plan currently, do not initialize. It will only lead to heartbreak.
        if (!LessonPlanScriptor.IsInLessonPlan())
        {
            return;
        }

        title.text = LessonPlanScriptor.GetCurrentNode().articleTitle;
        content.text = LessonPlanScriptor.GetCurrentNode().articleContent;
    }

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
