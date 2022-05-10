using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Tuning")]
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float torquePower = 10f;

    [Header("Seralized For Testing")]
    [SerializeField]Vector3 playerMovement;

    Rigidbody myrigidBody;
    PlayerInput myPlayerInput;

    InputAction rotateShipAction;
    InputAction thrustAction;

    private void Awake()
    {
        GetReferances();
    }

    private void Update()
    {
        ReadPlayerInput();
    }

    private void FixedUpdate()
    {
        FlyShip();
    }

    void GetReferances()
    {
        myrigidBody = GetComponent<Rigidbody>();
        myPlayerInput = GetComponent<PlayerInput>();
        rotateShipAction = myPlayerInput.actions["Rotate Ship"];
        thrustAction = myPlayerInput.actions["Thrust"];
    }

    private void ReadPlayerInput()
    {
        playerMovement.y = thrustAction.ReadValue<float>();
        playerMovement.z = rotateShipAction.ReadValue<Vector2>().x;
    }

    private void FlyShip()
    {
        myrigidBody.AddRelativeForce(new Vector3(0, playerMovement.y, 0) * mainThrust);
        myrigidBody.AddRelativeTorque(new Vector3(0, 0, -playerMovement.z) * torquePower);
    }
}
