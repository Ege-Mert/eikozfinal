using UnityEngine;

public class EyeManager : MonoBehaviour
{
    public static EyeManager Instance { get; private set; }

    public float eyeOpenDuration = 2f;
    public float eyeClosedDuration = 2f;
    private bool eyeOpen = false;

    private float timer = 0f;
    private IEyeState state;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        state = GetComponent<IEyeState>();
        SwitchState();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (eyeOpen && timer >= eyeOpenDuration)
        {
            eyeOpen = false;
            timer = 0f;
            state.OnEyeClosed();
        }
        else if (!eyeOpen && timer >= eyeClosedDuration)
        {
            eyeOpen = true;
            timer = 0f;
            state.OnEyeOpen();
        }

        state.UpdateState();
    }

    public bool IsEyeOpen => eyeOpen;

    private void SwitchState()
    {
        if (eyeOpen)
        {
            state.OnEyeOpen();
        }
        else
        {
            state.OnEyeClosed();
        }
    }
}
