using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        Rigidbody rb = GetComponent<Rigidbody>();

        // Freeze rotation along all axes
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
