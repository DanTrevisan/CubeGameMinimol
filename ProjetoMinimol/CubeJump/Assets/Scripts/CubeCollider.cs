using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollider : MonoBehaviour
{
    public bool IsThisLastFace = false;

    private void OnTriggerEnter(Collider other)
    {
        if (IsThisLastFace)
            return;
        if (other.CompareTag("Floor"))
        {
            IsThisLastFace = true;
        }
    }
}
