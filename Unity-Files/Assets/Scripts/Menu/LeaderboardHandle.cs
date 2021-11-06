using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Net;
public class LeaderboardHandle : MonoBehaviour
{
    public InputField submitUsername;
    public GameObject GameOverPanel;
    public float time { get; set; }
    public int finalScore { get; set; }

    private MySqlConnectionStringBuilder mySQLConectionBuilder = new MySqlConnectionStringBuilder();


    // Start is called before the first frame update
    void Start()
    {
        mySQLConectionBuilder.Server = "sql4.freesqldatabase.com";
        mySQLConectionBuilder.Port = 3306;
        mySQLConectionBuilder.UserID = "sql4449219";
        mySQLConectionBuilder.Password = "hsFqWLxIIF";
        mySQLConectionBuilder.Database = "sql4449219";
    }

    public void SubmitScores()
    {
        SetScoreRecords(mySQLConectionBuilder.ConnectionString, submitUsername.text);
        GameOverPanel.GetComponent<Animator>().enabled = true;

        List<Score> Scores = new List<Score>(GetScoreRecords(mySQLConectionBuilder.ConnectionString));
        for (int i = 0; i < Scores.Count; i++)
        {
            Text scoreLabel = GameObject.Find($"ScoreText ({i + 1})").GetComponent<Text>();
            string editedText = scoreLabel.text.Replace("AAA", $"{Scores[i].username}").Replace("111", $"{Scores[i].score}").Replace("222", $"{Scores[i].timeLasted}");
            scoreLabel.text = editedText;
        }
    }

    private void SetScoreRecords(string connectionString, string username)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO Scores VALUES ('{username.ToUpper()}', '{finalScore}', '{Mathf.FloorToInt(time)}', '{GetIPAddress()}')";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (MySqlException e)
        {
            Debug.Log(e.ToString());
        }
    }

    private List<Score> GetScoreRecords(string connectionString)
    {
        List<Score> records = new List<Score>();
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Scores ORDER BY Score DESC LIMIT 5";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new Score(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2)));
                        }
                    }
                }
            }
        }
        catch (MySqlException e)
        {
            Debug.Log(e.ToString());
        }
        return records;
    }

    public class Score
    {
        public string username { get; }
        public int score { get; }
        public int timeLasted { get; }

        public Score(string username, int score, int timeLasted)
        {
            this.username = username;
            this.score = score;
            this.timeLasted = timeLasted;
        }
    }

    static string GetIPAddress()
    {
        return new WebClient().DownloadString("https://api.ipify.org").TrimEnd();
    }
}
