//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using GooglePlayGames.BasicApi;
//using GooglePlayGames;
//using System.IO;
//using System;

//public class API : MonoBehaviour {
//    //string URL = "http://www.omdbapi.com/?t=avengers&type=movie&apikey=e15d9329";
//    public Text responseText;
//    private string access_token = "";
//    private string jsonPath;
//    private int timeInterval = 10; //How far back in time(minutes) the data will be gathered from the Google fit API

//    public void Awake()
//    {
//        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
//            // requests an ID token be generated.  This OAuth token can be used to
//            //  identify the player to other services such as Firebase.
//            .RequestIdToken()
//            .Build();
//        PlayGamesPlatform.InitializeInstance(config);
//        PlayGamesPlatform.Activate();
//    }

//    // Use this for initialization
//    void Start () {
//        jsonPath = Application.streamingAssetsPath + "/postrequest.json";
//    }

//    public void Request()
//    { 
//        //Login into google play games
//        Social.localUser.Authenticate((bool success) => {
//            //if succeeded we can collect the authentication token needed for the request to the Google fit API
//            if (success){
//                Debug.Log("You've successfully logged in");
//                access_token = ((PlayGamesLocalUser) Social.localUser).GetIdToken();
//                //Debug.Log(access_token);
//                responseText.text = "ja";
//                POST();
//            }
//            else{
//                Debug.Log("Login failed for some reason");
//                responseText.text = "NEJw";
//            }
//        });
////		string URL = "https://www.googleapis.com/fitness/v1/users/me/dataSources?access_token=" + access_token;
////        WWW request = new WWW(URL);
////        StartCoroutine(OnResponse(request));
//    }

//    public void Requestttttttt()
//    {
//        //Login into google play games
//        Social.localUser.Authenticate((bool success) =>
//        {

//        });
//    }

////    private IEnumerator OnResponse(WWW req)
////    {
////        yield return req;
////
////		responseText.text = req.text;
////    }

//    public WWW POST() //inte helt säker på att den är helt rätt
//    {
//        //Creating the request body in form of a Json file for our post request
//        CreatingRequestBody(timeInterval);
//        //Getting the Json for our post request to Google fit API
//        string jsonString = File.ReadAllText (jsonPath);

//        WWW www;
//        Dictionary<string,string> postHeader = new Dictionary<string,string>();
//        postHeader.Add("Content-type", "application/json");

//        // convert json string to byte
//        var requestBody = System.Text.Encoding.UTF8.GetBytes(jsonString);

//        www = new WWW("https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate?access_token=" + access_token, requestBody, postHeader);
//        StartCoroutine(WaitForRequest(www));
//        return www;
//    }

//    IEnumerator WaitForRequest(WWW data)
//    {
//        yield return data; // Wait until the download is done
//        if (data.error != null)
//        {
//            Debug.Log("There was an error sending request: " + data.error);
//        }
//        else
//        {
//            Debug.Log("WWW Request: " + data.text);
//            responseText.text = data.text;
//        }	
//    }

//    /// <summary>
//    /// Writing the request body to a json file.
//    /// </summary>
//    /// <param name="timeInterval">The interval to check. (minutes)</param>
//    private void CreatingRequestBody(int timeInterval)
//    {
//        var epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
//        StreamWriter writer = new StreamWriter(jsonPath, false);
//        long endTimeMillis = ((long)epoch);
//        long startTImeMillis = endTimeMillis - ((long)timeInterval * 60000);
//        writer.WriteLine("{\n\"aggregateBy\":[\n{\n\"dataSourceId\":\"derived:com.google.step_count.delta:com.google.android.gms:estimated_steps\"\n}\n],\n\"bucketByTime\":{\"durationMillis\":86400000},\n\"startTimeMillis\":"+startTImeMillis+",\n\"endTimeMillis\":"+endTimeMillis+"\n}");
//        writer.Close();
//    }

//    void OnApplicationQuit()
//    {
//        PlayGamesPlatform.Instance.SignOut();
//    }
//}

////private string URL = "http://www.omdbapi.com/?t=avengers&type=movie&apikey=e15d9329";
////"https://www.googleapis.com/fitness/v1/users/me/dataSources";
////URL = "https://www.googleapis.com/fitness/v1/users/me/dataSources?access_token=" + access_token;
