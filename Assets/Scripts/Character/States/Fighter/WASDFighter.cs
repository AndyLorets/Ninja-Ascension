using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 
[CreateAssetMenu(fileName = "WASDFighter", menuName = "ScriptableObjects/Fighters/WASDFighter")]
public class WASDFighter : FighterBase
{
    private CameraSwitcher _camSwitch;
    public override void Init(Character character)
    {
        base.Init(character);
        _camSwitch = FindObjectOfType<CameraSwitcher>(); 
    }
    public override void Run(bool state)
    {
        if (!state) return;
        base.Run(state);
        RaycastHit hit = new RaycastHit();
        _character.RaySphereCast(_character.whatIsEnemy, ref hit, _raySphereRadius);

        if (Input.GetKeyDown(KeyCode.W)) SetJutsu("W");
        if (Input.GetKeyDown(KeyCode.A)) SetJutsu("A");
        if (Input.GetKeyDown(KeyCode.S)) SetJutsu("S");
        if (Input.GetKeyDown(KeyCode.D)) SetJutsu("D");
        if (Input.GetKeyDown(KeyCode.LeftShift)) RunJutsu(hit.transform);
    }
    protected override void RunJutsu(Transform target = null)
    {
        base.RunJutsu(target);
        _camSwitch.SetShootJutsuEffect(); 
    }
}

