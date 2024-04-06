using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class TempInput : MonoBehaviour
{

    PeterTestControls input = null;
    Vector2 movement = Vector2.zero;
    float rotate = 0f;

    Rigidbody rb;

    [SerializeField] GameObject thirdPersonCamera;
    [SerializeField] GameObject firstPersonCamera;
    [SerializeField] GameObject topDownCamera;

    [SerializeField] float accelerationSpeed = 1f;
    [SerializeField] float maxSpeed = 1f;
    [SerializeField] float rotateSpeed = 1f;

    private void Awake()
    {
        input = new PeterTestControls();
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        firstPersonCamera.SetActive(false);
        topDownCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
    }

    void FixedUpdate()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y = rot.y + movement.x * rotateSpeed;
        transform.rotation = Quaternion.Euler(rot);

        Vector3 rotatedMove = transform.rotation * new Vector3(0, 0, movement.y);
        rb.AddForce(rotatedMove.normalized * accelerationSpeed, ForceMode.Force);

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //gets horixontal velocity
        if (flatVel.magnitude > maxSpeed) //if horizontal velocity is greater than set speed
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed; //sets velocity ot be max speed
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void OnEnable()
    {
        input.Enable();
        input.PlayerTest.Movement.performed += OnMoveKey;
        input.PlayerTest.Movement.canceled += OnMoveCancel;

        input.PlayerTest.Rotation.performed += OnRotateKey;
        input.PlayerTest.Rotation.canceled += OnRotateCancel;

        input.PlayerTest.Cameras.performed += OnCameraInput;
    }

    private void OnDisable()
    {
        input.Disable();
        input.PlayerTest.Movement.performed -= OnMoveKey;
        input.PlayerTest.Movement.canceled -= OnMoveCancel;

        input.PlayerTest.Rotation.performed -= OnRotateKey;
        input.PlayerTest.Rotation.canceled -= OnRotateCancel;

        input.PlayerTest.Cameras.performed -= OnCameraInput;
    }

    private void OnMoveKey(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void OnMoveCancel(InputAction.CallbackContext context)
    {
        movement = Vector2.zero;
    }

    private void OnRotateKey(InputAction.CallbackContext context)
    {
        switch (context.control.name)
        {
            case "q":
                rotate = -1;
                break;
            case "e":
                rotate = 1;
                break;
        }
    }

    private void OnRotateCancel(InputAction.CallbackContext context)
    {
        rotate = 0;
    }

    private void OnCameraInput(InputAction.CallbackContext context)
    {
        switch (context.control.name)
        {
            case "1":
                firstPersonCamera.SetActive(true);
                topDownCamera.SetActive(false);
                thirdPersonCamera.SetActive(false);
                break;
            case "2":
                firstPersonCamera.SetActive(false);
                topDownCamera.SetActive(true);
                thirdPersonCamera.SetActive(false);
                break;
            case "3":
                firstPersonCamera.SetActive(false);
                topDownCamera.SetActive(false);
                thirdPersonCamera.SetActive(true);
                break;
        }
    }
}
