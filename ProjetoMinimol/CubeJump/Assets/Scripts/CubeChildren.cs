using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used to detect if the cube is on the floor, and to store which of the faces are on the floor.
public class CubeChildren : MonoBehaviour
{
    [HideInInspector]public bool onFloor = false;
    public List<CubeCollider> cubeColliders;
    public CubeCollider lastFaceOnFloor;
    public bool didScorePointThisJump = true;

    public static event Action<CubeChildren> OnTouchDown;
    [HideInInspector] public Vector3 EulerAngleVelocity;

    [SerializeField]
    private VoidEventChannelSO m_defeatChannnel;
    private void OnCollisionEnter(Collision collision)
    {
        if (onFloor)
            return;
        if (collision.collider.gameObject.tag == "Floor")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            onFloor = true;
        }
    }

    public void SetLastFace(CubeCollider col)
    {
        if (lastFaceOnFloor != col)
        {
            lastFaceOnFloor = col;
            OnTouchDown?.Invoke(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.GameState != GameState.STATE_PLAYING)
            return;
        if (other.gameObject.tag == "Defeat")
        {
            m_defeatChannnel.RaiseEvent();
        }
    }
    void Start()
    {
        lastFaceOnFloor = cubeColliders[0];
    }

}
