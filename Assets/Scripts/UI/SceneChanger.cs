using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneChanger : MonoBehaviour
{
    public void ChangeToScene(int sceneToChangeTo)
    {
        Application.LoadLevel(sceneToChangeTo);
    }
}
