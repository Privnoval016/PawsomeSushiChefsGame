using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AOEInfo : MonoBehaviour
{
    public int[] costs = new int[4];
    public int level = 0;
    public TowerAOE launcherScript;
    public int type = -1;
    public float[] ranges = new float[4];
    public float[] damages = new float[4];
    public Material[] levelSkins = new Material[4];
    public GameObject cap;

    // Start is called before the first frame update
    void Start()
    {
        launcherScript.range = ranges[0];
        launcherScript.damage = damages[0];
    }

    // Update is called once per frame
    void Update()
    {
        cap.GetComponent<MeshRenderer>().material = levelSkins[level-1];
    }


}
