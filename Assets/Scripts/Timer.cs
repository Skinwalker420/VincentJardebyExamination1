using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float currentTime;
    void Update()
    {
        currentTime += Time.deltaTime;
        timerText.text = currentTime.ToString("00.00") + "s";
    }
}
