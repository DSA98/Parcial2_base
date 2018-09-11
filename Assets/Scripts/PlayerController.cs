using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float movementFactor;
    private bool canFire = true;
    private float coolDownTime = 0.5F;
    private Collider2D myCollider;

    private bool currentShieldActtive = false;

    private float health = 100;
    [SerializeField]
    Slider mHealthBar;

    /*[SerializeField]
    private Object bulletGO;
    [SerializeField]
    private Object bulletApGO;*/

    [SerializeField]
    GameObject barrier;

    PowerUpDecorator mPowerUps;

    public bool CurrentShieldActtive
    {
        get
        {
            return currentShieldActtive;
        }

        set
        {
            currentShieldActtive = value;
        }
    }

    protected bool InsideCamera(bool positive)
    {
        float direction = positive ? 1F : -1F;
        Vector3 cameraPoint = Camera.main.WorldToViewportPoint(
            new Vector3(
                myCollider.bounds.center.x + myCollider.bounds.extents.x * direction,
                0F,
                0F));
        return cameraPoint.x >= 0F && cameraPoint.x <= 1F;
    }

    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
        mHealthBar.value = health;
        mPowerUps = GetComponent<PowerUpDecorator>();
    }

    // Update is called once per frame
    private void Update()
    {
        movementFactor = Input.GetAxis("Horizontal");

        if (InsideCamera(movementFactor > 0F) && movementFactor != 0F)
        {
            transform.position += new Vector3(movementFactor * speed * Time.deltaTime, 0F, 0F);
        }

        if (/*bulletGO != null &&*/ Input.GetAxis("Fire1") != 0 && canFire)
        {
            //Instantiate(bulletGO, transform.position + (transform.up * 0.5F), Quaternion.identity);
            if (BulletsPool.BulletsPoolInstance != null)
            {
                GameObject cloneNormalBullet = BulletsPool.BulletsPoolInstance.GetNormalBulletPool();
                cloneNormalBullet.SetActive(true);
                cloneNormalBullet.transform.position = transform.position + (transform.up * 0.5F);
                cloneNormalBullet.transform.rotation = Quaternion.identity;
                cloneNormalBullet.GetComponent<Bullet>().AddImpulse();
                cloneNormalBullet.GetComponent<Bullet>().StartAutoDestroy();
            }
            print("Fiyah!");
            StartCoroutine("FireCR");
        }
        if (/*bulletApGO != null &&*/ Input.GetAxis("Fire2") != 0 && canFire)
        {
            //Instantiate(bulletApGO, transform.position + (transform.up * 0.5F), Quaternion.identity);
            if (BulletsPool.BulletsPoolInstance != null)
            {
                GameObject cloneApBullet = BulletsPool.BulletsPoolInstance.GetApBulletPool();
                cloneApBullet.SetActive(true);
                cloneApBullet.transform.position = transform.position + (transform.up * 0.5F);
                cloneApBullet.transform.rotation = Quaternion.identity;
                cloneApBullet.GetComponent<Bullet>().AddImpulse();
                cloneApBullet.GetComponent<Bullet>().StartAutoDestroy();
            }
            print("Fiyah!");
            StartCoroutine("FireCR");
        }
        GetPowerUpFromDecorator();
    }

    private void OnDestroy()
    {
        StopCoroutine("FireCR");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Hazard>() != null)
        {
            SingletonReferee.RefereeInstance.DefeatCondition();
        }
    }

    private IEnumerator FireCR()
    {
        canFire = false;
        yield return new WaitForSeconds(coolDownTime);
        canFire = true;
    }

    public void ReduceHealth(float _damageTaken)
    {
        health -= _damageTaken;
        mHealthBar.value = health;
        if (health <= 0)
        {
            SingletonReferee.RefereeInstance.DefeatCondition();
        }
    }

    private void GetPowerUpFromDecorator()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            mPowerUps.GetPowerUp(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            mPowerUps.GetPowerUp(2);
        }
    }

    public void ActivateShieldPowerUp()
    {
        currentShieldActtive = true;
        barrier.SetActive(true);
        Invoke("InactivateShieldPowerUp", 5f);
    }

    public void InactivateShieldPowerUp()
    {
        currentShieldActtive = false;
        barrier.SetActive(false);
    }
}