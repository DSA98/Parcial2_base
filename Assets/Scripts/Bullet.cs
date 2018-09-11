using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    protected int damage = 1;

    protected Collider2D myCollider;
    [SerializeField]
    protected Rigidbody2D myRigidbody;

    [SerializeField]
    protected float force = 10F;

    [SerializeField]
    protected float autoDestroyTime = 5F;

    // Use this for initialization
    protected virtual void Start()
    {
        myCollider = GetComponent<Collider2D>();

    }

    protected abstract void AutoDestroy();

    public abstract void AddImpulse();

    public void StartAutoDestroy()
    {
        Invoke("AutoDestroy", autoDestroyTime);
    }
}