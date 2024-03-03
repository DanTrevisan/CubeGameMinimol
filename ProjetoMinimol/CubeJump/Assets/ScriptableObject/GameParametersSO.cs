using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Scriptable Objects/Game Parameters",
fileName = "GameParameters")]
public class GameParametersSO : ScriptableObject
{
    public int m_pointsBeforeMaxCubesToWin = 10;
    public Vector3 JumpForce;
    public int maxChildren = 20;
    public int minRotation = 50;
    public int maxRotation = 100;
    public Vector2 minMaxX;
    public Vector2 minMaxZ;
    public int nudgeForce;
}

