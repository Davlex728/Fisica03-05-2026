using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlanetPlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform planet;
    [SerializeField] private float moveSpeed = 6f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 gravityUp = (transform.position - planet.position).normalized;
        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, gravityUp).normalized;

        if (forward.sqrMagnitude < 0.001f)
            forward = Vector3.ProjectOnPlane(transform.up, gravityUp).normalized;

        Vector3 right = Vector3.Cross(gravityUp, forward).normalized;
        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }
}