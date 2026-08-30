using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectorManager : MonoBehaviour
{
    public SelectorContainer[] characterSelectors = new SelectorContainer[4];
    public AllyData[] allies;
    public TeamData currentTeam;
    public GameObject[] characterPrefabs;

    void Start()
    {
        for (int i = 0; i < currentTeam.defaultParty.Length; i++)
        {
            currentTeam.team[i] = currentTeam.defaultParty[i];    
        }
        
        for (int i = 0; i < currentTeam.team.Length; i++)
        {
            for (int j = 0; j < characterPrefabs.Length; j++)
            {
                if (currentTeam.team[i] == characterPrefabs[j])
                {
                    characterSelectors[i].dropdown.value = j;
                    characterSelectors[i].PreviousValue = j;
                }
            }
        }
    }

    public void OnDropdownChange()
    {
        for (int i = 0; i < characterSelectors.Length; i++)
        {
            for (int j = 0; j < characterSelectors.Length; j++)
            {
                if (characterSelectors[j].dropdown.value == characterSelectors[i].dropdown.value && i != j)
                {
                    characterSelectors[i].dropdown.value = characterSelectors[i].PreviousValue;
                    return;
                }
            }
            
            characterSelectors[i].speciesLabel.text = allies[characterSelectors[i].dropdown.value].species;
            characterSelectors[i].attackLabel.text = allies[characterSelectors[i].dropdown.value].attackName;
            characterSelectors[i].healthLabel.text = allies[characterSelectors[i].dropdown.value].initialHealth.ToString();
            characterSelectors[i].damageLabel.text = allies[characterSelectors[i].dropdown.value].initialDamage.ToString();
            characterSelectors[i].regenLabel.text = allies[characterSelectors[i].dropdown.value].initialRegen.ToString();
            characterSelectors[i].characterIcon.sprite = allies[characterSelectors[i].dropdown.value].allyIcon;
            characterSelectors[i].PreviousValue = characterSelectors[i].dropdown.value;
            
            currentTeam.team[i] = characterPrefabs[characterSelectors[i].dropdown.value];
            
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(3);
    }
}

[System.Serializable]
public class SelectorContainer
{
    public GameObject selector;
    public TMP_Dropdown dropdown;
    [NonSerialized]
    public int PreviousValue;
    public Image characterIcon;
    public TMP_Text speciesLabel;
    public TMP_Text attackLabel;
    public TMP_Text healthLabel;
    public TMP_Text damageLabel;
    public TMP_Text regenLabel;
}
