using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShiel : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HazardObjects>()!=null)
        {
            collision.gameObject.GetComponent<HazardObjects>().HitOnBarrier();
            GetComponentInParent<PlayerController>().InactivateShieldPowerUp();
            gameObject.SetActive(false);
        }
    }
}
