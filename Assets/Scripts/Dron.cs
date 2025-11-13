using UnityEngine;
using UnityEngine.InputSystem;

public class Dron : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions _actions;
    private Rigidbody _rb;
    private Vector3 _direction;
    private float _movementSensibility = 20;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float velocity;
    private void Awake()
    {
        _actions = new InputSystem_Actions();
        _actions.Player.SetCallbacks(this);
        _rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _actions.Enable();
    }
    private void OnDisable()
    {
        _actions.Disable();
    }
    private void FixedUpdate()
    {
        // Calcular dirección relativa a la cámara
        Vector3 camForward = cameraPivot.forward;
        Vector3 camRight = cameraPivot.right;


        // Dirección final del movimiento
        Vector3 moveDir = (camForward * _direction.z + camRight * _direction.x).normalized;
        _rb.AddForce(moveDir * _movementSensibility * velocity, ForceMode.Acceleration);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }
}
