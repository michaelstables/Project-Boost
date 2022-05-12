using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SessionManager : MonoBehaviour
{
    [Header("Developer Testing")]
    [SerializeField] bool isDeveloper = false;
    bool collisionDisabled = false;

    PlayerInput developerInputSystem;
    InputAction disableCollisions;
    InputAction loadNextLevel;

    int currentSceneIndex;
    int numberOfSessionManagers;

    private void Awake()
    {
        developerInputSystem = GetComponent<PlayerInput>();
        PersistSessionManager();
    }

    private void Start()
    {
        disableCollisions = developerInputSystem.actions["Disable Collisions"];
        loadNextLevel = developerInputSystem.actions["Load Next Level"];

        loadNextLevel.performed += SkipLevel;
        disableCollisions.performed += DisableCollisions;
    }

    public void RealoadLevel()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void PersistSessionManager()
    {
        numberOfSessionManagers = FindObjectsOfType<SessionManager>().Length;
        if (numberOfSessionManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void SkipLevel(InputAction.CallbackContext context)
    {
        if (isDeveloper)
        {
            LoadNextLevel();
        }
    }

    private void DisableCollisions(InputAction.CallbackContext context)
    {
        if (isDeveloper)
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    public bool GetCollisionDisabled()
    {
        return collisionDisabled;
    }
}


