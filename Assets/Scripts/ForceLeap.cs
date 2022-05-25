using UnityEngine;
using UnityEngine.InputSystem;

public class ForceLeap : MonoBehaviour
{
    private CharacterController character;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    public void OnForceLeap(InputValue value)
    {
        //if (value.isPressed)
            
    }
}
