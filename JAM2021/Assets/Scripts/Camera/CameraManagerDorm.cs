using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerDorm : MonoBehaviour
{
    public Transform Player;

    public Vector2 clampZ;

    void Awake()
    {
        Vector3 position = Vector3.zero;
        position.x = transform.position.x;
        position.y = transform.position.y;
        position.z = Mathf.Clamp(Player.position.z, clampZ.x, clampZ.y);

        transform.position = position;
        
    }


    void Update()
    {
        Vector3 position = Vector3.zero;
        position.x = transform.position.x;
        position.y = transform.position.y;
        position.z = Mathf.Clamp(Player.position.z - 10, clampZ.x, clampZ.y);

        transform.position = position;
    }
}
