using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour {

    private static BulletsPool bulletsPoolInstance = null;
    public static BulletsPool BulletsPoolInstance
    {
        get
        {
            return bulletsPoolInstance;
        }

        set
        {
            bulletsPoolInstance = value;
        }
    }

    [SerializeField]
    GameObject normalBullet, apBullet;
    List<GameObject> normalBulletPool, apBulletPool;
    private float poolSizes = 6;

    void Awake()
    {
        if (bulletsPoolInstance == null)
        {
            bulletsPoolInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        normalBulletPool = new List<GameObject>();
        apBulletPool = new List<GameObject>();
        for (int i = 0; i < poolSizes; i++)
        {
            GameObject cloneNormalBullet = Instantiate(normalBullet);
            cloneNormalBullet.SetActive(false);
            normalBulletPool.Add(cloneNormalBullet);
        }
        for (int i = 0; i < poolSizes; i++)
        {
            GameObject cloneApBullet = Instantiate(apBullet);
            cloneApBullet.SetActive(false);
            apBulletPool.Add(cloneApBullet);
        }
    }
    public GameObject GetNormalBulletPool()
    {
        if (normalBulletPool.Count > 0)
        {
            return AllocateNormalBulletPoolObject();
        }
        if (normalBulletPool.Count <= 0)
        {
            ProduceNormaBulletPool();
            return AllocateNormalBulletPoolObject();
        }
        return null;
    }
    public GameObject GetApBulletPool()
    {
        if (apBulletPool.Count > 0)
        {
            return AllocateApBulletPoolObject();
        }
        if (apBulletPool.Count <= 0)
        {
            ProduceApBulletPool();
            return AllocateApBulletPoolObject();
        }
        return null;
    }
    public void ProduceNormaBulletPool()
    {
        GameObject cloneObjectPool = Instantiate(normalBullet);
        cloneObjectPool.SetActive(false);
        normalBulletPool.Add(cloneObjectPool);
    }
    public void ProduceApBulletPool()
    {
        GameObject cloneObjectPool = Instantiate(apBullet);
        cloneObjectPool.SetActive(false);
        apBulletPool.Add(cloneObjectPool);
    }
    //Allocate or assign an object from the pools
    public GameObject AllocateNormalBulletPoolObject()
    {
        for (int i = 0; i < normalBulletPool.Count; i++)
        {
            //In case of an inactive object in the pool
            if (!normalBulletPool[i].activeInHierarchy)
            {
                //We return the pool object and we eliminate it from the list for it's use
                GameObject objectForPooling = normalBulletPool[i];
                normalBulletPool.Remove(normalBulletPool[i]);
                return objectForPooling;
            }
        }
        return null;
    }
    public GameObject AllocateApBulletPoolObject()
    {
        for (int i = 0; i < apBulletPool.Count; i++)
        {
            //In case of an inactive object in the pool
            if (!apBulletPool[i].activeInHierarchy)
            {
                //We return the pool object and we eliminate it from the list for it's use
                GameObject objectForPooling = apBulletPool[i];
                apBulletPool.Remove(apBulletPool[i]);
                return objectForPooling;
            }
        }
        return null;
    }
    //Return GameObjects to pools
    public void ReturnGameObjectNormalBullet(GameObject _gamObjectFarm)
    {
        _gamObjectFarm.SetActive(false);
        normalBulletPool.Add(_gamObjectFarm);
    }
    public void ReturnGameObjectApBullet(GameObject _gamObjectTower)
    {
        _gamObjectTower.SetActive(false);
        apBulletPool.Add(_gamObjectTower);
    }
}
