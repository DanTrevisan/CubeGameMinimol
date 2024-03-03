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
    [SerializeField]
    private VoidEventChannelSO resetChannel;
    // Start is called before the first frame update

    public CanvasGroup startUI;
    public CanvasGroup scoreUI;
    public CanvasGroup endUI;
    public GameObject playerTurnUI;

    public TextMeshProUGUI textVictory;

    private void Start()
    {
        defeatChannel.OnEventRaised += CallDefeatScreen;
        victoryChannel.OnEventRaised += CallVictoryScreen;
    }

    private void CallVictoryScreen()
    {
        endUI.alpha = 1;
        endUI.interactable = true;
        endUI.blocksRaycasts = true;

        scoreUI.alpha = 0;
        textVictory.text = "Victory!";
    }

    private void CallDefeatScreen()
    {
        endUI.alpha = 1;
        endUI.interactable = true;
        endUI.blocksRaycasts = true;

        scoreUI.alpha = 0;
        textVictory.text = "Defeat...";

    }

    public void StartP1Game()
    {
        start1pGame.RaiseEvent();
        scoreUI.alpha = 1;
        startUI.alpha = 0;
        startUI.interactable = false;
        startUI.blocksRaycasts = false;

    }

    public void StartP2Game()
    {
        start2pGame.RaiseEvent();
        scoreUI.alpha = 1;
        startUI.alpha = 0;
        startUI.interactable = false;
        startUI.blocksRaycasts = false;
    }

    public void CallResetGame()
    {
        endUI.alpha = 0;
        endUI.interactable = false;
        endUI.blocksRaycasts = false;
        scoreUI.alpha = 0;
        startUI.alpha = 1;
        startUI.interactable = true;
        startUI.blocksRaycasts = true;
        playerTurnUI.SetActive(false);
        resetChannel.RaiseEvent();

    }
}
