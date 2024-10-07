using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset;   // Offset between the camera and the player
    public Vector3 rotationOffset; // Offset between the camera's rotation and the player's rotation

    private bool isFindingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            StartCoroutine(FindPlayer());
        }
        else
        {
            // Initialize the offset if not set in the inspector
            if (offset == Vector3.zero)
            {
                offset = transform.position - player.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            FollowPlayerMethod();
        }
        else if (!isFindingPlayer)
        {
            StartCoroutine(FindPlayer());
        }
    }

    private IEnumerator FindPlayer()
    {
        isFindingPlayer = true;
        while (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;

                // Initialize the offset if not set in the inspector
                if (offset == Vector3.zero)
                {
                    offset = transform.position - player.position;
                }

                Debug.Log("Player found");
            }
            else
            {
                Debug.Log("Player not found, retrying in 1 second...");
                yield return new WaitForSeconds(1f);
            }
        }
        isFindingPlayer = false;
    }

    private void FollowPlayerMethod()
    {
        if (player != null)
        {
            transform.position = player.position + offset;

            // Update the camera's rotation to follow the player's rotation
            transform.rotation = player.rotation * Quaternion.Euler(rotationOffset);
        }
    }
}
