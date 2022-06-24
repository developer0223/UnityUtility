<br><br>


# How to use WebRequestor
```
영어로 작성할까 하다가 그냥 한국어로 작성하겠습니다!
```
<br><br>


# 초기 세팅
WebRequestor을 사용하기 위해서는 기본 헤더 설정, Base-URL 설정 등 초기 설정이 필요합니다.
또한 WebRequestor의 Instance는 외부에서 접근이 불가능하도록 구현되어있습니다.
static method를 사용하여 WebRequestor의 모든 기능에 접근할 수 있습니다.
<br><br><br><br><br>





## 헤더 설정
기본 헤더를 설정합니다.
<br>

### Default Header 설정
```
WebRequestor.SetDefaultRequestHeaders(new Dictionary<string, string>()
{
    { "Content-Type", "application/json" }
});
```
<br>

### Default Header 추가
Default Header에 값을 추가합니다.
기존에 해당 Key를 가진 Header가 추가되어있을 경우, 오류를 반환하지 않고 최신 값으로 갱신됩니다.
```
WebRequestor.AddDefaultRequestHeaders("Content-Type", "application/json");
```
<br>

### Default Header 제거
해당 Key를 가진 Default Header를 제거합니다.
```
WebRequestor.RemoveDefaultRequestHeaders("Content-Type");
```
<br>

### Default Header 초기화
Default Header를 초기화하는 함수는 따로 제공되지 않습니다.
Default Header를 초기화하고 싶을 경우 아래와 같이 사용하시기 바랍니다.
```
WebRequestor.SetDefaultRequestHeaders(null);

// or

WebRequestor.SetDefaultRequestHeaders(new Dictionary<string, string>());
```
<br><br><br><br><br>





## Base-URL 설정
요청이 보내질 서버의 IP주소 + 포트 번호를 url형식으로 입력해 기본 요청 URL을 설정합니다.
추후 API호출 시에는 앞의 Base-URL을 적지 않고 "/api/v1/user/login"의 형태로 호출이 가능합니다.
(요청 URL에 "http"가 포함되어있지 않으면 자동으로 Base-URL이 추가됩니다.

※   Base-URL을 잘못 설정한것에 대한 예외 처리는 제공하지 않습니다. Base-URL의 유효성 검증은 프로그래머가 직접 하여야 합니다.
```
WebRequestor.SetBaseURL("http://3.36.118.98:8080");
```
<br><br><br><br><br>





## 실제 사용
실세 사용 코드는 로그인을 기준으로 예제를 작성하겠습니다.
```
// 전송할 데이터에 대한 객체를 생성합니다.
// 통신 시 사용되는 모든 데이터의 형태는 WebApiSample.cs에 클래스 또는 구조체를 생성합니다.
// 요청이 필요한 모든 형식에 대해 클래스 또는 구조체를 직접 작성하는것이 어렵다면
// JSON string 형태의 전송 또는 Dictionary 형태의 전송도 지원합니다.
LoginReq loginReq = new LoginReq()
{
    email = email,
    password = password
};


// 로그인 요청에 필요한 데이터 LoginReq를 JSON 형태의 string으로 변환합니다.
string bodyData = JsonMapper.ToJson(loginReq);

// WebRequestor.Post(string url, string bodyData, Action<long, string> callback) 메서드를 이용하여
// 서버에 요청을 전송합니다.
// 수신되는 모든 responseCode에 대한 정보는 ResponseCode.cs에 상수로 정리되어있습니다.
// result는 JSON 형태의 string으로 반환되며 해당 데이터를 LitJson 라이브러리를 이용하여 파싱하여 사용합니다.
WebRequestor.Post("/api/v1/login", bodyData, (responseCode, result) =>
{
    if (responseCode == ResponseCode.OK)
    {
        JsonData data = JsonMapper.ToObject(result);

        string accessToken = data["Access_token"].GetStringValue();
        string refreshToken = data["Refresh_token"].GetStringValue();
        string accessTokenExpireDatetime = data["Access_token_expire"].GetStringValue();

        WebRequestor.SetJwtAccessToken(accessToken);
        WebRequestor.SetJwtRefreshToken(refreshToken);
    }
});
```
<br><br><br><br><br>





## 로그인 이후 세팅
로그인 API가 호출 된 이후에는 로그인 시 받아온 Access-Token, Refresh-Token을 로컬 메모리에 캐싱하는 과정이 필요합니다.
<br>

### Access-Token 캐싱
Access-Token을 설정합니다.
<br>

아래의 메서드 호출 시 자동으로 WebRequestor.AddDefaultRequestHeaders() 함수가 호출되며 Default Header를 자동으로 등록합니다.

```
WebRequestor.SetJwtAccessToken("your jwt access token here. (xxx.yyy.zzz)");
```
<br>

Login 이후 Default Header를 수정할 시 인증 토큰 헤더가 제대로 전송되지 않을 수 있으며 초기화 이후 아래와 같이 코드를 작성하여 헤더를 재설정해야합니다.
<br>

이 부분은 불안정한 부분으로 Login 이후 Default Header 초기화가 불가피한 상황이 있을 시 개발자에게 문의 바랍니다.
```
WebRequestor.AddDefaultRequestHeaders("Authorization", WebRequestor.GetJwtAccessToken());
```
<br>

### Refresh-Token 캐싱
Refresh-Token을 설정합니다.
```
WebRequestor.SetJwtRefreshToken("your jwt refresh token here. (xxx.yyy.zzz)");
```


