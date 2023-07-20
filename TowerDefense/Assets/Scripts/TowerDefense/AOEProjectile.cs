using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AOEProjectile : MonoBehaviour
{
    public int damage;
    public bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(killYourself());
    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.GetComponent<EnemyDoT>() == null)
        {
            other.AddComponent<EnemyDoT>();
            other.GetComponent<EnemyDoT>().projectile = gameObject.GetComponent<AOEProjectile>();
            other.GetComponent<EnemyDoT>().damage = damage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.GetComponent<EnemyDoT>() != null)
        {
            Debug.Log("Destroyed");
            Destroy(other.GetComponent<EnemyDoT>());
        }
    }

    IEnumerator killYourself()
    {
        yield return new WaitForSeconds(2.5f);

        isAlive = false;
        Destroy(gameObject);
    }

}
