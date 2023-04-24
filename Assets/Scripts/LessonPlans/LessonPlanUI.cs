using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using System.Globalization;
using Unity.VisualScripting;
using TMPro;
using OpenAI;
using UnityEngine.UI;
using TMPro.EditorUtilities;
using static TreeEditor.TreeEditorHelper;
using JetBrains.Annotations;

public class LessonPlanUI : MonoBehaviour
{
    // Prefab for spawning lesson plan UI
    public GameObject lessonPlanPrefab;
    // The gameobject displaying the list of lesson plans
    public GameObject lessonPlanList;
    // The list of all lesson plans
    public LessonPlan[] lessonPlans;
    // The lesson plan currently selected for editing
    public LessonPlan lessonPlan;

    // The Lesson Plan Editor UI
    public GameObject lessonPlanEditor;
    // The Lesson Plan Viewer UI
    public GameObject lessonPlanViewer;

    // The title viewed in the editor
    public TextMeshProUGUI lessonPlanEditorTitle;

    // UI that controls the properties of the lesson plan
    public GameObject lessonProperties;
    // UI that controls the properties of the quiz
    public GameObject quizProperties;
    // UI that controls the properties of the simulation
    public GameObject simulationProperties;
    // UI that controls the properties of the article
    public GameObject articleProperties;

    // Prefab for spawning lesson plan nodes
    public GameObject nodePrefab;
    // The gameobject displaying the list of nodes
    public GameObject nodeList;

    // Fields for the name and description of the lesson plan
    public TMP_InputField planNameInput;
    public TMP_InputField planDescriptionInput;
    public TextMeshProUGUI planNameField;
    public TextMeshProUGUI planDescriptionField;

    // The currently selected lesson node
    public LessonNode node;

    // Inputs for the names and content of articles
    public TMP_InputField articleNameInput;
    public TMP_InputField articleContentInput;

    // Input for the number of rounds in a simulation
    public TMP_InputField simulationRoundsInput;
    // String that allows us to analyze the value of the input to make sure it's valid.
    private int simulationRoundsValue;

    #region Editor
    public void AddQuizNode()
    {
        AddNode(NodeType.Quiz);
    }

    public void AddSimulationNode()
    {
        AddNode(NodeType.Simulation);
    }

    public void AddArticleNode()
    {
        AddNode(NodeType.Article);
    }

    public void AddNode(NodeType type)
    {
        LessonNode n = new LessonNode();
        n.type = type;
        lessonPlan.nodes.Add(n);
        DisplayNodes();
    }

    // Shows all lesson plans in the UI to the user
    public void DisplayNodes()
    {
        RemoveNodeDisplay();
        foreach (LessonNode node in lessonPlan.nodes)
        {
            GameObject n = Instantiate(nodePrefab);
            n.transform.SetParent(nodeList.transform, false);
            n.GetComponent<LessonNodeWrapper>().SetNode(node);
        }
    }

