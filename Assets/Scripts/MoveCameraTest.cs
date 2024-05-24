using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraTest : MonoBehaviour
{
    [SerializeField] Transform cameraPosition = null;

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
