using System.Collections;
using System.Collections.Generic;
using DG.Tweening; 
using UnityEngine;
[CreateAssetMenu(fileName = "FireBallJutsu", menuName = "ScriptableObjects/Jutsus/FireBallJutsu")]
public class FireBallJutsu : Jutsu
{
    [SerializeField] private FarAttackPool _sphere;
    public override void Attack(Transform target = null)
    {
        bool isPlayer = _character.name == "Player" ? true : false;
        Vector3 dir = isPlayer ? Camera.main.transform.GetChild(0).position : _character.transform.position + _character.transform.forward; 
        FarAttackPool pool = Instantiate(_sphere);
        pool.isPlayer = isPlayer;
        pool.transform.position = _character.farAttackPos.position; 
        pool.transform.LookAt(dir);
        if (target != null) pool.SetTarget(target);

        //Debug.Log($"FireBallJutsu from - {_character.transform.name}");
    }
}
