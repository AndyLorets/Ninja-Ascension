using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "AIAutoJumper", menuName = "ScriptableObjects/Jumper/AIAutoJumper")]
public class AIAutoJumper : Jumper
{
    [SerializeField] private bool _targetWithCamera;
    private bool _isPlayer;
    private TargetPointer _targetPointer;
    private JumpPlatform _lastJumpPlatform;
    private const float _minDistToJumper = 1200f, _maxDistRay = 300f;
    private float _currentDuration; 
    public override void Init(Character character)
    {
        base.Init(character);
        _readyToJump = true;
        _isPlayer = character.transform.name == "Player"; 
        _targetPointer = _isPlayer ? GameObject.Find("TargetPointer (Player)").GetComponent<TargetPointer>() :
            GameObject.Find("TargetPointer (Enemy)").GetComponent<TargetPointer>();
        //if (!_isPlayer) _player = FindObjectOfType<Player>(); 
    }
    public override void Run(bool state = true)
    {
        if (_readyToJump && OnGround() && state)
        {
            if (!overJump)
                _character.animator.SetTrigger("Jump");
            else Jump();

            _readyToJump = false;
            return;
        }
        //_currentDuration = overJump ? duration * .1f : duration;
        //string idleName = _character.transform.position.y > 10f ? "IdleCrouch" : "Idle";
        //_character.animator.SetBool(idleName, OnGround());
    }
    public override void Jump()
    {
        base.Jump();
        _character.rb.isKinematic = true;

        float dist = Vector3.Distance(_character.transform.position, GetJumpPoint().characterPos);
        float duration = dist / _jumpSpeed;
        float currentDuration = overJump ? duration * .5f : duration;
        //Debug.Log($"Dist {dist} | duration {duration}");

        _character.transform.DOJump(GetJumpPoint().characterPos, _jumpPower, 1, duration).SetEase(Ease.Flash)
            .OnComplete(delegate () 
            { 
                _character.rb.isKinematic = false;
                _readyToJump = true;
                _character.transform.DORotate(_currentJumpPlatform.characterRot, 0.3f); 
            });
        //_character.transform.DORotate(GetJumpPoint().characterRot, currentJumpDuration).OnComplete(() => _readyToJump = true);

        if (_lastJumpPlatform != null)
            _lastJumpPlatform.character = null;
        _lastJumpPlatform = GetJumpPoint();
        _lastJumpPlatform.character = _character;
    }
    private JumpPlatform GetJumpPoint()
    {
        Vector3 origin = _targetWithCamera ? Camera.main.transform.position : _character.transform.position;
        Vector3 forward = _targetWithCamera ? Camera.main.transform.forward : _character.transform.forward;
        RaycastHit[] hits = Physics.SphereCastAll(origin + forward * 50, 50, forward,  _maxDistRay, _whatIsJumpPlatform);
        Vector3 pos = _character.transform.position;/*_targetWithCamera && !_isPlayer ? _player.transform.position : _character.transform.position;*/
        float distance = Mathf.Infinity;
        foreach (RaycastHit hit in hits)
        {
            JumpPlatform go = hit.collider.GetComponent<JumpPlatform>();
            Vector3 diff = go.characterPos - pos;
            float currentDist = diff.sqrMagnitude;
            bool bestDist = currentDist < distance && currentDist > _minDistToJumper; 
            if (bestDist && go.character == null)
            {
                _currentJumpPlatform = go;
                distance = currentDist;
                _targetPointer.SetTarget(go.transform);
            }
        }
        return _currentJumpPlatform;
    }
    public override void JumpPress()
    {
    }
    public override void OnGroundEnter()
    {
    }
}






