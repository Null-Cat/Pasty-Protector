using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UsernameValidateSubmit : MonoBehaviour
{
    private void Start()
    {
        GetComponent<InputField>().onValidateInput += delegate (string s, int i, char c) { return char.IsLetter(c) ? char.ToUpper(c) : '\0'; };
        GetComponent<InputField>().Select();
    }
    public void ValidateInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) && GetComponent<InputField>().text.Length == 3)
        {
            GameObject.Find("GameOverPanel").GetComponent<LeaderboardHandle>().SubmitScores();
        }
    }
}
