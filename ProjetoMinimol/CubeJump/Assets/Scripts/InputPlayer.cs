using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This class is mainly respinsible for detecting the inputs and moving the blocks according to inputs.
public class InputPlayer : MonoBehaviour
{
    
    public GameObject CubeObject;
    private List<CubeChildren> ChildrenObjects;
    public GameParametersSO gameParameters;

    #region Input
    public Input InputMap;
    private InputAction JumpAction;
    private InputAction UpAction;
    private InputAction DownAction;
    private InputAction LeftAction;
    private InputAction RightAction;
    #endregion

    #region events
    public static event Action OnPointScore;
    [SerializeField]
    private VoidEventChannelSO resetChannel;
    #endregion

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

        UpAction = InputMap.Main.UpNudge;
        UpAction.Enable();
        UpAction.performed += UpNudge;

        DownAction = InputMap.Main.DownNudge;
        DownAction.Enable();
        DownAction.performed += DownNudge;

        LeftAction = InputMap.Main.LeftNudge;
        LeftAction.Enable();
        LeftAction.performed += LeftNudge;

        RightAction = InputMap.Main.RightNudge;
        RightAction.Enable();
        RightAction.performed += RightNudge;

        CubeChildren.OnTouchDown += OnTouchDown;
        resetChannel.OnEventRaised += ResetCubes;
    }

    private void RightNudge(InputAction.CallbackContext context)
    {
        if (GameManager.GameState == GameState.STATE_PAUSE)
            return;
        foreach (var cubeChild in ChildrenObjects)
        {
            if (!cubeChild.onFloor)
            {
                cubeChild.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -gameParameters.nudgeForce));

            }
        }
    }

    private void LeftNudge(InputAction.CallbackContext context)
    {
        if (GameManager.GameState == GameState.STATE_PAUSE)
            return;
        foreach (var cubeChild in ChildrenObjects)
        {
            if (!cubeChild.onFloor)
            {
                cubeChild.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, gameParameters.nudgeForce));

            }
        }
    }

    private void DownNudge(InputAction.CallbackContext context)
    {
        if (GameManager.GameState == GameState.STATE_PAUSE)
            return;
        foreach (var cubeChild in ChildrenObjects)
        {
            if (!cubeChild.onFloor)
            {
                cubeChild.GetComponent<Rigidbody>().AddForce(new Vector3(-gameParameters.nudgeForce, 0, 0));
            }
        }
    }

    private void UpNudge(InputAction.CallbackContext context)
    {
        if (GameManager.GameState == GameState.STATE_PAUSE)
            return;
        foreach (var cubeChild in ChildrenObjects)
        {
            if (!cubeChild.onFloor)
            {
                cubeChild.GetComponent<Rigidbody>().AddForce(new Vector3(gameParameters.nudgeForce, 0,0));
            }
        }
    }

   

    private void OnTouchDown(CubeChildren cubeDown)
    {
        if(ChildrenObjects.Count < gameParameters.maxChildren && cubeDown.didScorePointThisJump == false)
        {
            OnPointScore.Invoke();
            cubeDown.didScorePointThisJump = true;
            Vector3 instantiatePos = new Vector3(UnityEngine.Random.Range(gameParameters.minMaxX.x, gameParameters.minMaxZ.y), 0.5f, 
                UnityEngine.Random.Range(gameParameters.minMaxX.x, gameParameters.minMaxZ.y));
            GameObject cube = GameObject.Instantiate(CubeObject, instantiatePos, this.transform.rotation, this.transform);
            ChildrenObjects.Add(cube.GetComponent<CubeChildren>());
        }
        if(ChildrenObjects.Count >= gameParameters.maxChildren)
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
                cubeChild.GetComponent<Rigidbody>().AddForce(gameParameters.JumpForce);
                cubeChild.EulerAngleVelocity = new Vector3(UnityEngine.Random.Range(gameParameters.minRotation - 1, gameParameters.maxRotation), 0, 0);
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

    private void OnDisable()
    {
        JumpAction.Disable();
        JumpAction.performed -= Jump;
        CubeChildren.OnTouchDown -= OnTouchDown;
        resetChannel.OnEventRaised -= ResetCubes;

    }
}
