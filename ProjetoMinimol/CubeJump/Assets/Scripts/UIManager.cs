using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private VoidEventChannelSO start1pGame;
    [SerializeField]
    private VoidEventChannelSO start2pGame;
    [SerializeField]
    private VoidEventChannelSO defeatChannel;
    [SerializeField]
    private VoidEventChannelSO victoryChannel;
    // Start is called before the first frame update

    public GameObject startUI;
    public GameObject scoreUI;
    public GameObject endUI;
    public GameObject playerTurnUI;

    public TextMeshProUGUI textVictory;

    private void Start()
    {
        defeatChannel.OnEventRaised += CallDefeatScreen;
        victoryChannel.OnEventRaised += CallVictoryScreen;
    }

    private void CallVictoryScreen()
    {
        endUI.SetActive(true);
        scoreUI.SetActive(false);
        textVictory.text = "Victory!";
    }

    private void CallDefeatScreen()
    {
        endUI.SetActive(true);
        scoreUI.SetActive(false);
        textVictory.text = "Defeat...";

    }

    public void StartP1Game()
    {
        start1pGame.RaiseEvent();
        scoreUI.SetActive(true);
        startUI.SetActive(false);
    }

    public void StartP2Game()
    {
        start2pGame.RaiseEvent();
        scoreUI.SetActive(true);
        startUI.SetActive(false);

    }
}
