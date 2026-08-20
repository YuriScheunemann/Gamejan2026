using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    public Vector2 velocityMP;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main != null ? Camera.main.transform : null;
        if (cameraTransform != null)
            lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null)
        {
            if (Camera.main == null) return;
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;
            return;
        }

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * velocityMP.x, deltaMovement.y * velocityMP.y, 0f);
        lastCameraPosition = cameraTransform.position;
    }
}

