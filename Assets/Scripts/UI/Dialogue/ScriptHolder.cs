using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptHolder", menuName = "ScriptableObjects/ScriptHolder", order = 1)]
public class ScriptHolder : ScriptableObject
{
    [TextArea]
    public List<string> script;
}