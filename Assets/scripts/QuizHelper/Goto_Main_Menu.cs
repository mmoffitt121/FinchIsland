using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Goto_Main_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Enter_MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
