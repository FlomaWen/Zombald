using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    // Intervalle de temps avant de changer de direction
    [SerializeField] private float changeDirectionTime = 2f; 
    private Vector3 direction;
    private float timer;

    private Spawner mySpawner;

    public event Action<Enemy> OnHit;

    public void Start()
    {
        // Commence avec une direction aléatoire
        ChangeDirection();
    }

    public void Initialize(Spawner spawner)
    {
        mySpawner = spawner;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Change de direction à intervalles réguliers
        if (timer >= changeDirectionTime)
        {
            ChangeDirection();
            timer = 0f;
        }

        // Déplace l'ennemi dans la direction actuelle
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        // Génère une nouvelle direction aléatoire sur le plan horizontal
        float randomX = UnityEngine.Random.Range(-1f, 1f);
        float randomZ = UnityEngine.Random.Range(-1f, 1f);
        direction = new Vector3(randomX, 0f, randomZ).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && player.isAttacking) 
        {
            // Le joueur a touché l'ennemi et le détruit
            mySpawner.EnemyDestroyed();

            OnHit?.Invoke(this);
            Destroy(gameObject);
        }
    }
}