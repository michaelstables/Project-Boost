using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Tuning")]
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float torquePower = 10f;

    [Header("Effects")]
    [SerializeField] ParticleSystem thrustParticles;

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
        HandleRocketThrustEffects();
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

    private void SteerShip()
    {
        myRigidBody.AddRelativeTorque(new Vector3(0, 0, -playerMovement.z) * torquePower);
    }

    private void ApplyThrust()
    {
        myRigidBody.AddRelativeForce(new Vector3(0, playerMovement.y, 0) * mainThrust);
    }

    private void HandleRocketThrustEffects()
    {
        if (thrustAction.ReadValue<float>() == 1)
        {
            StartRocketThrustEffects();
        }
        else
        {
            StopRocketThrustEffects();
        }
    }

    private void StartRocketThrustEffects()
    {
        rocketAudioManager.PlayRocketSoundEffect();
        thrustParticles.Play();
    }

    private void StopRocketThrustEffects()
    {
        rocketAudioManager.StopRocketSoundEffect();
        thrustParticles.Stop();
    }
}
