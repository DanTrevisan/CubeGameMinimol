using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeChildren : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]public bool onFloor = false;
    public List<CubeCollider> cubeColliders;
    public CubeCollider lastCubeOnFloor;

    public static event Action OnTouchDown;

    private void OnCollisionEnter(Collision collision)
    {
        if (onFloor)
            return;
        if (collision.collider.gameObject.tag == "Floor")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            onFloor = true;
            foreach (var cube in cubeColliders)
            {
                if (cube.IsThisLastFace == true && lastCubeOnFloor != cube)
                {
                    lastCubeOnFloor = cube;
                    Debug.Log("PONTO!");
                    OnTouchDown?.Invoke();
                }
            }
        }
    }
    void Start()
    {
        lastCubeOnFloor = cubeColliders[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("angle valid? " + transform.localRotation.eulerAngles.x);

    }

}
