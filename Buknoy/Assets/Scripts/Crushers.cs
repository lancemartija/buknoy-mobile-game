using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushers : MonoBehaviour
{
    public float upspeed;
    public float downspeed;
    public Transform up;
    public Transform down;
    bool chop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= up.position.y)
        {
            chop = true;
        }
        if(transform.position.y <= down.position.y)
        {
            chop = false;
        }
        if(chop)
        {
            transform.position = Vector2.MoveTowards(transform.position,down.position,downspeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position,up.position,upspeed * Time.deltaTime);
        }
    }
}
