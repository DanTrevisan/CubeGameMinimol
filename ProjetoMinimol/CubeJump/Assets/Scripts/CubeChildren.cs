using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeChildren : MonoBehaviour
{
    // Start is called before the first frame update
    public bool onFloor = false;

    private float lastXRotation = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Floor")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            onFloor = true;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("angle " + transform.rotation.x);
        if (IsValidAngle())
        {
            if (transform.rotation.x != lastXRotation) ;
        }
    }

    private bool IsValidAngle()
    {
        if (transform.rotation.eulerAngles.x == 0 || transform.rotation.x == 180 || transform.rotation.x == 90 || transform.rotation.x == -90 || transform.rotation.x == -180)
            return true;
        else
            return false;
    }
}
