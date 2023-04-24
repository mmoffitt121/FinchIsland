using System.Security.Cryptography;
using System.Text;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

namespace Encryption
{
    public class PasswordManager
    {
        public IEnumerator Login(string email, string password, System.Action<string> OnData)
        {
            // setup password to be hashed
            string hash = "";
            using (SHA256 sha256 = SHA256.Create())
            {
                //encode the password into individual bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                //hash password
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                //replace some characters that are not valid in SQL
                hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

            //server link
            string url = $"http://localhost/app/access.php?email={email}&pass={hash}";

            UnityWebRequest request = UnityWebRequest.Get(url);

            // send the get request to apache
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else if (request.downloadHandler.text != "")
            {
                // extract data from get
                string[] parsedata = request.downloadHandler.text.Split(',');

                // create path for current user, because we dont use dependency injection
                string path = Environment.CurrentDirectory + "\\save\\current_user.txt";

                if (File.Exists(path))
                {
                    // delete file if one already exisats
                    Debug.Log("File already exists at " + path + " Deleteing.. ");
                    File.Delete(path);
                }

                // create text file and write to text file
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine("Email: " + email);
                    writer.WriteLine("ID: " + parsedata[0]);
                }
                // load main menu
                SceneManager.LoadScene(1);
            }
            else
            {
                // invalid login
                Debug.Log("Invalid Login!");
            }
        }
    }

    // if we used dependency injection...
    public class Account
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int MaxSubLicenses { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}