using System.Collections.Generic;
using UnityEngine;
public abstract class PlayerJumper : Jumper
{
    protected PostProgressingEffect _postProgressingEffect;
    protected float _jumpTime;
    protected bool _onJumpAnim;
    private float _timeDeley;

    public override void Init(Character character)
    {
        base.Init(character);
        _postProgressingEffect = FindObjectOfType<PostProgressingEffect>();
        _jumpTime = _jumpSpeed;
    }
    public override void Run(bool state = true)
    {
        string idleName = _character.transform.position.y > 10f ? "IdleCrouch" : "Idle";
        _timeDeley += Time.deltaTime; 
        if (_readyToJump && OnGround())
        {
            _jumpTime -= .4f;
            SetAnimJump("Jump"); 
            _readyToJump = false;
            return;
        }
        _character.animator.SetBool(idleName, OnGround());

        if (_debbugType == DebbugType.Run) Debug.Log($"Run");
    }
    public override void Jump()
    {
        base.Jump(); 
        _postProgressingEffect.SetPostEffects(true);
        _jumpTime = _jumpSpeed;
        _onJumpAnim = false;

        if (_debbugType == DebbugType.Jump) Debug.Log($"Jump"); 
    }
    public override void OnGroundEnter()
    {
        _timeDeley = 0;
    }

    public override void JumpPress()
    {
        if (!_onJumpAnim && !_readyToJump)
        {
            if (_debbugType == DebbugType.JumpPress && _currentJumpPlatform != null)
                Debug.Log($"{_doubleJump} doubleJump | {_nearToJumper} nearToJumper | {_currentJumpPlatform.name} _currentJumpPoint");

            if (_doubleJump || _nearToJumper) _readyToJump = true;
            else if (OnGround() && !_doubleJump) { SetAnimJump("JumpingUp");}
        }
    }
    private void SetAnimJump(string value)
    {
        _character.animator.SetTrigger(value);
        _onJumpAnim = true;
    }
    private float DistToJumper() => Vector3.Distance(_character.transform.position, _currentJumpPlatform.characterPos);
    protected bool _doubleJump => OnGround() && _timeDeley < .3f;
    protected bool _nearToJumper => !OnGround() && DistToJumper() < _jumpRadius;
}
