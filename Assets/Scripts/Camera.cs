using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    private InputSystem_Actions _actions;
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 3f;       // Distancia de la cámara al jugador
    [SerializeField] private float height = 2f;         // Altura de la cámara
    [SerializeField] private float rotationSpeed = 1f;  // Sensibilidad del ratón
    private float minY = -30f;
    private float maxY = 60f;
    private float currentX = 0f;
    private float currentY = 0f;
    private Vector2 lookInput;
    private void Awake()
    {
        _actions = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        _actions.Camera.Enable();
        _actions.Camera.Look.performed += OnLookPerformed;
        _actions.Camera.Look.canceled += OnLookCanceled;
    }
    private void OnDisable()
    {
        _actions.Camera.Disable();
        _actions.Camera.Look.performed -= OnLookPerformed;
        _actions.Camera.Look.canceled -= OnLookCanceled;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        lookInput = Vector2.zero;
    }
    void LateUpdate()
    {
        if (player == null) return;

        // Aplica la rotación con el input del ratón
        currentX += lookInput.x * rotationSpeed;
        currentY -= lookInput.y * rotationSpeed;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        Vector3 direction = new Vector3(0, height, -distance);
        Vector3 desiredPosition = player.position + rotation * direction;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10f);
        transform.LookAt(player.position + Vector3.up * height / 2);
    }
}