    // Removes all lesson plans from the display
    public void RemoveNodeDisplay()
    {
        foreach (Transform child in nodeList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Function called to choose which type of node we are editing
    public void ChooseNodeTypeToEdit(EditNodeType nodeType)
    {
        lessonProperties.SetActive(false);
        quizProperties.SetActive(false);
        simulationProperties.SetActive(false);
        articleProperties.SetActive(false);
        switch (nodeType)
        {
            case EditNodeType.LessonPlan:
                lessonProperties.SetActive(true);
                break;
            case EditNodeType.Quiz:
                quizProperties.SetActive(true);
                break;
            case EditNodeType.Simulation:
                simulationProperties.SetActive(true);
                simulationRoundsInput.text = node.runs.ToString();
                simulationRoundsValue = node.runs;
                break;
            case EditNodeType.Article:
                articleNameInput.text = node.articleTitle;
                articleContentInput.text = node.articleContent;
                articleProperties.SetActive(true);
                break;
        }
    }

    // Function called to cancel the user's changes
    public void CancelEdit()
    {
        lessonPlanEditor.SetActive(false);
        lessonPlanViewer.SetActive(true);
        LoadLessonPlanList();
    }

    // Function called to save the user's changes
    public void SaveEdit()
    {
        lessonPlan.name = planNameField.text;
        lessonPlan.description = planDescriptionField.text;
        lessonPlanEditor.SetActive(false);
        lessonPlanViewer.SetActive(true);
        SaveLessonPlanList();
        LoadLessonPlanList();
        DisplayLessonPlans();
    }

    // Function called to delete a specific lesson node
    public void DeleteNode(LessonNode node)
    {
        lessonPlan.nodes.Remove(node);
        DisplayNodes();
    }

    public void SaveArticle()
    {
        node.articleTitle = articleNameInput.text;
        node.articleContent = articleContentInput.text;
        DisplayNodes();
        ChooseNodeTypeToEdit(EditNodeType.LessonPlan);
    }

    // Function called to edit a particular lesson node
    public void EditNode(LessonNode node)
    {
        this.node = node;
        ChooseNodeTypeToEdit((EditNodeType)node.type);
    }

    public void SetSimulationRounds()
    {
        int result;
        if (Int32.TryParse(simulationRoundsInput.text, out result))
        {
            simulationRoundsValue = result;
            node.runs = simulationRoundsValue;
        }
        else
        {
            simulationRoundsInput.text = simulationRoundsValue.ToString();
        }
    }
    #endregion

    #region Viewer
    // Adds a lesson plan to the list and the screen.
    public void CreateLessonPlan()
    {
        LessonPlan newLesson = new LessonPlan();
        newLesson.id = GetUniqueId();
        newLesson.createdDate = DateTime.Today.ToString("d");
        newLesson.name = "Lesson Plan " + newLesson.id;
        newLesson.description = "";
        lessonPlans = lessonPlans.Append(newLesson).ToArray();
        DisplayLessonPlans();
        SaveLessonPlanList();
    }

    public void EditLessonPlan(LessonPlan lp)
    {
        lessonPlan = lp;
        lessonPlanEditor.SetActive(true);
        lessonPlanViewer.SetActive(false);
        lessonPlanEditorTitle.text = lessonPlan.name;
        planNameInput.text = lp.name;
        planDescriptionInput.text = lp.description;
        ChooseNodeTypeToEdit(EditNodeType.LessonPlan);
        DisplayNodes();
    }

    public void DeleteLessonPlan(LessonPlan lp)
    {
        List<LessonPlan> temp = lessonPlans.ToList();
        temp.Remove(lp);
        lessonPlans = temp.ToArray();
        SaveLessonPlanList();
        DisplayLessonPlans();
    }

    public void SaveLessonPlanList()
    {
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath + "/Lesson Plans");
        foreach (string file in filePaths)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        foreach (LessonPlan l in lessonPlans)
        {
            string json = JsonUtility.ToJson(l);
            string filePath = Application.persistentDataPath + "/Lesson Plans";
            if (!File.Exists(filePath)) { Directory.CreateDirectory(filePath); }
            File.WriteAllText(filePath + "/Lesson Plan_" + l.id + "_" + l.name + ".json", json);
        }
    }

    // Loads the list of lesson plans from the user's json.
    public void LoadLessonPlanList()
    {
        List<LessonPlan> lps = new List<LessonPlan>();
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath + "/Lesson Plans");
        foreach (string file in filePaths)
        {
            if (File.Exists(file))
            {
                string json = File.ReadAllText(file);
                lps.Add(JsonUtility.FromJson<LessonPlan>(json));
            }
        }

        lessonPlans = lps.ToArray();
    }

    // Shows all lesson plans in the UI to the user
    public void DisplayLessonPlans()
    {
        RemoveLessonPlanDisplay();
        foreach (LessonPlan plan in lessonPlans)
        {
            GameObject lp = Instantiate(lessonPlanPrefab);
            lp.transform.SetParent(lessonPlanList.transform, false);
            lp.GetComponent<LessonPlanWrapper>().SetLessonPlan(plan);
        }
    }

    // Removes all lesson plans from the display
    public void RemoveLessonPlanDisplay()
    {
        foreach (Transform child in lessonPlanList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // Function to get unique lesson plan ID from the array of lesson plans
    public int GetUniqueId()
    {
        int i = 0;
        bool found = true;
        while (true)
        {
            foreach (LessonPlan lp in lessonPlans)
            {
                if (lp.id == i)
                {
                    found = false;
                    break;
                }
            }

            if (found)
            {
                return i;
            }
            found = true;
            i++;
        }
    }
    #endregion

    private void Start()
    {
        LoadLessonPlanList();
        DisplayLessonPlans();
    }
}

[Serializable]
public enum EditNodeType
{
    Simulation,
    Article,
    Quiz,
    LessonPlan
}

[Serializable]
public enum NodeType
{
    Simulation,
    Article,
    Quiz
}