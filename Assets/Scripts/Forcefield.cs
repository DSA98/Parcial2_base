using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour {

    private bool canActivatePowerUp = true;
    private float coolDownTime = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HazardObjects>() != null)
        {
            collision.gameObject.GetComponent<HazardObjects>().HitOnBarrier();
        }
    }

    public void ActivateShield()
    {
        if (canActivatePowerUp)
        {
            canActivatePowerUp = false;
            gameObject.SetActive(true); 
            Invoke("DeactivateForceField",0.2f);
        }
    }

    public void DeactivateForceField()
    {
        gameObject.SetActive(false);
        Invoke("CoolDown", coolDownTime);
    }

    public void CoolDown()
    {
        canActivatePowerUp = true;
    }
}
