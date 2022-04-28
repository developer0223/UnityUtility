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

public class WebRequestorDemo : MonoBehaviour
{
    public Text txt_debug = null;

    private void Start()
    {
        //WebRequestorDev.SetBaseURL("http://192.168.1.250:8081/");
        WebRequestor.SetDefaultRequestHeaders(new Dictionary<string, string>()
        {
            { "Content-Type", "application/json" }
        });

        string email = "abc@gmail.com";
        string password = "test";
        AppendDebugText($"Login Start. email: {email}, password: {password}");
        Login(email, password);
    }

    [Serializable]
    public class LoginReq
    {
        public string email = string.Empty;
        public string password = string.Empty;
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

        WebRequestor.Post("http://192.168.1.250:8081/api/login", bodyData, (responseCode, result) =>
        {
            Debug.Log($"Login Post. responseCode: {responseCode}");
            Debug.Log($"Login Post. result: {result}");
            if (responseCode == ResponseCode.OK)
            {
                JsonData data = JsonMapper.ToObject(result);

                string accessToken = data["Access_token"].StringValue();
                string refreshToken = data["Refresh_token"].StringValue();
                string accessTokenExpireDatetime = data["Access_token_expire"].StringValue();

                WebRequestor.SetJwtAccessToken(accessToken);
                WebRequestor.SetJwtRefreshToken(refreshToken);

                AppendDebugText($"JwtAccessToken: {accessToken}");
                AppendDebugText($"JwtRefreshToken: {refreshToken}");
                AppendDebugText($"accessTokenExpireDatetime: {accessTokenExpireDatetime}");

                DashBoard();
            }
        });
    }


    private void DashBoard()
    {
        string academy = "1";
        Dictionary<string, string> dashboardReq = new Dictionary<string, string>()
        {
            { nameof(academy), academy }
        };

        AppendDebugText($"academy: {academy}");
        WebRequestor.Get("http://192.168.1.250:8081/api/dashboard", dashboardReq, (responseCode, result) =>
        {
            Debug.Log($"DashBoard. responseCode:\n{responseCode}");
            Debug.Log($"DashBoard. result:\n{result}");

            AppendDebugText($"DashBoard. responseCode:\n{responseCode}");
            AppendDebugText($"DashBoard. result:\n{result}");
            if (responseCode == ResponseCode.OK)
            {
                JsonReader reader = new JsonReader(result);
                JsonData data = JsonMapper.ToObject(reader);

                AppendDebugText($"DashBoard. Success");
            }
        });
    }

    private void AppendDebugText(string text)
    {
        txt_debug.text += $"{text}\n";
    }
}