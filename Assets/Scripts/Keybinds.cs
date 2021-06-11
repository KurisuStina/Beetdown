using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybinds", menuName = "Keybinds")]
public class Keybinds : ScriptableObject
{
    [Header("Movement")]
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    [Header("UI")]
    public KeyCode pauseKey;
    //public KeyCode scoreBoard;

    [Header("Game")]
    public KeyCode attack;
    public KeyCode reload;
}
