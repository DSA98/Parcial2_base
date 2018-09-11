using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFromSpawner : MonoBehaviour {

    [SerializeField]
    GameObject barrier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>()!=null && !collision.gameObject.GetComponent<PlayerController>().CurrentShieldActtive)
            {
                collision.gameObject.GetComponentInChildren<PlayerController>().ActivateShieldPowerUp();
                Destroy(gameObject);
            }
        }
    }
}
