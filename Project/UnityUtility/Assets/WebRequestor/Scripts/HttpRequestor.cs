// System
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Collections;
using System.Collections.Generic;

// Unityv
using UnityEngine;
using UnityEngine.UI;

// Project
// Alias

public class HttpRequestor : MonoBehaviour
{
    /*
     * [POST] 192.168.1.250:8081/api/login?email=abc@gmail.com
     * 현재 id 존재& token값 존재 일때 -> status 200
     * id 미존재 -> status 400
     * 에러 -> status 500
     * 형태로 반환되고 refresh token은 고려하지 않은 상태로 반환됩니다. 이 부분은 코드추가해서 수정해두겠습니다.
     */
    private class Url
    {
        public static string SERVER_BASE_URL = "192.168.1.250:8081";
    }

    private static HttpRequestor Instance = null;
    private static HttpClient client = new HttpClient();

    private static string refreshToken = string.Empty;
    private static string accessToken = string.Empty;

    public static HttpRequestor GetOrCreate()
    {
        if (Instance == null)
        {
            GameObject gameObject = new GameObject(nameof(HttpRequestor));
            Instance = gameObject.GetComponent<HttpRequestor>();
        }

        return Instance;
    }

    private static void Initialize(string hostBaseUrl)
    {
        client = new HttpClient
        {
            BaseAddress = new Uri(hostBaseUrl)
        };
    }

    private static void SetJwtToken(string jwtToken)
    {
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
    }

    //////////////////////////////////////////////////
    
    public static void SetJwtRefreshToken()
    {

    }

    public static void SetJwtAccessToken()
    {

    }

    public static void RequestAuthorization()
    {

    }
}