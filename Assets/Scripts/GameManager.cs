using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private Health _plrHealth;
    private PlayerAttack _pltAttack;
    
    [Header("Ally Slots")]
    public AllyDataContainer[] party = new AllyDataContainer[4];

    public GameObject damageIndicator;

    [Header("UI Elements")]
    public Image healthBar;

    public TMP_Text waveCounter;
    public TMP_Text remainingCounter;
    public GameObject nextWaveButton;
    private WaveManager _waveManager;
    public Image abilityFill;
    
    [Header("Upgrade Screen")]
    public GameObject upgradeScreen;
    public Button[] upgradeButtons;
    public StatPanel[] statCounters;
    public int upgradePoints = 0;
    public TMP_Text upgradePointsText;

    public GameObject bossUI;
    public Image bossBarFill;
    private GameObject _boss;
    private AudioSource _audioSource;
    
    void CreatureHit(GameObject target, GameObject attacker, int damage, DamageTypes damageType)
    {
        GameObject indicator = Instantiate(damageIndicator, target.transform.position, Quaternion.identity);
        TMP_Text text = indicator.GetComponentInChildren<TextMeshPro>();
        Rigidbody2D rb = indicator.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 10);
        text.text = damage.ToString();
        
        //if ally hit, make it bold
        if (target.CompareTag("Ally") || target.CompareTag("Player"))
            text.fontStyle = FontStyles.Italic;
        
        switch (damageType)
        {
            case DamageTypes.Physical:
                text.color = Color.red;
                break;
            case DamageTypes.Fire:
                text.color = Color.orange;
                break;
            case DamageTypes.Cold:
                text.color = Color.white;
                break;
            case DamageTypes.Lightning:
                text.color = Color.blue;
                break;
            case DamageTypes.Acid:
                text.color = Color.green;
                break;
        }
    }

    private void Start()
    {
        //initalize allies
        party[0].ally = Instantiate(party[0].ally , player.transform.position, Quaternion.identity);
        party[0].ally.GetComponent<AlliedMoveTo>().allyOffset = new Vector2(3, 0);
        
        party[1].ally = Instantiate(party[1].ally , player.transform.position, Quaternion.identity);
        party[1].ally.GetComponent<AlliedMoveTo>().allyOffset = new Vector2(-3, 0);
        
        party[2].ally = Instantiate(party[2].ally , player.transform.position, Quaternion.identity);
        party[2].ally.GetComponent<AlliedMoveTo>().allyOffset = new Vector2(0, 3);
        
        party[3].ally = Instantiate(party[3].ally , player.transform.position, Quaternion.identity);
        party[3].ally.GetComponent<AlliedMoveTo>().allyOffset = new Vector2(0, -3);
        
        //player data
        player = GameObject.FindGameObjectWithTag("Player");
        _plrHealth = player.GetComponent<Health>();
        _pltAttack = player.GetComponent<PlayerAttack>();
        _waveManager = GameObject.FindGameObjectWithTag("Wave Manager").GetComponent<WaveManager>();
        _plrHealth = player.GetComponent<Health>();

        for (int i = 0; i < party.Length; i++)
        {
            party[i].HealthComponent = party[i].ally.GetComponent<Health>();
            party[i].allyIcon.sprite = party[i].ally.GetComponent<AlliedStatManager>().allyData.allyIcon;
            party[i].allyBar.color = party[i].ally.GetComponent<AlliedStatManager>().allyData.allyColor;
            party[i].AllyData = party[i].ally.GetComponent<AlliedStatManager>().allyData;
            party[i].AlliedAI = party[i].ally.GetComponent<AlliedAI>();
        }
        
        upgradeScreen.SetActive(false);
        
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float healthPercent = (float)_plrHealth.health / _plrHealth.maxHealth;
        healthBar.fillAmount = healthPercent;

        for (int i = 0; i < party.Length; i++)
        {
            float fillpercent = (float)party[i].HealthComponent.health / party[i].HealthComponent.maxHealth;
            party[i].allyBar.fillAmount = fillpercent;
        }

        if (bossUI.activeInHierarchy)
        {
            float bossFill = (float)_boss.GetComponent<Health>().health / _boss.GetComponent<Health>().maxHealth;
            bossBarFill.fillAmount = bossFill;
        }

        waveCounter.text = _waveManager.currentWaveIndex.ToString();

        float abilityPercent = 1 - Mathf.Min(_pltAttack._attackTimer, _pltAttack.attackSpeed) / _pltAttack.attackSpeed;
        abilityFill.rectTransform.sizeDelta = new Vector2(100, abilityPercent * 100f);
    }

    void EnemyDied(GameObject target, GameObject killer)
    {
        remainingCounter.text = (GameObject.FindGameObjectsWithTag("Enemy").Length - 1).ToString();
    }

    public void TriggerWave()
    {
        nextWaveButton.SetActive(false);
        _waveManager.StartWave();
        upgradeScreen.SetActive(false);

        foreach (var member in party)
        {
            member.HealthComponent.Ressurect();
            member.AlliedAI.RequestState(States.Defending);
        }

        if (_waveManager.currentWaveIndex == 9)
        {
            _boss = _waveManager.boss;
            bossUI.SetActive(true);
        }

        _audioSource.Play();
    }

    public void UpdateHealth(int slot)
    {
        if (upgradePoints > 0)
        {
            //linear upgrade
            party[slot].AllyData.health += 5;
            upgradePoints--;
        }
        
        UpdateStats();
    }
    
    public void UpdateMovementSpeed(int slot)
    {
        if (upgradePoints > 0)
        {
            //linear
            party[slot].AllyData.moveSpeed += 0.5f;
            upgradePoints--;
        }
        
        UpdateStats();
    }
    
    public void UpdateDamage(int slot)
    {
        if (upgradePoints > 0)
        {
            //linear upgrade
            party[slot].AllyData.damage += 1;
            upgradePoints--;
        }
        
        UpdateStats();
    }
    
    public void UpdateAttackSpeed(int slot)
    {
        //dont let it be zero
        if (party[slot].AllyData.attackSpeed - 0.2f <= 0.2f)
        {
            return;
        }

        if (upgradePoints > 0)
        {
            //linear
            party[slot].AllyData.attackSpeed -= 0.2f;
            upgradePoints--;
        }
        
        UpdateStats();
    }
    
    public void UpdateRegen(int slot)
    {
        if (upgradePoints > 0)
        {
            //linear
            party[slot].AllyData.regen += 1;
            upgradePoints--;
        }
        
        UpdateStats();
    }

    void UpdateStats()
    {

        for (int i = 0; i < party.Length; i++)
        {
            party[i].ally.GetComponent<AlliedStatManager>().ApplyStats();
            
            statCounters[i].healthText.text = party[i].AllyData.health.ToString();
            statCounters[i].damageText.text = party[i].AllyData.damage.ToString();
            statCounters[i].regenText.text = party[i].AllyData.regen.ToString();
            statCounters[i].attackSpeedText.text = party[i].AllyData.attackSpeed.ToString("F2");
            statCounters[i].moveSpeedText.text = party[i].AllyData.moveSpeed.ToString("F2");
            statCounters[i].icon.sprite = party[i].AllyData.allyIcon;
            statCounters[i].name.text = party[i].AllyData.name;
            
        }
        
        upgradePointsText.text = upgradePoints.ToString();

        if (upgradePoints == 0)
        {
            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                upgradeButtons[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                upgradeButtons[i].interactable = true;
            }
        }
        
    }

    void WaveEnded()
    {
        UpdateStats();
        upgradeScreen.SetActive(true);
        nextWaveButton.SetActive(true);
        upgradePoints += 4;
        upgradePointsText.text = upgradePoints.ToString();
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].interactable = true;
        }
    }

    public void ResetGame()
    {
        
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
        
    }
    
    void OnEnable()
    {
        EventManager.OnCreatureHit += CreatureHit;
        EventManager.OnEnemyDeath += EnemyDied;
        EventManager.OnWaveEnd += WaveEnded;
    }

    void OnDisable()
    {
        EventManager.OnCreatureHit -= CreatureHit;
        EventManager.OnEnemyDeath -= EnemyDied;
        EventManager.OnWaveEnd -= WaveEnded;
    }
}

[System.Serializable]
public class StatPanel
{
    public Image icon;
    public TMP_Text name;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public TMP_Text regenText;
    public TMP_Text attackSpeedText;
    public TMP_Text moveSpeedText;
}

[System.Serializable]
public class AllyDataContainer
{
    public GameObject ally;
    [NonSerialized]
    public AllyData AllyData;
    [NonSerialized]
    public Health HealthComponent;
    [NonSerialized]
    public AlliedAI AlliedAI;
    [Header("UI Elements")] 
    public GameObject HUDcontainer;
    public Image allyIcon;
    public Image allyBar;
}
