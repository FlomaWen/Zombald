using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject entityPrefab;
    [SerializeField] private GameObject entityPrefab2;
    [SerializeField] private float spawnInterval = 1;
    [SerializeField] private int currentRound = 1;
    [SerializeField] private int enemiesPerRound = 5;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI enemyleftText;
    private int enemiesRemainingToSpawn;
    private bool isFirstRound = true;

    private float lastSpawnTime;
    private int spawnCount;
    private int totalEnemiesSpawnedThisRound;
    private int totalEnemiesToSpawn;

    private void Start()
    {
        PrepareNextRound();
    }

    private void Update()
    {
        if (lastSpawnTime >= spawnInterval && enemiesRemainingToSpawn > 0)
        {
            lastSpawnTime = 0;
            spawnCount++;
            enemiesRemainingToSpawn--;
            totalEnemiesSpawnedThisRound++;

            var chosenPrefab = Random.Range(0, 2) == 0 ? entityPrefab : entityPrefab2;
            var instance = Instantiate(chosenPrefab, transform.position, Quaternion.identity);
            var enemyScript = instance.GetComponent<Enemy>();
            if (enemyScript != null) enemyScript.Initialize(this);
        }

        lastSpawnTime += Time.deltaTime;

        if (totalEnemiesSpawnedThisRound >= totalEnemiesToSpawn && spawnCount <= 0) PrepareNextRound();
    }

    private void PrepareNextRound()
    {
        if (!isFirstRound) currentRound++;
        if (isFirstRound) isFirstRound = false;
        totalEnemiesToSpawn = enemiesPerRound * currentRound;
        enemiesRemainingToSpawn = totalEnemiesToSpawn;
        totalEnemiesSpawnedThisRound = 0;
        if (roundText != null) roundText.text = "MANCHE " + currentRound;
        Debug.Log("Manche " + currentRound);
        if (enemyleftText != null) enemyleftText.text = "Ennemis restants: " + spawnCount;
    }

    public void EnemyDestroyed()
    {
        spawnCount--;
        if (enemyleftText != null) enemyleftText.text = "Ennemis restants: " + spawnCount;
        Debug.Log("Ennemi détruit, Restant: " + spawnCount);
    }
}