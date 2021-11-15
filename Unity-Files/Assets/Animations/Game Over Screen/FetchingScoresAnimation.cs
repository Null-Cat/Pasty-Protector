using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class FetchingScoresAnimation : MonoBehaviour
{
    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
        AnimateText();
    }

    private async void AnimateText()
    {
        while (textComponent != null)
        {
            try
            {
            textComponent.text = "Fetching Scores";
            await Task.Delay(200);
            textComponent.text = "Fetching Scores.";
            await Task.Delay(200);
            textComponent.text = "Fetching Scores..";
            await Task.Delay(200);
            textComponent.text = "Fetching Scores...";
            await Task.Delay(200);
            }
            catch (MissingReferenceException)
            {
                Debug.Log("Missing Reference: Ignoring Due to Async Winddown");
            }
        }
    }
}
