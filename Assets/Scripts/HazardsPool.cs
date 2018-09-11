using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardsPool : MonoBehaviour {

    private static HazardsPool hazardPoolInstance = null;
    public static HazardsPool HazardPoolInstance
    {
        get
        {
            return hazardPoolInstance;
        }

        set
        {
            hazardPoolInstance = value;
        }
    }

    [SerializeField]
    GameObject hazard, invader, debris;
    List<GameObject> hazardPool, invaderPool, debrisPool;
    private float poolSizes = 6;


    // Use this for initialization
    void Awake () {
        if (hazardPoolInstance == null)
        {
            hazardPoolInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}

    private void Start()
    {
        hazardPool = new List<GameObject>();
        invaderPool = new List<GameObject>();
        debrisPool = new List<GameObject>();
        for (int i = 0; i < poolSizes; i++)
        {
            GameObject cloneHazard = Instantiate(hazard);
            cloneHazard.SetActive(false);
            hazardPool.Add(cloneHazard);
        }
        for (int i = 0; i < poolSizes; i++)
        {
            GameObject cloneInvader = Instantiate(invader);
            cloneInvader.SetActive(false);
            invaderPool.Add(cloneInvader);
        }
        for (int i = 0; i < poolSizes; i++)
        {
            GameObject cloneDebris = Instantiate(debris);
            cloneDebris.SetActive(false);
            debrisPool.Add(cloneDebris);
        }
    }

    public GameObject GetHazardPool()
    {
        if (hazardPool.Count > 0)
        {
            return AllocateHazardPoolObject();
        }
        if (hazardPool.Count <= 0)
        {
            ProduceHazardPool();
            return AllocateHazardPoolObject();
        }
        return null;
    }
    public GameObject GetInvaderPool()
    {
        if (invaderPool.Count > 0)
        {
            return AllocateInvaderPoolObject();
        }
        if (invaderPool.Count <= 0)
        {
            ProduceInvaderPool();
            return AllocateInvaderPoolObject();
        }
        return null;
    }
    public GameObject GetDebrisPool()
    {
        if (debrisPool.Count > 0)
        {
            return AllocateDebrisPoolObject();
        }
        if (debrisPool.Count <= 0)
        {
            ProduceDebrisPool();
            return AllocateDebrisPoolObject();
        }
        return null;
    }

    public void ProduceHazardPool()
    {
        GameObject cloneObjectPool = Instantiate(hazard);
        cloneObjectPool.SetActive(false);
        hazardPool.Add(cloneObjectPool);
    }
    public void ProduceInvaderPool()
    {
        GameObject cloneObjectPool = Instantiate(invader);
        cloneObjectPool.SetActive(false);
        invaderPool.Add(cloneObjectPool);
    }
    public void ProduceDebrisPool()
    {
        GameObject cloneObjectPool = Instantiate(debris);
        cloneObjectPool.SetActive(false);
        debrisPool.Add(cloneObjectPool);
    }

    //Allocate or assign an object from the pools
    public GameObject AllocateHazardPoolObject()
    {
        for (int i = 0; i < hazardPool.Count; i++)
        {
            //In case of an inactive object in the pool
            if (!hazardPool[i].activeInHierarchy)
            {
                //We return the pool object and we eliminate it from the list for it's use
                GameObject objectForPooling = hazardPool[i];
                hazardPool.Remove(hazardPool[i]);
                return objectForPooling;
            }
        }
        return null;
    }
    public GameObject AllocateInvaderPoolObject()
    {
        for (int i = 0; i < invaderPool.Count; i++)
        {
            //In case of an inactive object in the pool
            if (!invaderPool[i].activeInHierarchy)
            {
                //We return the pool object and we eliminate it from the list for it's use
                GameObject objectForPooling = invaderPool[i];
                invaderPool.Remove(invaderPool[i]);
                return objectForPooling;
            }
        }
        return null;
    }
    public GameObject AllocateDebrisPoolObject()
    {
        for (int i = 0; i < debrisPool.Count; i++)
        {
            //In case of an inactive object in the pool
            if (!debrisPool[i].activeInHierarchy)
            {
                //We return the pool object and we eliminate it from the list for it's use
                GameObject objectForPooling = debrisPool[i];
                debrisPool.Remove(debrisPool[i]);
                return objectForPooling;
            }
        }
        return null;
    }

    //Return GameObjects to pools
    public void ReturnGameObjectHazard(GameObject _gamObjectHazard)
    {
        if (_gamObjectHazard.GetComponent<HazardObjects>() != null)
        {
            _gamObjectHazard.GetComponent<HazardObjects>().Resistance = 1;
        }
        _gamObjectHazard.SetActive(false);
        hazardPool.Add(_gamObjectHazard);
    }
    public void ReturnGameObjectInvader(GameObject _gamObjectInvader)
    {
        if (_gamObjectInvader.GetComponent<HazardObjects>() != null)
        {
            _gamObjectInvader.GetComponent<Invader>().Dead = false;
            _gamObjectInvader.GetComponent<HazardObjects>().MyCollider.enabled = true;
            _gamObjectInvader.GetComponent<HazardObjects>().Resistance = 1;
        }
        _gamObjectInvader.SetActive(false);
        invaderPool.Add(_gamObjectInvader);
    }
    public void ReturnGameObjectDebris(GameObject _gamObjectDebris)
    {
        if (_gamObjectDebris.GetComponent<HazardObjects>() != null)
        {
            _gamObjectDebris.GetComponent<HazardObjects>().Resistance = 1;
        }
        _gamObjectDebris.SetActive(false);
        debrisPool.Add(_gamObjectDebris);
    }
}
