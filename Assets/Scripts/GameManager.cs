using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private Health _plrHealth;
    private PlayerAttack _pltAttack;
    
    //todo: replace with proper data structure
    [Header("Ally Slots")]
    public GameObject slot1;
    private Health _1Health;
    private AllyData _1data;
    
    public GameObject slot2;
    private Health _2Health;
    private AllyData _2data;
    
    public GameObject slot3;
    private Health _3Health;
    private AllyData _3data;
    
    public GameObject slot4;
    private Health _4Health;
    private AllyData _4data;

    public GameObject damageIndicator;

    [Header("UI Elements")]
    public Image healthBar;
    
    public GameObject ally1Container;
    private Image _ally1Image;
    private Image _bar1Image;
    
    public GameObject ally2Container;
    private Image _ally2Image;
    private Image _bar2Image;
    
    public GameObject ally3Container;
    private Image _ally3Image;
    private Image _bar3Image;
    
    public GameObject ally4Container;
    private Image _ally4Image;
    private Image _bar4Image;

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
        slot1 = Instantiate(slot1, player.transform.position, Quaternion.identity);
        slot1.GetComponent<AlliedMoveTo>().allyOffset = new Vector2(3, 0);
        slot2 = Instantiate(slot2, player.transform.position, Quaternion.identity);
        slot2.GetComponent<AlliedMoveTo>().allyOffset = new Vector2(-3, 0);
        slot3 = Instantiate(slot3, player.transform.position, Quaternion.identity);
        slot3.GetComponent<AlliedMoveTo>().allyOffset = new Vector2(0, 3);
        slot4 = Instantiate(slot4, player.transform.position, Quaternion.identity);
        slot4.GetComponent<AlliedMoveTo>().allyOffset = new Vector2(0, -3);
        
        player = GameObject.FindGameObjectWithTag("Player");
        _plrHealth = player.GetComponent<Health>();
        _pltAttack = player.GetComponent<PlayerAttack>();
        _waveManager = GameObject.FindGameObjectWithTag("Wave Manager").GetComponent<WaveManager>();
        
        _plrHealth = player.GetComponent<Health>();
        _1Health = slot1.GetComponent<Health>();
        _2Health = slot2.GetComponent<Health>();
        _3Health = slot3.GetComponent<Health>();
        _4Health = slot4.GetComponent<Health>();
        
        //assign bar image refernces
        _bar1Image = ally1Container.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        _bar2Image = ally2Container.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        _bar3Image = ally3Container.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        _bar4Image = ally4Container.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        
        _ally1Image = ally1Container.transform.GetChild(0).GetComponent<Image>();
        _ally2Image = ally2Container.transform.GetChild(0).GetComponent<Image>();
        _ally3Image = ally3Container.transform.GetChild(0).GetComponent<Image>();
        _ally4Image = ally4Container.transform.GetChild(0).GetComponent<Image>();
        
        _ally1Image.sprite = slot1.GetComponent<AlliedStatManager>().allyData.allyIcon;
        _ally2Image.sprite = slot2.GetComponent<AlliedStatManager>().allyData.allyIcon;
        _ally3Image.sprite = slot3.GetComponent<AlliedStatManager>().allyData.allyIcon;
        _ally4Image.sprite = slot4.GetComponent<AlliedStatManager>().allyData.allyIcon;
        
        //assign bar colors
        _bar1Image.color = slot1.GetComponent<AlliedStatManager>().allyData.allyColor;
        _bar2Image.color = slot2.GetComponent<AlliedStatManager>().allyData.allyColor;
        _bar3Image.color = slot3.GetComponent<AlliedStatManager>().allyData.allyColor;
        _bar4Image.color = slot4.GetComponent<AlliedStatManager>().allyData.allyColor;
        
        //get ally data reference
        _1data = slot1.GetComponent<AlliedStatManager>().allyData;
        _2data = slot2.GetComponent<AlliedStatManager>().allyData;
        _3data = slot3.GetComponent<AlliedStatManager>().allyData;
        _4data = slot4.GetComponent<AlliedStatManager>().allyData;
        
        upgradeScreen.SetActive(false);
    }

    void Update()
    {
        float healthPercent = (float)_plrHealth.health / _plrHealth.maxHealth;
        healthBar.fillAmount = healthPercent;
        
        float ally1percent = (float)_1Health.health / _1Health.maxHealth;
        _bar1Image.fillAmount = ally1percent;
        float ally2percent = (float)_2Health.health / _2Health.maxHealth;
        _bar2Image.fillAmount = ally2percent;
        float ally3percent = (float)_3Health.health / _3Health.maxHealth;
        _bar3Image.fillAmount = ally3percent;
        float ally4percent = (float)_4Health.health / _4Health.maxHealth;
        _bar4Image.fillAmount = ally4percent;

        waveCounter.text = _waveManager.currentWaveIndex.ToString();
        remainingCounter.text = _waveManager.enemyCount.ToString();

        float abilityPercent = 1 - Mathf.Min(_pltAttack._attackTimer, _pltAttack.attackSpeed) / _pltAttack.attackSpeed;
        abilityFill.rectTransform.sizeDelta = new Vector2(100, abilityPercent * 100f);
    }

    void EnemyDied(GameObject target, GameObject killer)
    {
        
    }

    public void TriggerWave()
    {
        nextWaveButton.SetActive(false);
        _waveManager.StartWave();
        upgradeScreen.SetActive(false);
    }

    public void UpdateHealth(int slot)
    {
        switch (slot)
        {
            case 1:
                _1data.health += 1;
                break;
            case 2:
                _2data.health += 1;
                break;
            case 3:
                _3data.health += 1;
                break;
            case 4:
                _4data.health += 1;
                break;
        }
        UpdateStats();
    }
    
    public void UpdateMovementSpeed(int slot)
    {
        if (upgradePoints > 0)
        {
            switch (slot)
            {
                case 1:
                    _1data.moveSpeed += 1;
                    break;
                case 2:
                    _2data.moveSpeed += 1;
                    break;
                case 3:
                    _3data.moveSpeed += 1;
                    break;
                case 4:
                    _4data.moveSpeed += 1;
                    break;
            }
            upgradePoints -= 1;
        }

        UpdateStats();
    }
    
    public void UpdateDamage(int slot)
    {
        if (upgradePoints > 0)
        {
            switch (slot)
            {
                case 1:
                    _1data.damage += 1;
                    break;
                case 2:
                    _2data.damage += 1;
                    break;
                case 3:
                    _3data.damage += 1;
                    break;
                case 4:
                    _4data.damage += 1;
                    break;
            }
            upgradePoints -= 1;
        }

        UpdateStats();
    }
    
    public void UpdateAttackSpeed(int slot)
    {
        if (upgradePoints > 0)
        {
            switch (slot)
            {
                case 1:
                    _1data.attackSpeed += 1;
                    break;
                case 2:
                    _2data.attackSpeed += 1;
                    break;
                case 3:
                    _3data.attackSpeed += 1;
                    break;
                case 4:
                    _4data.attackSpeed += 1;
                    break;
            }
            upgradePoints -= 1;
        }

        UpdateStats();
    }
    
    public void UpdateRegen(int slot)
    {
        if (upgradePoints > 0)
        {
            switch (slot)
            {
                case 1:
                    _1data.regen += 1;
                    break;
                case 2:
                    _2data.regen += 1;
                    break;
                case 3:
                    _3data.regen += 1;
                    break;
                case 4:
                    _4data.regen += 1;
                    break;
            }
            upgradePoints -= 1;
        }

        UpdateStats();
    }

    void UpdateStats()
    {
        slot1.GetComponent<AlliedStatManager>().ApplyStats();
        slot2.GetComponent<AlliedStatManager>().ApplyStats();
        slot3.GetComponent<AlliedStatManager>().ApplyStats();
        slot4.GetComponent<AlliedStatManager>().ApplyStats();
        
        //slot 1
        statCounters[0].healthText.text = _1data.health.ToString();
        statCounters[0].damageText.text = _1data.damage.ToString();
        statCounters[0].regenText.text = _1data.regen.ToString();
        statCounters[0].attackSpeedText.text = _1data.attackSpeed.ToString();
        statCounters[0].moveSpeedText.text = _1data.moveSpeed.ToString();
        statCounters[0].icon.sprite = _1data.allyIcon;
        statCounters[0].name.text = _1data.name;
        
        //slot 2
        statCounters[1].healthText.text = _2data.health.ToString();
        statCounters[1].damageText.text = _2data.damage.ToString();
        statCounters[1].regenText.text = _2data.regen.ToString();
        statCounters[1].attackSpeedText.text = _2data.attackSpeed.ToString();
        statCounters[1].moveSpeedText.text = _2data.moveSpeed.ToString();
        statCounters[1].icon.sprite = _2data.allyIcon;
        statCounters[1].name.text = _2data.name;
        
        //slot 3
        statCounters[2].healthText.text = _3data.health.ToString();
        statCounters[2].damageText.text = _3data.damage.ToString();
        statCounters[2].regenText.text = _3data.regen.ToString();
        statCounters[2].attackSpeedText.text = _3data.attackSpeed.ToString();
        statCounters[2].moveSpeedText.text = _3data.moveSpeed.ToString();
        statCounters[2].icon.sprite = _3data.allyIcon;
        statCounters[2].name.text = _3data.name;
        
        //slot 4
        statCounters[3].healthText.text = _4data.health.ToString();
        statCounters[3].damageText.text = _4data.damage.ToString();
        statCounters[3].regenText.text = _4data.regen.ToString();
        statCounters[3].attackSpeedText.text = _4data.attackSpeed.ToString();
        statCounters[3].moveSpeedText.text = _4data.moveSpeed.ToString();
        statCounters[3].icon.sprite = _4data.allyIcon;
        statCounters[3].name.text = _4data.name;
        
        upgradePointsText.text = upgradePoints.ToString();
        
    }

    void WaveEnded()
    {
        UpdateStats();
        upgradeScreen.SetActive(true);
        nextWaveButton.SetActive(true);
        upgradePoints += 4;
        upgradePointsText.text = upgradePoints.ToString();
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
