using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletsPool.BulletsPoolInstance.ReturnGameObjectNormalBullet(gameObject);
    }

    protected override void AutoDestroy()
    {
        BulletsPool.BulletsPoolInstance.ReturnGameObjectNormalBullet(gameObject);
    }

    public override void AddImpulse()
    {
        force = 10f;
        myRigidbody.AddForce(transform.up * force, ForceMode2D.Impulse);
    }
}
