using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : HazardObjects
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            if (collision.gameObject.GetComponent<Bullet>() is NormalBullet)
            {
                resistance -= 1;

                if (resistance == 0)
                {
                    OnHazardDestroyed();
                }
                /*if (SingletonReferee.RefereeInstance != null)
                {
                    SingletonReferee.RefereeInstance.AddScore();
                }*/
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                if (!collision.gameObject.GetComponent<PlayerController>().CurrentShieldActtive)
                {
                    collision.gameObject.GetComponent<PlayerController>().ReduceHealth(damageToPlayer);
                    OnHazardDestroyed();
                }
            }
        }
    }

    protected override void OnHazardDestroyed()
    {
        HazardsPool.HazardPoolInstance.ReturnGameObjectHazard(gameObject);
    }
}
