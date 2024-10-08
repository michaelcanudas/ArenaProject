using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLook : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] PlayerLookConfig config;

    float multiplyer = 0.01f;
    float horizontalRotation = 0f;
    float verticalRotation = 0f;

    Transform cameraHolder = null;
    GameObject cameraObject = null;
    Camera cam = null;
    PlayerLookInput cameraInput = null;

    public void SetCamera(PlayerLookInput cameraInput)
    {
        this.cameraInput = cameraInput;
    }

    void Awake()
    {
        CreateCameraHolder();
        CreateCameraObject();
        CreateCamera();
        CreateAudioListener();
    }

    void CreateCameraHolder()
    {
        cameraHolder = transform.Find("Camera Holder");
        if (cameraHolder == null)
            cameraHolder = new GameObject("Camera Holder").transform;
    }

    void CreateCameraObject()
    {
        if (cameraHolder.Find("Camera") == null)
            cameraObject = new GameObject("Camera");
        else
            cameraObject = cameraHolder.Find("Camera").gameObject;
    }

    void CreateCamera()
    {
        cam = cameraObject.GetComponent<Camera>();
        if (cam == null)
            cam = cameraObject.AddComponent<Camera>();
    }

    void CreateAudioListener()
    {
        if (cameraObject.GetComponent<AudioListener>() == null)
            cameraObject.AddComponent<AudioListener>();
    }

    void Start()
    {
        SetupCameraHolder();
        SetupCameraObject();
        SetupCamera();

        LockCursor();
    }

    void SetupCameraHolder()
    {
        cameraHolder.parent = transform;
        cameraHolder.localPosition = config.offset;
        cameraHolder.localEulerAngles = Vector3.zero;

        cameraHolder.GetComponent<InterpolatedTransform>().ResetInterpolation();
    }

    void SetupCameraObject()
    {
        cameraObject.transform.parent = cameraHolder;
        cameraObject.transform.localPosition = Vector3.zero;
        cameraObject.transform.localEulerAngles = Vector3.zero;
    }

    void SetupCamera()
    {
        cam.fieldOfView = config.normalFOV;
        cam.nearClipPlane = 0.01f;
        cam.GetUniversalAdditionalCameraData().renderPostProcessing = true;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        LookInput();
    }

    void FixedUpdate()
    {
        Look();
    }

    void LookInput()
    {
        horizontalRotation += cameraInput.HorizontalInput * multiplyer * config.xSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation - cameraInput.VerticalInput * multiplyer * config.ySensitivity, config.minClamp, config.maxClamp);
    }

    void Look()
    {
        transform.localEulerAngles = Vector3.up * horizontalRotation;
        cameraHolder.localEulerAngles = Vector3.right * verticalRotation;
    }
}

public class PlayerLookInput
{
    public float HorizontalInput;
    public float VerticalInput;
}