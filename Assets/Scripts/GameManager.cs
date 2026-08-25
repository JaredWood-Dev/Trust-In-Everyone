using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    
    [Header("Ally Slots")]
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;

    public GameObject damageIndicator;
    
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
    
    
    void OnEnable()
    {
        EventManager.OnCreatureHit += CreatureHit;    
    }

    void OnDisable()
    {
        EventManager.OnCreatureHit -= CreatureHit;
    }
    
}
