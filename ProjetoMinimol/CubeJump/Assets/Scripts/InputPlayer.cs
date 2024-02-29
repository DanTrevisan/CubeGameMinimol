using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Input InputMap;
    public Vector3 JumpForce;
    private Vector3 EulerAngleVelocity;

    public int maxChildren = 20;
    public int minRotation = 50;
    public int maxRotation = 100;
    private InputAction JumpAction;

    private List<CubeChildren> ChildrenObjects;

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
    }

    private void OnDisable()
    {
        JumpAction.Disable();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * Time.fixedDeltaTime);
        foreach (var cubeChild in ChildrenObjects)
        {
            if (!cubeChild.onFloor)
            {
                Rigidbody rb = cubeChild.GetComponent<Rigidbody>();
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        foreach (var cubeChild in ChildrenObjects)
        {
            if (cubeChild.onFloor)
            {
                cubeChild.GetComponent<Rigidbody>().AddForce(JumpForce);
                EulerAngleVelocity = new Vector3(Random.Range(minRotation - 1, maxRotation), 0, 0);
                cubeChild.onFloor = false;
            }
        }
        
    }
}
