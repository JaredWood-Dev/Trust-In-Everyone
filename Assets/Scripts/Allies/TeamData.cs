using UnityEngine;

[CreateAssetMenu(fileName = "TeamData", menuName = "Scriptable Objects/TeamData")]
public class TeamData : ScriptableObject
{
    public GameObject[] team;
    public GameObject[] defaultParty;
}
