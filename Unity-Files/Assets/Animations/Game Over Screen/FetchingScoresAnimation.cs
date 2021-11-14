using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class FetchingScoresAnimation : MonoBehaviour
{

    void Update()
    {
        AnimateText();
    }

    private async void AnimateText()
    {
        Text text = GetComponent<Text>();
        while (true)
        {
            await Task.Run(async () => {
                text.text = "Fetching Scores.";
                await Task.Delay(1000);
                text.text = "Fetching Scores..";
                await Task.Delay(1000);
                text.text = "Fetching Scores...";
                await Task.Delay(1000);
            });
        }
    }
}
