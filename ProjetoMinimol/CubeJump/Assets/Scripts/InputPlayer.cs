using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Input InputMap;
    public Vector3 JumpForce;

    public int maxChildren = 20;
    public int minRotation = 50;
    public int maxRotation = 100;
    public Vector2 minMaxX;
    public Vector2 minMaxZ;
    private InputAction JumpAction;
    public GameObject CubeObject;
    private List<CubeChildren> ChildrenObjects;

    public static event Action OnPointScore;


    private void Awake()
    {
        InputMap = new Input();
        ChildrenObjects = new List<CubeChildren>();
        ChildrenObjects.Add(gameObject.transform.GetChild(0).gameObject.GetComponent<CubeChildren>());
    }
    private void OnEnable()
    {
        JumpAction = InputMap.Main.Jump;
        JumpAction.Enable();
        JumpAction.performed += Jump;
        CubeChildren.OnTouchDown += OnTouchDown;
    }

    
    private void OnDisable()
    {
        JumpAction.Disable();
        CubeChildren.OnTouchDown -= OnTouchDown;

    }

    private void OnTouchDown(CubeChildren cubeDown)
    {
        if(ChildrenObjects.Count < maxChildren && cubeDown.didScorePointThisJump == false)
        {
            cubeDown.didScorePointThisJump = true;
            Vector3 instantiatePos = new Vector3(UnityEngine.Random.Range(minMaxX.x, minMaxX.y), 0.5f, UnityEngine.Random.Range(minMaxZ.x, minMaxZ.y));
            GameObject cube = GameObject.Instantiate(CubeObject, instantiatePos, this.transform.rotation, this.transform);
            ChildrenObjects.Add(cube.GetComponent<CubeChildren>());
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (GameManager.GameState == GameState.STATE_PAUSE)
            return;
        foreach (var cubeChild in ChildrenObjects)
        {
            if (cubeChild.onFloor)
            {
                cubeChild.GetComponent<Rigidbody>().AddForce(JumpForce);
                cubeChild.EulerAngleVelocity = new Vector3(UnityEngine.Random.Range(minRotation - 1, maxRotation), 0, 0);
                cubeChild.onFloor = false;
                cubeChild.didScorePointThisJump = false;
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var cubeChild in ChildrenObjects)
        {
            if (!cubeChild.onFloor)
            {
                Quaternion deltaRotation = Quaternion.Euler(cubeChild.EulerAngleVelocity * Time.fixedDeltaTime);
                Rigidbody rb = cubeChild.GetComponent<Rigidbody>();
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
        }
    }

    
}
