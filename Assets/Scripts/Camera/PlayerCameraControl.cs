using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    private void Update()
    {
        Vector3 transformPosition = transform.position;
        Vector3 position1 = target.position;
        Vector3 newPosition = Vector3.Lerp(transformPosition, position1, speed * Time.deltaTime);
        transformPosition.x = newPosition.x;
        transformPosition.y = newPosition.y;
        transform.position = transformPosition;
    }
}
