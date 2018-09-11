using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApBullet : Bullet {

    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!collision.gameObject.GetComponent<Barrier>() && !collision.gameObject.GetComponent<PowerUpShiel>())
        {
            if (!collision.gameObject.CompareTag("Hazard"))
            {
                if (collision.gameObject.GetComponent<HazardObjects>() != null)
                {
                    collision.gameObject.GetComponent<HazardObjects>().ApBulletImpact();
                    BulletsPool.BulletsPoolInstance.ReturnGameObjectApBullet(gameObject);
                }
            }
            //BulletsPool.BulletsPoolInstance.ReturnGameObjectApBullet(gameObject);
        }

    }

    protected override void AutoDestroy()
    {
        BulletsPool.BulletsPoolInstance.ReturnGameObjectApBullet(gameObject);
    }
    public override void AddImpulse()
    {
        force = 3f;
        myRigidbody.AddForce(transform.up * force, ForceMode2D.Impulse);
    }
}
