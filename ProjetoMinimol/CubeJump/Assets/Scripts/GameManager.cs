using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    STATE_PAUSE,
    STATE_PLAYING,
    STATE_VICTORY,
    STATE_DEFEAT
}
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputPlayer.OnPointScore += OnPointScore;

    }

    private void OnPointScore()
    {
       // throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
