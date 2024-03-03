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
    void Start()
    {
        textScore = this.GetComponent<TextMeshProUGUI>();
        pointChannel.OnEventRaised += OnPointChange;
        OnPointChange(0);
    }

    private void OnPointChange(int score)
    {
        //This game has only a few strings of text, but the texts should be on a localization sheet in a full game.
        textScore.text = "Score: " + score;
    }
}
