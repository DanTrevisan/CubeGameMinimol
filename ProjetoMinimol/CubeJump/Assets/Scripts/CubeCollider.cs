using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class detects which of the faces are currently on the floor.
public class CubeCollider : MonoBehaviour
{
    public CubeChildren MyCube;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            MyCube.SetLastFace(this);
        }
    }
}
