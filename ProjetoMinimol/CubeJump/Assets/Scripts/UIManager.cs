using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        defeatChannel.OnEventRaised += CallDefeatScreen;
        victoryChannel.OnEventRaised += CallVictoryScreen;
    }

    private void CallVictoryScreen()
    {
        throw new NotImplementedException();
    }

    private void CallDefeatScreen()
    {
        endUI.SetActive(true);
    }

    public void StartP1Game()
    {
        start1pGame.RaiseEvent();
        startUI.SetActive(false);
    }

    public void StartP2Game()
    {
        start2pGame.RaiseEvent();
        startUI.SetActive(false);

    }
}
