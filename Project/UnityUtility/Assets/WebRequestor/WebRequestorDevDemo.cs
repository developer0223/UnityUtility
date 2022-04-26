// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Project
using developer0223.WebRequestor;
using LitJson;

// Alias

public class WebRequestorDevDemo : MonoBehaviour
{
    private void Start()
    {
        //WebRequestorDev.SetBaseURL("http://192.168.1.250:8081/");
        //WebRequestorDev.SetDefaultRequestHeaders(new Dictionary<string, string>()
        //{
        //    { "Content-Type", "application/json" }
        //});

        //Login("abc@gmail.com", "test");
        Test();
    }

    [Serializable]
    private class LoginReq
    {
        public string email;
        public string password;
    }

    private void Login(string email, string password)
    {
        LoginReq loginReq = new LoginReq()
        {
            email = email,
            password = password
        };

        string bodyData = JsonMapper.ToJson(loginReq);
        Debug.Log($"bodyData : {bodyData}");
        WebRequestorDev.Post("http://192.168.1.107:8081/api/login", bodyData, (responseCode, result) =>
        {
            Debug.Log($"Login Post. responseCode: {responseCode}");
            Debug.Log($"Login Post. result: {result}");
            if (responseCode == ResponseCode.OK)
            {
                JsonData data = JsonMapper.ToObject(result);

                string accessToken = data["Access_token"].StringValue();
                string refreshToken = data["Refresh_token"].StringValue();
                string accessTokenExpireDatetime = data["Access_token_expire"].StringValue();

                WebRequestorDev.SetJwtAccessToken(accessToken);
                WebRequestorDev.SetJwtRefreshToken(refreshToken);
            }
        });
    }

    private void Test()
    {
        //WebRequestorDev.Get("https://jsonplaceholder.typicode.com/users", new Dictionary<string, string>(), (responseCode, result) =>
        WebRequestorDev.Get("http://192.168.1.107:8081/api/test", new Dictionary<string, string>(), (responseCode, result) =>
        {
            Debug.Log($"Test Get. responseCode:\n{responseCode}");
            Debug.Log($"Test Get. result:\n{result}");
            if (responseCode == ResponseCode.OK)
            {
                JsonReader reader = new JsonReader(result);
                JsonData data = JsonMapper.ToObject(reader);
            }
        });
    }
}