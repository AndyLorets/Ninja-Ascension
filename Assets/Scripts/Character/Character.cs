using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Character : MonoBehaviour
{
    [Header("States")]
    [SerializeField] protected Jumper _jumper;
    [SerializeField] protected FighterBase _fighter;
    [SerializeField] protected bool _jumperActive, _fighterActive;
    [Space(5)]
    [Header("Components")]
    [SerializeField] protected Skin _skin;
    protected Animator _animator;
    protected Rigidbody _rb;
    public Rigidbody rb { get { return _rb; } private set { } }
    public Jumper jumper { get { return _jumper; } private set { } }
    public Animator animator { get { return _animator; } private set { } }
    [Space(5)]
    [Header("AttackSettings")]
    [SerializeField] private Transform _farAttackPos; 
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private LayerMask _whatIsJutsu;
    [SerializeField] protected float _radiusAttack = 30f;
    //[SerializeField] protected float _followingDistance = 60f;
    [Range(20, 50)]
    [SerializeField] private float _checkSphereRadius = 30;
    public LayerMask whatIsEnemy { get { return _whatIsEnemy; } private set { } }
    public Transform farAttackPos { get { return _farAttackPos; } private set { } }
    [Space(10)]
    [Header("Gizmos")]
    [SerializeField] private bool _drawGizsom; 
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _jumper = Instantiate(_jumper);
        _jumper.Init(this);
        _fighter = Instantiate(_fighter);
        _fighter.Init(this);
        _skin.Init(this);
        _animator = _skin.animator;
    }
    protected virtual void Start()
    {
        //ChangeState(_idleState);
        //StartCoroutine(CheckState()); 
    }
    protected bool CheckEnemySphere(bool forward)
    {
        Vector3 dir = forward ? transform.forward : -transform.forward;
        return Physics.CheckSphere(transform.position + dir * _checkSphereRadius, _checkSphereRadius, _whatIsEnemy);
    }
    protected bool CheckJutsuSphere()
    {
        return Physics.CheckSphere(transform.position, _checkSphereRadius, _whatIsJutsu);
    }
    #region Raycast
    private const float _maxDistRayJP = 300f;
    private float _sphereRadius = 12; 
    private float _currentDistRayJP;
    private Vector3 _origin;
    private Vector3 _direction;
    public bool RaySphereCast(LayerMask layerMask, ref RaycastHit hit, float sphereRadius = 12)
    {
        _sphereRadius = sphereRadius;
        _origin = Camera.main.transform.position;
        _direction = Camera.main.transform.forward;
        if (Physics.SphereCast(_origin, _sphereRadius, _direction, out hit, _maxDistRayJP, layerMask))
        {
            _currentDistRayJP = hit.distance;
            return true;
        }
        _currentDistRayJP = _maxDistRayJP;
        return false;
    }
    #endregion
    private void OnDrawGizmos()
    {
        if (!_drawGizsom) return;
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, .2f);
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, _radiusAttack);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, _followingDistance);

        //bool _freeRay = _maxDistRayJP == _currentDistRayJP;
        //Gizmos.color = _freeRay ? Color.black : Color.blue;
        //Gizmos.DrawLine(_origin, _origin + _direction * _currentDistRayJP);
        //Gizmos.DrawWireSphere(_origin + _direction * _currentDistRayJP, _sphereRadius);

        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(Camera.main.transform.position + Camera.main.transform.forward * 50, Camera.main.transform.position + Camera.main.transform.forward * 300f);
        //Gizmos.DrawWireSphere(Camera.main.transform.position + Camera.main.transform.forward * 50, 50);

        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position + transform.forward * _checkSphereRadius, _checkSphereRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position - transform.forward * _checkSphereRadius, _checkSphereRadius);
    }

}
