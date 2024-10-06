using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    [SerializeField] private bool lockYAxis = false;
    [SerializeField] private bool lockXAxis = false;
    [SerializeField] private bool lockZAxis = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        // Make the billboard face the camera
        Vector3 targetPosition = Camera.main.transform.position;

        if (lockXAxis) targetPosition.x = transform.position.x;
        if (lockYAxis) targetPosition.y = transform.position.y;
        if (lockZAxis) targetPosition.z = transform.position.z;

        transform.LookAt(targetPosition);
    }
}
