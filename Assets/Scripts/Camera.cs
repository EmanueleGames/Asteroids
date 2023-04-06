using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 tempCameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag(GameManager.PLAYER_TAG).transform;
    }

    // Update is called once per frame
    void Update()
    {
        // to avoid errors when Ship gets destroyed
        if (!playerTransform)
            return;

        tempCameraTransform = transform.position;
        tempCameraTransform.x = playerTransform.position.x;
        tempCameraTransform.y = playerTransform.position.y;
        transform.position = tempCameraTransform;
    }
}
