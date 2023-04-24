using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneChanger : MonoBehaviour
{
    //Change to the scene specified by the parameter. The key for the ints is in the build settings.
    public void ChangeToScene(int sceneToChangeTo)
    {
        Application.LoadLevel(sceneToChangeTo);
    }
}
