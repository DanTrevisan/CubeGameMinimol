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
    private static int m_points;
    private static int CurrentPoints
    {
        get
        {
            return m_points;
        }
        set
        {
            m_points = value;
        }
    }
    private static bool m_isMaxedCubes = false;
    public static bool IsMaxedCubes
    {
        get
        {
            return m_isMaxedCubes;
        }
        set
        {
            m_isMaxedCubes = value;
        }
    }

    private static bool m_isMultiplayerMode = false;
    public static bool isMultiplayerMode
    {
        get
        {
            return m_isMultiplayerMode;
        }
        set
        {
            m_isMultiplayerMode = value;
        }
    }

    #region Events
    [SerializeField]
    private VoidEventChannelSO defeatChannel;
    [SerializeField]
    private VoidEventChannelSO victoryChannel;
    [SerializeField]
    private VoidEventChannelSO p1StartChannel;
    [SerializeField]
    private VoidEventChannelSO p2StartChannel;
    [SerializeField]
    private VoidEventChannelSO resetChannel;
    [SerializeField]
    private IntEventChannelSO pointChannel;

    #endregion

    private int m_currentPoints = 0;
    private int m_pointsAfterMaxCubes = 0;
    private int m_pointsBeforeMaxCubesToWin = 10;

    void Start()
    {
        InputPlayer.OnPointScore += OnPointScore;
        defeatChannel.OnEventRaised += GameManager_OnDefeat;
        p1StartChannel.OnEventRaised += Start1PGame;
        p2StartChannel.OnEventRaised += Start2PGame;
        resetChannel.OnEventRaised += ResetGame;

    }

    private void ResetGame()
    {
        m_GameState = GameState.STATE_PAUSE;
        ResetPoints();
    }

    private void Start1PGame()
    {
        m_GameState = GameState.STATE_PLAYING;
        isMultiplayerMode = false;
    }
    private void Start2PGame()
    {
        m_GameState = GameState.STATE_PLAYING;
        isMultiplayerMode = true;
    }
    private void GameManager_OnDefeat()
    {
        if(GameState == GameState.STATE_PAUSE) { return; }
        m_GameState = GameState.STATE_PAUSE;
        Debug.Log("Defeat!");
        pointChannel.RaiseEvent(m_currentPoints);
        ResetPoints();
    }

    private void OnPointScore()
    {
        if (m_GameState == GameState.STATE_PAUSE)
            return;
        m_currentPoints++;
        pointChannel.RaiseEvent(m_currentPoints);
        if (IsMaxedCubes) 
        {
            m_pointsAfterMaxCubes++;
            if (m_pointsAfterMaxCubes >= m_pointsBeforeMaxCubesToWin)
            {
                Debug.Log("Victory!");
                m_GameState = GameState.STATE_PAUSE;
                victoryChannel.RaiseEvent();
                pointChannel.RaiseEvent(m_currentPoints);

            }

        }
       // throw new NotImplementedException();
    }

    private void ResetPoints()
    {
        m_currentPoints = 0;
        m_pointsAfterMaxCubes = 0;
        pointChannel.RaiseEvent(m_currentPoints);

    }

    private void OnDisable()
    {
        defeatChannel.OnEventRaised -= GameManager_OnDefeat;

    }

    // Update is called once per frame

}
