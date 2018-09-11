using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour {

    [SerializeField]
    HazardSpawner theSpawner;

	void Start () {
        SingletonReferee.RefereeInstance.OnDefeat += StateDefeatState;
        SingletonReferee.RefereeInstance.OnRaiseDifficulty += RaiseDifficulty;
    }

	public void StateDefeatState()
    {
        Time.timeScale = 0F;
        print("Game Over");
    }
    public void RaiseDifficulty()
    {
        print("Difficulty raised");
        theSpawner.SpawnFrequency -= 1;
        theSpawner.SpawnFrequency = Mathf.Clamp(theSpawner.SpawnFrequency, 1, 10);
        print(theSpawner.SpawnFrequency);
    }
}
