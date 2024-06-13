using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public Button restartButton;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.OnPlayerDeath += ShowRestartButton;
            playerMovement.OnPlayerFinish += ShowRestartButton;
        }
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (playerMovement != null)
        {
            playerMovement.OnPlayerDeath -= ShowRestartButton;
            playerMovement.OnPlayerFinish -= ShowRestartButton;
        }
    }

    private void ShowRestartButton()
    {
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
