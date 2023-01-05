using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    float timeout = 5;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy);
    }

    // Update is called once per frame
    void Update()
    {

        if(timeout > 3)
        {
            timeout -= Time.deltaTime;

            return;
        }
        Instantiate(enemy);
    }
}
