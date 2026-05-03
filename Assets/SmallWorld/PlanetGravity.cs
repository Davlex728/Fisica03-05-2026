using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    [SerializeField] private float planetMass = 1000f;
    [SerializeField] private float gravityConstant = 10f;
    [SerializeField] private float alignSpeed = 10f;

    public void Attract(Rigidbody rb)
    {
        Vector3 direction = transform.position - rb.position;
        float distance = direction.magnitude;

        if (distance <= 0.001f)
            return;

        Vector3 gravityDirection = direction.normalized;

        float playerMass = rb.mass;
        float forceMagnitude = gravityConstant * (planetMass * playerMass) / (distance * distance);

        rb.AddForce(gravityDirection * forceMagnitude, ForceMode.Force);

        Quaternion targetRotation =
            Quaternion.FromToRotation(rb.transform.up, -gravityDirection) * rb.rotation;

        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, alignSpeed * Time.fixedDeltaTime));
    }
}