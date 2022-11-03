using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform Player;

    private void Update()
    {
        transform.position = new Vector3(Player.position.x, Player.position.y + 3, transform.position.z);
    }
}
