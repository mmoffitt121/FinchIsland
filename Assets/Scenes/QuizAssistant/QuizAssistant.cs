using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using System.Diagnostics;

namespace OpenAI

{
    public class QuizAssistant : MonoBehaviour
    {
        // UI FIELDS (BUTTONS, SCROLL BAR, TEXT FIELD
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private Button Show_Answer;
        //[SerializeField] private Button Go_to_Scene;

        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;
        //Height value for Scroll Wheel
        private float height;
        //OPEN AI OBJ AUTHENTICATOR
        private OpenAIApi openai = new OpenAIApi("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");

        private string userInput;
        //INITIALIZING THE CHAT BOT TO SIMPLIFY QUIZ REQUESTS FOR THE FACILITATOR
        private string prompt = "Act as Quiz maker assistant for a teacher. Make multiple choice quiz questions about finches. Make sure to have only 1 correct choice out of 4 choices. DO NOT SHOW THE CORRECT ANSWER";

        //private string new_prompt = "Show the answers to the previous questions, make sure that they are matching the previous questions";
        //WAIT FOR BUTTON TO BE CLICKED TO SEND A COMPLETION REQUEST
        private void Start()
        {
            button.onClick.AddListener(SendReply);
            Show_Answer.onClick.AddListener(RevealAnswer);

        }

        //APPEND COMPLETION RESPONSE TO THE FINAL MESSAGE
        private void AppendMessage(string message, bool isUser = true)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(isUser ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }
        //SEND REPLY UTILITY FUNCTION FOR START()
        private async void SendReply()
        {
  
          
            print("Reply function print statement");
            userInput = inputField.text;
            prompt += $"{userInput}\nResponse: ";
            AppendMessage(userInput);
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            //CALL THE API AND CREATE A COMPLETION REQUEST WITH RESPECT TO THE GIVEN FACILITATOR PROMPTS IN JSON FORMAT
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = prompt,            //PROMPT FIELD
                Model = "text-davinci-003", //TEXT GENERATION MODEL 
                MaxTokens = 180             //MAX ALLOWED TOKENS 
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                AppendMessage(completionResponse.Choices[0].Text, false);
                prompt += $"{completionResponse.Choices[0].Text}";
            }
            else
            {
                Debug.LogWarning("API Key Authentication Error: No text was generated from this given prompt. Please provide a valid Secret Key");
            }

            button.enabled = true;
            inputField.enabled = true;
        }

        private async void RevealAnswer()
        {

            print("Reveal Answer print statement");
            //userInput = inputField.text;
            string temp = "Reveal the answer to the previous Questions";
            prompt += $"{temp}";

            AppendMessage(temp);

            Show_Answer.enabled = false;
            inputField.text = "";
            inputField.enabled = false;

            //CALL THE API AND CREATE A COMPLETION REQUEST WITH RESPECT TO THE GIVEN FACILITATOR PROMPTS IN JSON FORMAT
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = prompt,            //PROMPT FIELD
                Model = "text-davinci-003", //TEXT GENERATION MODEL 
                MaxTokens = 180             //MAX ALLOWED TOKENS 
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                AppendMessage(completionResponse.Choices[0].Text, false);
                prompt += $"{completionResponse.Choices[0].Text}";
            }
            else
            {
                Debug.LogWarning("API Key Authentication Error: No text was generated from this given prompt. Please provide a valid Secret Key");
            }

            //button.enabled = true;
            Show_Answer.enabled = true;
            inputField.enabled = true;
        }
    }
}
