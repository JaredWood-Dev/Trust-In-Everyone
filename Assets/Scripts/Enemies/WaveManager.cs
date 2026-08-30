using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public float timeToNextEnemy = 0.5f;
    public int currentWaveIndex = 0;
    public int enemyCount;
    public Dialogue wave1Dialogue;
    public Dialogue wave2Dialogue;
    public Dialogue wave5Dialogue;
    public Dialogue wave10Dialogue;
    public Dialogue postBossDialogue;
    
    public GameObject boss;

    [Header("Spawning")] 
    public Vector2[] spawnPositions;

    void Update()
    {
        
    }

    public IEnumerator SpawnWave()
    {
        
        if (currentWaveIndex < waves.Length)
        {

            enemyCount = waves[currentWaveIndex].enemies.Length;

            foreach (EnemyAI enemy in waves[currentWaveIndex].enemies)
            {
                Vector2 randomSpawn = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)];
                GameObject e = Instantiate(enemy.gameObject, randomSpawn, Quaternion.identity);

                yield return new WaitForSeconds(timeToNextEnemy);
            }
        }

        currentWaveIndex++;
    }

    public void StartWave()
    {
        if (currentWaveIndex == 0)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(wave1Dialogue);
        }
        
        if (currentWaveIndex == 1)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(wave2Dialogue);
        }
        
        if (currentWaveIndex == 4)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(wave5Dialogue);
        }

        if (currentWaveIndex == 8)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(wave10Dialogue);
            boss = Instantiate(boss, new Vector3(0, 0, 0), Quaternion.identity);
            currentWaveIndex++;
        }
        
        if (currentWaveIndex < waves.Length)
            StartCoroutine(SpawnWave());
    }

    public void EnemyKilled(GameObject target, GameObject killer)
    {
        enemyCount--;
        print("printing enemeies:");
        var e = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var en in e)
        {
            print(en.name);
        }
        print("end printing enemeis");
        if (GameObject.FindGameObjectsWithTag("Enemy").Length - 1 == 0)
        {
            EventManager.WaveEnd();
        }
        
    }

    void BossDied()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(postBossDialogue);
    }

    private void OnEnable()
    {
        EventManager.OnEnemyDeath += EnemyKilled;
        EventManager.OnBossDied += BossDied;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyDeath -= EnemyKilled;
        EventManager.OnBossDied -= BossDied;
    }

}

[System.Serializable]
public class Wave
{
    public EnemyAI[] enemies;
}
