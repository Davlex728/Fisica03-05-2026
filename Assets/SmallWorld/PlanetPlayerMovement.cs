using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlanetPlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform planet;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckDistance = 0.6f;

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

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            Vector3 gravityUp = (transform.position - planet.position).normalized;
            rb.AddForce(gravityUp * jumpForce, ForceMode.VelocityChange);
        }
    }

    private bool IsGrounded()
    {
        Vector3 gravityUp = (transform.position - planet.position).normalized;
        // Raycast hacia el centro del planeta para comprobar si estamos tocando el suelo
        return Physics.Raycast(transform.position, -gravityUp, groundCheckDistance);
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