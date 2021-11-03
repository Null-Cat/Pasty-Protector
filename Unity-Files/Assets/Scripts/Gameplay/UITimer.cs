using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    private float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        this.GetComponent<Text>().text = Mathf.FloorToInt(time / 60).ToString("00") + ":" + Mathf.FloorToInt(time % 60).ToString("00");
    }
}
