using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSword : MonoBehaviour
{
    //Making these public so we can watch them change in the designer
    public Transform handle;
    public Animator swingAnimator;

    //The values can be edited in the designer (easy tweaking without recompling code)
    public float readyToAttack = 0;
    public float smallSwingDelay = 2.0f;
    public float bigSwingDelay = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        handle = transform.Find("SwordHandle");
        swingAnimator = handle.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        readyToAttack -= Time.deltaTime;

        if (readyToAttack < 0)
        {
            //Check left mouse button, then check right mouse button (both at same time not allowed)
            if (Input.GetMouseButton(0))
            {
                //Small swing
                //swingAnimator.Play("SmallSwing");
                swingAnimator.SetTrigger("SmallSwing");
                readyToAttack += smallSwingDelay;
            }
            else if (Input.GetMouseButton(1))
            {
                //Big swing - change Trigger to BigSwing
                swingAnimator.SetTrigger("SmallSwing");
                readyToAttack += bigSwingDelay;
            }
        }
    }
}
