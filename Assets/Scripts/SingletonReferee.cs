using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingletonReferee : MonoBehaviour {

    private static SingletonReferee refereeInstance = null;
    public static SingletonReferee RefereeInstance
    {
        get
        {
            return refereeInstance;
        }

        set
        {
            refereeInstance = value;
        }
    }

    public delegate void Defeated();
    public event Defeated OnDefeat;
    public delegate void RaiseDifficulty();
    public event RaiseDifficulty OnRaiseDifficulty;

    [SerializeField]
    Text mText;
    private int score = 0;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    private float timer = 0;

    // Use this for initialization
    void Awake() {
        if (refereeInstance == null)
        {
            refereeInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        mText.text = "Hazards destroyed: " + score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 20f)
        {
            timer = 0;
            if (OnRaiseDifficulty != null)
            {
                OnRaiseDifficulty();
            }
        }
	}

    public void AddScore()
    {
        score += 1;
        mText.text = "Hazards destroyed: " + score.ToString() ;
    }

    public void DefeatCondition()
    {
        if (OnDefeat != null)
        {
            OnDefeat();
        }
    }
}
