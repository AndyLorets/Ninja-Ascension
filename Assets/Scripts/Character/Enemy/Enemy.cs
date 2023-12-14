using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Player _player;
    private AIFighter _aiFighter; 
    protected override void Awake()
    {
        base.Awake();
        _player = FindObjectOfType<Player>();
        _aiFighter = _fighter as AIFighter;
    }
    protected override void Start()
    {
        base.Start(); 
        StartCoroutine(_aiFighter.SetAutoJutsu(_aiFighter.jutsus[0].JutsuCode()));
    }
    private void Update()
    {
        bool state = CheckEnemySphere(false) || CheckEnemySphere(true); 
        _jumper.Run(state && _jumperActive);
        _aiFighter.Run(_fighterActive);
        _jumper.overJump = CheckEnemySphere(true);

        if (CheckJutsuSphere() && !_aiFighter.jutsuRun) SetFarJutsu(Random.Range(0, _aiFighter.jutsus.Length)); 

    }
    #region Fighter

    private void SetFarJutsu(int index)
    {
        if (!_fighterActive) return; 
        StartCoroutine(_aiFighter.SetAutoJutsu(_aiFighter.jutsus[index].JutsuCode()));
    }
    #endregion
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("JumpPlatform"))
        {
            bool rotateGround = collision.transform.eulerAngles.x > 12 || collision.transform.eulerAngles.x < -12;
            if (rotateGround) _rb.constraints = RigidbodyConstraints.FreezeAll;
            _skin.SetLegChakra(rotateGround);
        }
    }
}
