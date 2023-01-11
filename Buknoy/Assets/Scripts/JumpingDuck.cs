using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingDuck : MonoBehaviour
{
    public float upspeed;
    public float downspeed;
    public Transform up;
    public Transform down;
    bool land;
    private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= up.position.y)
        {
            land = true;
        }
        if (transform.position.y <= down.position.y)
        {
            land = false;
        }
        if (land)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                down.position,
                downspeed * Time.deltaTime
            );
            anim.SetBool("fall", true);
            anim.SetBool("jump", false);
        }
        else
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                up.position,
                upspeed * Time.deltaTime
            );
            anim.SetBool("jump", true);
            anim.SetBool("fall", false);
        }
    }
}
