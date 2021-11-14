using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Net;
using System.Threading.Tasks;
public class LeaderboardHandle : MonoBehaviour
{
    public InputField submitUsername;
    public GameObject GameOverPanel;
    public GameObject FetchingScores;
    public float time { get; set; }
    public int finalScore { get; set; }

    private MySqlConnectionStringBuilder mySQLConnectionBuilder = new MySqlConnectionStringBuilder();

    // Start is called before the first frame update
    void Start()
    {
        mySQLConnectionBuilder.Server = "sql4.freesqldatabase.com";
        mySQLConnectionBuilder.Port = 3306;
        mySQLConnectionBuilder.UserID = "sql4449219";
        mySQLConnectionBuilder.Password = "hsFqWLxIIF";
        mySQLConnectionBuilder.Database = "sql4449219";

        GameObject.Find("FinalScoreLabel").GetComponent<Text>().text = GameObject.Find("FinalScoreLabel").GetComponent<Text>().text.Replace("000", finalScore.ToString());
    }

    public async void SubmitScores()
    {
        GameObject.Find("GameOverLabel").SetActive(false);
        FetchingScores.GetComponent<FetchingScoresAnimation>().enabled = true;
        await SetScoreRecords(mySQLConnectionBuilder.ConnectionString, submitUsername.text);
        List<Score> Scores = new List<Score>(await GetScoreRecords(mySQLConnectionBuilder.ConnectionString));
        FetchingScores.GetComponent<FetchingScoresAnimation>().enabled = false;
        FetchingScores.SetActive(false);
        GameOverPanel.GetComponent<Animator>().enabled = true;
        for (int i = 0; i < Scores.Count; i++)
        {
            Text scoreLabel = GameObject.Find($"ScoreText ({i + 1})").GetComponent<Text>();
            string editedText = System.String.Format("{0,3} {1,22} {2,22} {3,22}", scoreLabel.text, Scores[i].username, Scores[i].score, Scores[i].timeLasted);
            scoreLabel.text = editedText;
        }
    }

    private async Task SetScoreRecords(string connectionString, string username)
    {
        await Task.Run(() =>
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
        });
    }

    private async Task<List<Score>> GetScoreRecords(string connectionString)
    {
        List<Score> records = new List<Score>();
        await Task.Run(() =>
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Username, Score, TimeLasted FROM Scores ORDER BY Score DESC LIMIT 5";
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
        });
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
