using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoT : MonoBehaviour
{
    public float damage = 0;
    public AOEProjectile projectile;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(takeDamage());
    }

    // Update is called once per frame
    void Update()
    {
       if (!projectile.isAlive)
        {
            Destroy(gameObject.GetComponent<EnemyDoT>());
        }
    }

    IEnumerator takeDamage()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("damage");
        for (int i = 0; i < 3; i++)
        {
            EnemyMovementWaypoint enemyScript = GetComponent<EnemyMovementWaypoint>();
            enemyScript.health -= (int)damage;

            yield return new WaitForSeconds(0.8f);
        }

    }
}
