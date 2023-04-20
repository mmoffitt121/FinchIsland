using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Encryption;
using System;
using TMPro;
using System.Security.Cryptography;
using UnityEngine.Networking;
using System.Text;

public class Login_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public Button loginButton;
    public PasswordManager passwordManager;


    void Start()
    {
        //Subscribe to onClick event
        loginButton.onClick.AddListener(captureCredentials);
    }

    public void captureCredentials() 
    {
        string userName = usernameField.text;
        string password = passwordField.text;
        passwordManager = new PasswordManager();

        string data = "";

        StartCoroutine(passwordManager.Login(userName, password, s => data = s));
    }
}
