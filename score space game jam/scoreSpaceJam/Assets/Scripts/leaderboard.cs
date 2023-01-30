using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "leaderboard", menuName = "leaderboard")]
public class leaderboard : ScriptableObject
{
    public string[] names;
    public int[] scores;
}
