using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {


    private bool canActivatePowerUp = true;
    private float coolDownTime = 10f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HazardObjects>() != null)
        {
            collision.gameObject.GetComponent<HazardObjects>().HitOnBarrier();
        }
    }

    public void ActivateShield()
    {
        if (canActivatePowerUp && !GetComponentInParent<PlayerController>().CurrentShieldActtive)
        {
            canActivatePowerUp = false;
            gameObject.SetActive(true);
            GetComponentInParent<PlayerController>().CurrentShieldActtive = true;
            Invoke("DeactivateShield", 5f);
        }
    }

    public void DeactivateShield()
    {
        gameObject.SetActive(false);
        GetComponentInParent<PlayerController>().CurrentShieldActtive = false;
        Invoke("CoolDown", coolDownTime);
    }

    public void CoolDown()
    {
        canActivatePowerUp = true;
    }
}
