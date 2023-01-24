using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    public int scene;
    public void EnterSandbox()
    {
        SceneManager.LoadScene(scene);
    }
}
