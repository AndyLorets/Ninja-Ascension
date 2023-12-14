using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Joystick _joystick; 
    private IInput _input;
    public void Init(IInput input)
    {
        _input = input; 
    }
    public void Look()
    {
        _input.Lookamotion(new Vector2(_joystick.Horizontal, _joystick.Vertical));
    }
    public void JumpBtn()
    {
        _input.JumpBtn(); 
    }
}
