using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : HazardObjects
{
    private int sense = 1;
    private bool dead = false;

    public bool Dead
    {
        get
        {
            return dead;
        }

        set
        {
            dead = value;
        }
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();
        ChangingSense();
    }
	
	// Update is called once per frame
	void Update () {
        if (!dead)
        {
            GetFunctionFromDecorator();
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private void ChangingSense()
    {
        InvokeRepeating("ChangeSense", 0.5f, 0.5f);
    }

    private void ChangeSense()
    {
        sense = sense * -1;
    }

    protected override void OnHazardDestroyed()
    {
        myCollider.enabled = false;
        StartCoroutine(DeathAnimation());
    }

    public IEnumerator DeathAnimation()
    {
        dead = true;
        for(float i = 0; i < spinTime; i += Time.deltaTime)
        {
            transform.eulerAngles += (6 * transform.forward * 1);
            yield return null;
        }
        HazardsPool.HazardPoolInstance.ReturnGameObjectInvader(gameObject);
    }

    public void GetFunctionFromDecorator()
    {
        if (DecoratorHazards.DecHazardsInstance != null)
        {
            transform.position += DecoratorHazards.DecHazardsInstance.GetFunction(1)*sense;
        }
    }
}
