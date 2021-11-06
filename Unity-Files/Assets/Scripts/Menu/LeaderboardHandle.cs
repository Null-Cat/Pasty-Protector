using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Net;
public class LeaderboardHandle : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {

    }

    private void SetScoreRecords(string connectionString, string username, int score, int timeLasted)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"INSERT INTO Scores VALUES ('{username}', '{score}', '{timeLasted}', '{GetIPAddress()}')";
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

    private void GetScoreRecords(string connectionString)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Debug.Log("connection established");
                string sql = "SELECT * FROM Scores ORDER BY Score DESC LIMIT 5";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }
                    }
                }
            }
        }
        catch (MySqlException e)
        {
            Debug.Log(e.ToString());
        }
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
