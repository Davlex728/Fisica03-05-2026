using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    [SerializeField] private float thrust = 300000f;

    private Rigidbody rb;
    private bool isThrusting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void StartThrust()
    {
        isThrusting = true;
    }

    public void StopThrust()
    {
        isThrusting = false;
    }

    private void FixedUpdate()
    {
        if (isThrusting)
        {
            rb.AddForce(Vector3.up * thrust, ForceMode.Force);
        }
    }
}