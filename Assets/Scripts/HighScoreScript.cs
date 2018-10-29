using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HighScoreScript {
    const string API_URL = "https://iggyvolz.com/dodge/api.php";

    public static async Task<string> GetHighScores(int limit = 10)
    {
        WWWForm form = new WWWForm();
        form.AddField("limit", limit);
        WWW request = new WWW(API_URL + "?method=GetHighScores", form);
        while (!request.isDone) await Task.Delay(10);
        return request.text;
    }

    public static async Task UploadHighScore(string username, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("username",username);
        form.AddField("score",score);
        WWW request = new WWW(API_URL + "?method=UploadHighScore", form);
        while (!request.isDone) await Task.Delay(100);
    }

}
