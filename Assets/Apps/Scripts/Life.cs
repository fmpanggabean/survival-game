using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private int current;
    [SerializeField] private int max;

    public void Set(int value)
    {
        current = value;
    }

    public void Reduce (int value)
    {
        current -= value;
        if (current < 0) current = 0;
    }

    public void Gain(int value)
    {
        current += value;
        if (current > max) current = max;
    }
}
