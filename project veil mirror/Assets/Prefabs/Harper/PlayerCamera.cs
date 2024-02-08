using UnityEngine;
using Mirror;

public class PlayerCamera : NetworkBehaviour
{
    public Vector3 offset = new Vector3(0, 0, -10); // Example offset, adjust as needed
    private Transform playerTransform;

    void Start()
    {
        // Get the parent player object's transform
        playerTransform = transform.parent;

        // If this is not the local player (controlled by the client), disable the camera
        if (!isLocalPlayer)
        {
            GetComponent<Camera>().enabled = false;
            return;
        }
    }

    void LateUpdate()
    {
        // If this is not the local player, do nothing
        if (!isLocalPlayer)
        {
            return;
        }

        // Follow the player's position with offset
        Vector3 desiredPosition = playerTransform.position + offset;
        transform.position = desiredPosition;

        // Ensure the camera is always looking straight ahead
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
