using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target60FPS : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}