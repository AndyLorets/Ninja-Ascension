using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[CreateAssetMenu(fileName = "ControllingPlayerJumper", menuName = "ScriptableObjects/Jumper/ControllingPlayerJumper")]
public class ControllingPlayerJumper : PlayerJumper
{
    [SerializeField] private LayerMask _whatIsJumpPlatform;
    [SerializeField] private DrawGizmos _drawGizmos;

    public override void Init(Character character)
    {
        base.Init(character);
        _drawGizmos = Instantiate(_drawGizmos, _character.transform);
    }
    public override void JumpPress()
    {
        base.JumpPress();
        RaycastHit hit = new RaycastHit(); 
        if (!_character.RaySphereCast(_whatIsJumpPlatform, ref hit)) { _onJumpAnim = false; return; }

        if (_nearToJumper || OnGround() || _doubleJump)
        {
            if (_currentJumpPlatform != null)
                _currentJumpPlatform.ColliderEnabled(true);
            _currentJumpPlatform = hit.collider.GetComponent<JumpPlatform>();
            _currentJumpPlatform.ColliderEnabled(false);
        }
    }
    public override void Jump()
    {
        if (_currentJumpPlatform == null) return;
        _character.rb.isKinematic = true; 
        _character.transform.DOJump(_currentJumpPlatform.characterPos, _jumpPower, 1, _jumpTime).SetEase(Ease.Flash).OnComplete(() => _character.rb.isKinematic = false);
        _character.transform.DORotate(_currentJumpPlatform.characterRot, 1).OnComplete(() => _postProgressingEffect.SetPostEffects());
        base.Jump();
    }
    public override void Run(bool state = true)
    {
        base.Run(state);
        //RayJumpPlatform();
        //_drawGizmos.DrawingGizmos(_maxDistRayJP, _currentDistRayJP, _sphereRadius, _origin, _direction, _hit.point, _nearToJumper);
    }
}
