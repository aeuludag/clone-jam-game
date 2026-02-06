using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/New Dialogue")]
public class Dialogue : ScriptableObject
{
    public string speaker;
    public List<string> lines;
}
