using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _legChakra;
    [SerializeField] private GameObject _jumpEffect;
    public Animator animator { get; private set; }
    private Character _character;
    private void Awake()
    {
        animator = GetComponent<Animator>(); 
    }
    public void Init(Character character)
    {
        _character = character;
    }
    public void Jump(float jumpPower = 0)
    {
        _character.jumper.Jump();
        SetLegChakra(false); 
        if(jumpPower > 0) Instantiate(_jumpEffect, _character.transform.position, _character.transform.rotation); 
    }
    public void SetLegChakra(bool state)
    {
        for (int i = 0; i < _legChakra.Length; i++)
        {
            if (state) _legChakra[i].Play();
            else _legChakra[i].Stop();
        }
    }
}
