using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Jumper : ScriptableObject
{
    [Header("Jump")]
    [SerializeField, Tooltip("Радиус на реакцию прыжка от земли")] protected float _jumpRadius = 30f;
    [SerializeField, Tooltip("Сила прыжка")] protected float _jumpPower = 3f;
    [Range(50, 200), SerializeField, Tooltip("Время прыжка")] protected float _jumpSpeed = 100f;
    [HideInInspector] public bool overJump;
    [Space(5)]
    [Header("Layers")]
    [SerializeField, Tooltip("Слой с которого игрок может прыгать")]
    private LayerMask _whatIsGround;
    [SerializeField, Tooltip("Слой с платформ для прыжков")]
    protected LayerMask _whatIsJumpPlatform; 
    [Space(5)]
    [Header("Debbuger")]
    [SerializeField] protected DebbugType _debbugType; 
    protected enum DebbugType{None, Run, Jump, JumpPress,  }
    protected Character _character; 
    protected JumpPlatform _currentJumpPlatform;
    protected List<JumpPlatform> _jumpPlatformsList = new List<JumpPlatform>();
    protected bool _readyToJump;
    public virtual void Init(Character character)
    {
        _readyToJump = false;
        _character = character;
        JumpPlatform[] jumpPlayforms = FindObjectsOfType<JumpPlatform>();
        for (int i = 0; i < jumpPlayforms.Length; i++)
            _jumpPlatformsList.Add(jumpPlayforms[i]);
    }
    public abstract void Run(bool state);
    public virtual void Jump()
    {
        _character.rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    public abstract void OnGroundEnter(); 
    public abstract void JumpPress(); 
    protected bool OnGround()
    {
        return Physics.CheckSphere(_character.transform.position, 1f, _whatIsGround);
    }
}
