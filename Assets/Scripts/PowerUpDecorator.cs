using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDecorator : MonoBehaviour {

    [SerializeField]
    Forcefield mForcefield;
    [SerializeField]
    Barrier mBarrier;

    public void GetPowerUp(int _input)
    {
        if (_input == 1)
        {
            mForcefield.ActivateShield();
        }
        if (_input == 2)
        {
            mBarrier.ActivateShield();
        }
    }
}
