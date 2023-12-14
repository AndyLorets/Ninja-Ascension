using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FighterBase : ScriptableObject
{
    [Range(1, 10)]
    [SerializeField] protected float _raySphereRadius = 5;
    [SerializeField] protected Jutsu[] _farJutsus;
    [SerializeField] protected Jutsu[] _blockJutsus;
    private List<Jutsu> _jutsusList = new List<Jutsu>(); 
    public Jutsu[] jutsus { get { return _farJutsus; } private set { } }
    protected Character _character;
    protected string _jutsuCode;
    public virtual void Init(Character character)
    {
        _character = character;
        for (int i = 0; i < _farJutsus.Length; i++)
        {
            _farJutsus[i] = Instantiate(_farJutsus[i]); 
            _farJutsus[i].Init(character);
            _jutsusList.Add(_farJutsus[i]); 
        }
    }

    public virtual void Run(bool state)
    {
    }
    protected void SetJutsu(string code = "RunJutsu")
    {
        _jutsuCode = code == "RunJutsu" ? "" : _jutsuCode + code;
        _character.animator.SetTrigger("W");
    }
    protected virtual void RunJutsu(Transform target = null)
    {
        for (int i = 0; i < _jutsusList.Count; i++)
        {
            if (_jutsuCode == _jutsusList[i].JutsuCode())
                _jutsusList[i].Attack(target);
        }

        SetJutsu();
    }
}
