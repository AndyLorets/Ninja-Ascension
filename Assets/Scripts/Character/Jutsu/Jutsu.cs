using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Jutsu : ScriptableObject
{
    [SerializeField] private string _jutsuCode;
    protected Character _character;

    public virtual void Init(Character character)
    {
        _character = character;
    }
    public abstract void Attack(Transform target = null); 
    public string JutsuCode() { return _jutsuCode; }
}
