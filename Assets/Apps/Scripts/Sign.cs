using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;

public class SignUp : MonoBehaviour
{
    public InputField emailInput;
    public InputField passwordInput;
    public InputField rePasswordInput;
    public Button submitButton;
    public class SignUpData
    {
        public string email;
        public string password;
        public string createDate;
    }
    // Start is called before the first frame update
    void Start()
    {
        submitButton.onClick.AddListener(OnSubmit);
    }

    // Update is called once per frame
    void OnSubmit()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string rePassword = rePasswordInput.text;

        if (password != rePassword)
        {
            Debug.LogError("Passwords do not match!");
            return;
        }

        SignUpData data = new SignUpData
        {
            email = email,
            password = password,
            createDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss")
        };

        StartCoroutine(SendToServer(data));
    }

    IEnumerator SendToServer(SignUpData data)
{
    string url = "https://binusgat.rf.gd/unity-api-test/api/auth/signup.php";

    WWWForm form = new WWWForm();
    form.AddField("email", data.email);
    form.AddField("password", data.password);
    form.AddField("createDate", data.createDate);

    UnityWebRequest request = UnityWebRequest.Post(url, form);
    yield return request.SendWebRequest();

    if (request.result == UnityWebRequest.Result.Success)
    {
        Debug.Log("Successful: " + request.downloadHandler.text);
    }
    else
    {
        Debug.LogError("Error: " + request.error);
    }
}
}