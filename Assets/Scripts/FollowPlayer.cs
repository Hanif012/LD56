using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset;   // Offset between the camera and the player

    public Vector3 rotationOffset; // Offset between the camera's rotation and the player's rotation

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the offset if not set in the inspector
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) FollowPlayerMethod();
    }

    private void FollowPlayerMethod()
    {
        transform.position = player.position + offset;

        // Update the camera's rotation to follow the player's rotation
        transform.rotation = player.rotation * Quaternion.Euler(rotationOffset);
    }
}
