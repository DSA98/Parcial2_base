using UnityEngine;

public class KillVolume : MonoBehaviour
{
    [SerializeField]
    private Shelter[] shelters;
    [SerializeField]
    GameObject defender;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HazardObjects>() != null)
        {
            for (int i = 0; i < shelters.Length; i++)
            {
                if (shelters[i] != null)
                {
                    shelters[i].Damage(collision.gameObject.GetComponent<HazardObjects>().DamageShelterMag);
                }
            }
            if(collision.gameObject.GetComponent<HazardObjects>() is Hazard)
            {
                SingletonReferee.RefereeInstance.DefeatCondition();
            }
        }
        Destroy(collision.gameObject);
    }
}