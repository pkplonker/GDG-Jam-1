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

    Rigidbody rb;

    [SerializeField] GameObject thirdPersonCamera;
    [SerializeField] GameObject firstPersonCamera;
    [SerializeField] GameObject topDownCamera;

    [SerializeField] float accelerationSpeed = 1f;
    [SerializeField] float maxSpeed = 1f;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] float gravity = 1f;
    [SerializeField] bool allowBackwards = false;

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
        //Debug.Log(transform.up);

        transform.RotateAround(transform.position, transform.up, movement.y < 0.1f && movement.y > -0.1f ? movement.x * rotateSpeed : movement.y * movement.x * rotateSpeed);

        if (!allowBackwards)
        {
            movement.y = Mathf.Clamp(movement.y, 0, 1);
        }

        Vector3 rotatedMove = transform.rotation * new Vector3(0, 0, movement.y);
        rb.AddForce(rotatedMove.normalized * accelerationSpeed, ForceMode.Force);

        if (rb.velocity.magnitude > maxSpeed) //if horizontal velocity is greater than set speed
        {
            Vector3 limitedVel = rb.velocity.normalized * maxSpeed; //sets velocity ot be max speed
            rb.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
        }

        rb.AddForce(-transform.up * gravity, ForceMode.Force);
    }

    private void OnEnable()
    {
        input.Enable();
        input.PlayerTest.Movement.performed += OnMoveKey;
        input.PlayerTest.Movement.canceled += OnMoveCancel;

        input.PlayerTest.Cameras.performed += OnCameraInput;
    }

    private void OnDisable()
    {
        input.Disable();
        input.PlayerTest.Movement.performed -= OnMoveKey;
        input.PlayerTest.Movement.canceled -= OnMoveCancel;

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
