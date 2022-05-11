using System.Collections;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    RocketAudioManager rocketAudioManager;
    PlayerMovement playerMovement;
    SessionManager sessionManager;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    bool isTransitioning = false;

    private void Awake()
    {
        rocketAudioManager = GetComponent<RocketAudioManager>();
        playerMovement = GetComponent<PlayerMovement>();
        sessionManager = FindObjectOfType<SessionManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly hit");
                break;
            case "Finish":
                if (!isTransitioning)
                {
                    StartCoroutine(SuccessSequence());
                }
                break;
            default:
                if (!isTransitioning)
                {
                    StartCoroutine(CrashSequence());
                }
                break;
        }
    }

    private void PrepareTransition()
    {
        isTransitioning = true;
        playerMovement.enabled = false;
        rocketAudioManager.StopRocketSoundEffect();
    }

    IEnumerator CrashSequence()
    {
        PrepareTransition();
        rocketAudioManager.PlayDeathExplosionSoundEffect();
        crashParticles.Play();
        yield return new WaitForSecondsRealtime(rocketAudioManager.GetDeathExplosionAudioClipLength());
        {
            sessionManager.RealoadLevel();
        }
    }

    IEnumerator SuccessSequence()
    {
        PrepareTransition();
        rocketAudioManager.PlayLevelSuccessSoundEffect();
        successParticles.Play();
        yield return new WaitForSecondsRealtime(rocketAudioManager.GetLevelSuccessAudioClipLength());
        {
            sessionManager.LoadNextLevel();
        }
    }
}
