using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScore : MonoBehaviour
{
    [SerializeField]
    private IntEventChannelSO pointChannel;

    private TextMeshProUGUI textScore;
    // Start is called before the first frame update
    void Start()
    {
        textScore = this.GetComponent<TextMeshProUGUI>();
        pointChannel.OnEventRaised += OnPointChange;
        OnPointChange(0);
    }

    private void OnPointChange(int score)
    {
        textScore.text = "Score: " + score;
    }
}
