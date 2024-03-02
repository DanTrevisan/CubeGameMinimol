using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
