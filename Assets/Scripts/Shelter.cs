using UnityEngine;
using UnityEngine.UI;

public class Shelter : MonoBehaviour, IReduceHealthShelter
{
    [SerializeField]
    private float maxResistance = 100;
    private float regenTimer = 0f;
    private bool regenerating = false;
    [SerializeField]
    GameObject myDefender;
    [SerializeField]
    Slider mSlider;

    public float MaxResistance
    {
        get
        {
            return maxResistance;
        }
        protected set
        {
            maxResistance = value;
        }
    }

    private void Start()
    {
        mSlider.value = maxResistance;
    }

    private void Update()
    {
        if (maxResistance <= 0)
        {
            SingletonReferee.RefereeInstance.DefeatCondition();
        }
        if (maxResistance < 100)
        {
            RegenHealth();
        }
        mSlider.value = maxResistance;
    }

    public void Damage(int damage)
    {
        maxResistance -= damage;
        print(maxResistance);
    }

    private void RegenHealth()
    {
        regenTimer += Time.deltaTime;
        if (regenTimer >= 5f)
        {
            maxResistance += 5;
            regenTimer = 0;
        }
    }
}