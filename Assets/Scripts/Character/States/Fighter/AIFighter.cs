using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AIFighter", menuName = "ScriptableObjects/Fighters/AIFighter")]
public class AIFighter : FighterBase
{
    public bool jutsuRun {get; private set;}
    public IEnumerator SetAutoJutsu(string code, Transform target = null)
    {
        jutsuRun = true; 
        _jutsuCode = code;
        _character.animator.SetTrigger("W");
        yield return new WaitForSeconds(.2f);
        _character.animator.SetTrigger("W");
        yield return new WaitForSeconds(.2f);
        _character.animator.SetTrigger("W");
        yield return new WaitForSeconds(.2f);
        _character.animator.SetTrigger("W");
        yield return new WaitForSeconds(.2f);
        RunJutsu(target);
        jutsuRun = false;
    }
}
