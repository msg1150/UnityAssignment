using System;
using UnityEngine;

[Serializable]
public class SkillEffect
{
    [SerializeField] private string statName;
    [SerializeField] private int amount;

    public string StatName => statName;
    public int Amount => amount;
}