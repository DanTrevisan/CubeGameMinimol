using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputPlayer : MonoBehaviour
{
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
    [SerializeField]
    private VoidEventChannelSO resetChannel;

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
        resetChannel.OnEventRaised += ResetCubes;
    }


    private void OnDisable()
    {
        JumpAction.Disable();
        JumpAction.performed -= Jump;
        CubeChildren.OnTouchDown -= OnTouchDown;
        resetChannel.OnEventRaised -= ResetCubes;

    }

    private void OnTouchDown(CubeChildren cubeDown)
    {
        if(ChildrenObjects.Count < maxChildren && cubeDown.didScorePointThisJump == false)
        {
            OnPointScore.Invoke();
            cubeDown.didScorePointThisJump = true;
            Vector3 instantiatePos = new Vector3(UnityEngine.Random.Range(minMaxX.x, minMaxX.y), 0.5f, UnityEngine.Random.Range(minMaxZ.x, minMaxZ.y));
            GameObject cube = GameObject.Instantiate(CubeObject, instantiatePos, this.transform.rotation, this.transform);
            ChildrenObjects.Add(cube.GetComponent<CubeChildren>());
        }
        if(ChildrenObjects.Count >= maxChildren)
        {
            GameManager.IsMaxedCubes = true;
            cubeDown.didScorePointThisJump = true;
            OnPointScore.Invoke();
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


    private void ResetCubes()
    {
        for (int i =0; i<ChildrenObjects.Count;i++)
        {
            Destroy(ChildrenObjects[i].gameObject);
        }
        ChildrenObjects = new List<CubeChildren>();
        GameObject cube = GameObject.Instantiate(CubeObject, new Vector3(0, 1, 0), this.transform.rotation, this.transform);
        ChildrenObjects.Add(cube.GetComponent<CubeChildren>());
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
