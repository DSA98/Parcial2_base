using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class HazardObjects : MonoBehaviour
{
    protected Collider2D myCollider;
    protected Rigidbody2D myRigidbody;

    [SerializeField]
    protected float resistance = 1F;
    private int damageShelterMag = 20;
    protected float damageToPlayer = 10;

    protected float spinTime = 1F;
    public int DamageShelterMag
    {
        get
        {
            return damageShelterMag;
        }

        set
        {
            damageShelterMag = value;
        }
    }

    public Collider2D MyCollider
    {
        get
        {
            return myCollider;
        }

        set
        {
            myCollider = value;
        }
    }

    public float Resistance
    {
        get
        {
            return resistance;
        }

        set
        {
            resistance = value;
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        myCollider = GetComponent<Collider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            //TODO: Make this to reduce damage using Bullet.damage attribute
            resistance -= 1;

            if (resistance == 0)
            {
                OnHazardDestroyed();
            }
            if (SingletonReferee.RefereeInstance != null)
            {
                SingletonReferee.RefereeInstance.AddScore();
            }
        }
        if (collision.gameObject.GetComponent<IReduceHealthShelter>() != null)
        {
            collision.gameObject.GetComponent<IReduceHealthShelter>().Damage(damageShelterMag);
            print("Damage Done to a shelter");
            OnHazardDestroyed();
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

    protected abstract void OnHazardDestroyed();

    public void ApBulletImpact()
    {
        OnHazardDestroyed();
    }

    public void HitOnBarrier()
    {
        OnHazardDestroyed();
    }
}