using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLaser : MonoBehaviour
{
    public float lifeSpan = 0.2f;
    public float timeAlive = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        
        if (timeAlive >= lifeSpan )
        {
            Destroy(gameObject);
        }
    }
}
