using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Encryption;
using System;

public class Login_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.InputField usernameField;
    public UnityEngine.UI.InputField passwordField;
    public PasswordManager passwordManager;
    public async void captureCredentials(UnityEngine.UI.InputField usernameField, UnityEngine.UI.InputField passwordField) 
    {
      bool isLoggedIn = await passwordManager.Login(usernameField.text, passwordField.text);

        Debug.Log(isLoggedIn);
    }
}
