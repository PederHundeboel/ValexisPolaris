/**
 * @author [Michael Lykke Strand]
 * @email [milystrand@gmail.com]
 * @github [https://github.com/MichaelLykkeStrand]
 */
using System;
using UnityEngine;


public abstract class Container : MonoBehaviour
{
    [SerializeField] protected int maxValue, value;
    public const int MIN = 0;
    public event Action OnEmpty;
    public event Action OnChange;
    public event Action OnFull;
    public event Action OnDecrease;
    public event Action OnIncrease;

    private void Awake()
    {
        if (this.value > this.maxValue)
        {
            this.value = maxValue;
        }
    }

    public int GetValue()
    {
        return value;
    }

    public int GetMax()
    {
        return this.maxValue;
    }

    public virtual void Add(int addValue)
    {
        this.value = Mathf.Clamp(value + addValue, MIN, this.maxValue);
        OnIncrease?.Invoke();
        OnChange?.Invoke();
    }

    public virtual void Subtract(int subValue)
    {
        this.value = Mathf.Clamp(value - subValue, MIN, this.maxValue);
        if (value <= MIN)
        {
            this.value = MIN;
            OnEmpty?.Invoke();
        }
        OnDecrease?.Invoke();
        OnChange?.Invoke();
    }


    public void SetMax(int newMaxValue)
    {
        if (newMaxValue <= MIN) newMaxValue = 1;
        if (this.value > newMaxValue)
        {
            this.value = newMaxValue;
        }
        this.maxValue = newMaxValue;
    }
}