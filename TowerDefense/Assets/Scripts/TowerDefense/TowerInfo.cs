using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo: MonoBehaviour
{
    public int[] costs = new int[4];
    public int level = 0;
    public TowerTurret turretScript;
    public int type = -1;
    public float[] fireRates = new float[4];
    public int[] damages = new int[4];

    // Start is called before the first frame update
    void Start()
    {
        turretScript.GetComponent<TowerTurret>();
        turretScript.fireRate = fireRates[0];
        turretScript.damage = damages[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
