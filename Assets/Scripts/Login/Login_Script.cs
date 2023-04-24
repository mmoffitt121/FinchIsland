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
using System.IO;
using UnityEngine.SceneManagement;

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

        if (userName != "sysAdmin" && password != "sysAdmin")
        {
            StartCoroutine(passwordManager.Login(userName, password, s => data = s));
        }
        else
        {
            // if sysadmin, dont access mysql
            /*
            string path = Environment.CurrentDirectory + "\\save\\current_user.txt";

            if (File.Exists(path))
            {
                Debug.Log("File already exists at " + path + " Deleteing.. ");
                File.Delete(path);
            }

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("Email: sysAdmin");
                writer.WriteLine("ID: -1");
            }
            */
            SceneManager.LoadScene(1);
        }
    }
}
