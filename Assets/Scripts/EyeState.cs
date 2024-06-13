using UnityEngine;

public class EyeState : MonoBehaviour, IEyeState
{
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void UpdateState()
    {
    }

    public void OnEyeOpen()
    {
        Debug.Log("Eye is open. Player must not move.");
    }

    public void OnEyeClosed()
    {
        Debug.Log("Eye is closed. Player can move freely.");
    }
}