using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizQuestion
{
    public string question;
    public QuestionType type;
    public string[] answers;
    public string[] answerResponses;
    public int[] answerIndex;

    [System.Serializable]
    public enum QuestionType
    {
        MultipleChoice,
        FreeResponse
    }
}
