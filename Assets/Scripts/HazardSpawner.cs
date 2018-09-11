using UnityEngine;

public static class SpawnerExtensions
{
    public static Vector3 GetPointInVolume(this Collider2D collider)
    {
        Vector3 result = Vector3.zero;
        result = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x), collider.transform.position.y, 0F);

        return result;
    }
}

[RequireComponent(typeof(Collider2D))]
public class HazardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject powerUpTemplate;

    private Collider2D myCollider;

    private float timer = 0f;

    [SerializeField]
    private float spawnFrequency = 1F;

    public float SpawnFrequency
    {
        get
        {
            return spawnFrequency;
        }

        set
        {
            spawnFrequency = value;
        }
    }

    // Use this for initialization
    private void Start()
    {
        myCollider = GetComponent<Collider2D>();

        //InvokeRepeating("SpawnEnemy", 0.2F, spawnFrequency);
        InvokeRepeating("SpawnPowerUp", 2F, 10F);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>= spawnFrequency)
        {
            timer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int type = Random.Range(1, 4);
        if (type == 1)
        {
            if (HazardsPool.HazardPoolInstance == null)
            {
                CancelInvoke();
            }
            else
            {
                //Instantiate(hazardTemplate, myCollider.GetPointInVolume(), transform.rotation);
                if (HazardsPool.HazardPoolInstance != null)
                {
                    GameObject cloneHazard = HazardsPool.HazardPoolInstance.GetHazardPool();
                    cloneHazard.SetActive(true);
                    cloneHazard.transform.position = myCollider.GetPointInVolume();
                    cloneHazard.transform.rotation = transform.rotation;
                }
            }
        }
        if (type == 2)
        {
            if (HazardsPool.HazardPoolInstance == null)
            {
                CancelInvoke();
            }
            else
            {
                //Instantiate(invaderTemplate, myCollider.GetPointInVolume(), transform.rotation);
                if (HazardsPool.HazardPoolInstance != null)
                {
                    GameObject cloneDebris = HazardsPool.HazardPoolInstance.GetDebrisPool();
                    cloneDebris.SetActive(true);
                    cloneDebris.transform.position = myCollider.GetPointInVolume();
                    cloneDebris.transform.rotation = transform.rotation;
                }
            }
        }
        if (type == 3)
        {
            if (HazardsPool.HazardPoolInstance == null)
            {
                CancelInvoke();
            }
            else
            {
                //Instantiate(debrisTemplate, myCollider.GetPointInVolume(), transform.rotation);
                if (HazardsPool.HazardPoolInstance != null)
                {
                    GameObject cloneInvader = HazardsPool.HazardPoolInstance.GetInvaderPool();
                    cloneInvader.SetActive(true);
                    cloneInvader.transform.position = myCollider.GetPointInVolume();
                    cloneInvader.transform.rotation = transform.rotation;
                }
            }
        }
    }
    private void SpawnPowerUp()
    {
        if (powerUpTemplate == null)
        {
            CancelInvoke();
        }
        else
        {
            Instantiate(powerUpTemplate, myCollider.GetPointInVolume(), transform.rotation);
        }
    }
}