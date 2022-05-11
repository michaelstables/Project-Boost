using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly hit");
                break;
            case "Finish":
                FindObjectOfType<SessionManager>().LoadNextLevel();
                break;
            default:
                FindObjectOfType<SessionManager>().ReloadScene();
                break;
        }
    }
}
