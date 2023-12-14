using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAttackPool : MonoBehaviour
{
    [SerializeField] private ParticleSystem _lastEffect;
    private Transform _target;
    private const float _speed = 100f;
    private float _t = 1f;
    public bool isPlayer;/*{ private get; set; }*/
    private void Start()
    {
        Destroy(gameObject, 6f);
    }
    public void Update()
    {
        _t -= Time.deltaTime;
        if (_target != null && _t < .9f) { transform.LookAt(_target.position + Vector3.up); }
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
    public void SetTarget(Transform target) => _target = target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isPlayer) return;
        else if (other.CompareTag("Enemy") && !isPlayer) return;
        Destroy(gameObject);
        Transform inst = Instantiate(_lastEffect, transform.position, Quaternion.identity).transform;
        inst.localScale *= 3; 
    }
}
