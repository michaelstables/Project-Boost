using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Tuning")]
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float torquePower = 10f;

    [Header("Seralized For Testing")]
    [SerializeField]Vector3 playerMovement;

    Rigidbody myRigidBody;
    PlayerInput myPlayerInput;
    RocketAudioManager rocketAudioManager;

    InputAction rotateShipAction;
    InputAction thrustAction;

    private void Awake()
    {
        GetReferances();
    }

    private void Update()
    {
        ReadPlayerInput();
        HandleRocketThrustSoundEffect();
    }

    private void FixedUpdate()
    {
        SteerShip();
        ApplyThrust();
    }

    void GetReferances()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myPlayerInput = GetComponent<PlayerInput>();
        rocketAudioManager = GetComponent<RocketAudioManager>();
        rotateShipAction = myPlayerInput.actions["Rotate Ship"];
        thrustAction = myPlayerInput.actions["Thrust"];
    }

    private void ReadPlayerInput()
    {
        playerMovement.y = thrustAction.ReadValue<float>();
        playerMovement.z = rotateShipAction.ReadValue<Vector2>().x;
    }

    private void HandleRocketThrustSoundEffect()
    {
        if (thrustAction.ReadValue<float>() == 1)
        {
            rocketAudioManager.PlayRocketSoundEffect();
        }
        else
        {
            rocketAudioManager.StopRocketSoundEffect();
        }
    }

    private void SteerShip()
    {     
        myRigidBody.AddRelativeTorque(new Vector3(0, 0, -playerMovement.z) * torquePower);
    }

    private void ApplyThrust()
    {
        myRigidBody.AddRelativeForce(new Vector3(0, playerMovement.y, 0) * mainThrust);
    }
}
