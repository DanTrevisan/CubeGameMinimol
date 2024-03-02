using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    STATE_PAUSE,
    STATE_PLAYING,
}
public class GameManager : MonoBehaviour
{

    public static GameState GameState
    {
        get
        {
            return m_GameState;
        }
        set
        {
            m_GameState = value;
        }
    }

    private static GameState m_GameState;
    private int m_currentPoints = 0;

    public int pointsVictory = 20;

    #region Events
    public static event Action OnVictory;
    public static event Action OnDefeat;
    [SerializeField]
    private VoidEventChannelSO defeatChannel;
    [SerializeField]
    private VoidEventChannelSO p1StartChannel;
    [SerializeField]
    private VoidEventChannelSO p2StartChannel;

    #endregion


    void Start()
    {
        InputPlayer.OnPointScore += OnPointScore;
        defeatChannel.OnEventRaised += GameManager_OnDefeat;
        p1StartChannel.OnEventRaised += Start1PGame;
        p2StartChannel.OnEventRaised += Start2PGame;

    }

    private void Start1PGame()
    {
        m_GameState = GameState.STATE_PLAYING;
    }
    private void Start2PGame()
    {
        m_GameState = GameState.STATE_PLAYING;
    }
    private void GameManager_OnDefeat()
    {
        if(GameState == GameState.STATE_PAUSE) { return; }
        m_GameState = GameState.STATE_PAUSE;
        Debug.Log("Defeat!");
        ResetPoints();
    }

    private void OnPointScore()
    {
        m_currentPoints++;
        if (m_currentPoints >= pointsVictory) 
        {
            Debug.Log("Victory!");
            OnVictory?.Invoke();
        }
       // throw new NotImplementedException();
    }

    private void ResetPoints()
    {
        m_currentPoints = 0;
    }

    private void OnDisable()
    {
        defeatChannel.OnEventRaised -= GameManager_OnDefeat;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
