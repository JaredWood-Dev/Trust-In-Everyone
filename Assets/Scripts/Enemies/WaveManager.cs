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

    [Header("Spawning")] 
    public Vector2[] spawnPositions;

    void Update()
    {
        
    }

    public IEnumerator SpawnWave()
    {
        enemyCount = waves[currentWaveIndex].enemies.Length;
        
        foreach (EnemyAI enemy in waves[currentWaveIndex].enemies)
        {
            Vector2 randomSpawn = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)];
            GameObject e = Instantiate(enemy.gameObject, randomSpawn, Quaternion.identity);
            
            yield return new WaitForSeconds(timeToNextEnemy);
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
        
        if (currentWaveIndex == 3)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(wave5Dialogue);
        }
        
        if (currentWaveIndex < waves.Length)
            StartCoroutine(SpawnWave());
    }

    public void EnemyKilled(GameObject target, GameObject killer)
    {
        enemyCount--;
        print("enemies: " + GameObject.FindGameObjectsWithTag("Enemy").Length);
        if (GameObject.FindGameObjectsWithTag("Enemy").Length - 1 == 0)
        {
            EventManager.WaveEnd();
        }
    }

    private void OnEnable()
    {
        EventManager.OnEnemyDeath += EnemyKilled;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyDeath -= EnemyKilled;
    }
}

[System.Serializable]
public class Wave
{
    public EnemyAI[] enemies;
}
