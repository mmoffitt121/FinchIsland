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
            Debug.Log("HERE");
            string hash = "";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

            string url = $"http://localhost/app/access.php?email={email}&pass={hash}";

            Debug.Log(url);

            UnityWebRequest request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else if (request.downloadHandler.text != "")
            {
                Debug.Log(request.downloadHandler.text);

                string[] parsedata = request.downloadHandler.text.Split(',');

                string path = Environment.CurrentDirectory + "\\save\\current_user.txt";

                if (File.Exists(path))
                {
                    Debug.Log("File already exists at " + path + "Deleteing.. ");
                    File.Delete(path);
                }

                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine("Email: " + email);
                    writer.WriteLine("ID: " + parsedata[0]);
                }
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("Invalid Login!");
            }
        }
    }

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