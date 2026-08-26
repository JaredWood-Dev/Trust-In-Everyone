using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private Health _plrHealth;
    private PlayerAttack _pltAttack;
    
    [Header("Ally Slots")]
    public GameObject slot1;
    private Health _1Health;
    public GameObject slot2;
    private Health _2Health;
    public GameObject slot3;
    private Health _3Health;
    public GameObject slot4;
    private Health _4Health;

    public GameObject damageIndicator;

    [Header("UI Elements")]
    public Image healthBar;
    public GameObject ally1Container;
    public Color barColor1;
    private Image _bar1Image;
    public GameObject ally2Container;
    public Color barColor2;
    private Image _bar2Image;
    public GameObject ally3Container;
    public Color barColor3;
    private Image _bar3Image;
    public GameObject ally4Container;
    public Color barColor4;
    private Image _bar4Image;

    public TMP_Text waveCounter;
    public TMP_Text remainingCounter;
    public GameObject nextWaveButton;
    private WaveManager _waveManager;
    public Image abilityFill;
    
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
        
        //assign bar colors
        _bar1Image.color = barColor1;
        _bar2Image.color = barColor2;
        _bar3Image.color = barColor3;
        _bar4Image.color = barColor4;
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
        if (_waveManager.enemyCount <= 1)
        {
            nextWaveButton.SetActive(true);
        }
    }

    public void TriggerWave()
    {
        nextWaveButton.SetActive(false);
        _waveManager.StartWave();
    }
    
    void OnEnable()
    {
        EventManager.OnCreatureHit += CreatureHit;
        EventManager.OnEnemyDeath += EnemyDied;
    }

    void OnDisable()
    {
        EventManager.OnCreatureHit -= CreatureHit;
        EventManager.OnEnemyDeath -= EnemyDied;
    }
    
}
