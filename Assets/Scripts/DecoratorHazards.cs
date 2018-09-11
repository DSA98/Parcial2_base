using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorHazards : MonoBehaviour {

    private static DecoratorHazards decHazardsInstance = null;
    public static DecoratorHazards DecHazardsInstance
    {
        get
        {
            return decHazardsInstance;
        }

        set
        {
            decHazardsInstance = value;
        }
    }

    Vector3 posChange;

    // Use this for initialization
    void Awake () {
        if (decHazardsInstance == null)
        {
            decHazardsInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public Vector3 GetFunction(int _id)
    {
        if (_id == 1)
        {
            posChange = (transform.right * 3f) * Time.deltaTime;
        }
        if (_id == 2)
        {
            posChange = transform.forward * 1;
        }
        return posChange;
    }

}
