using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Debbuger", menuName = "ScriptableObjects/Debbuger/Debbuger")]
public class Debbuger : ScriptableObject
{
    public bool debbuging; 

    public void Log(string massage, Transform from)
    {
        if (debbuging)
            Debug.Log($"{from.name} send: {massage}"); 
    }
}
