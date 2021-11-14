using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
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
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            FlashRed();
        }
    }

    public void buttonValidateInput()
    {
        if (GetComponent<InputField>().text.Length == 3)
        {
            GameObject.Find("GameOverPanel").GetComponent<LeaderboardHandle>().SubmitScores();
        }
        else
        {
            FlashRed();
        }
    }

    public async void FlashRed()
    {
        try
        {
            if (GetComponent<InputField>().text != "")
            {
                Color defaultColor = GetComponent<InputField>().textComponent.color;
                GetComponent<InputField>().textComponent.color = Color.red;
                await Task.Delay(100);
                GetComponent<InputField>().textComponent.color = defaultColor;
                await Task.Delay(100);
                GetComponent<InputField>().textComponent.color = Color.red;
                await Task.Delay(100);
                GetComponent<InputField>().textComponent.color = defaultColor;
            }
            else
            {
                Color defaultColor = GetComponent<InputField>().placeholder.color;
                GetComponent<InputField>().placeholder.color = Color.red;
                await Task.Delay(100);
                GetComponent<InputField>().placeholder.color = defaultColor;
                await Task.Delay(100);
                GetComponent<InputField>().placeholder.color = Color.red;
                await Task.Delay(100);
                GetComponent<InputField>().placeholder.color = defaultColor;
            }
        }
        catch (MissingReferenceException)
        {
            Debug.Log("Missing Reference: Ignoring Due to Async Winddown");
        }
    }
}
