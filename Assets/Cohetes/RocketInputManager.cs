using UnityEngine;
using UnityEngine.InputSystem;

public class RocketInputManager : MonoBehaviour
{
    [SerializeField] private Rocket[] rockets;

    public void OnLaunch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            foreach (Rocket rocket in rockets)
            {
                rocket.StartThrust();
            }
        }

        if (context.canceled)
        {
            foreach (Rocket rocket in rockets)
            {
                rocket.StopThrust();
            }
        }
    }
}