using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float changeDirectionTime = 2f;
    private Vector3 direction;

    private Spawner mySpawner;
    private float timer;
    
    public GameObject coinPrefab;

    public void Start()
    {
        Animator animator = GetComponent<Animator>();
        ChangeDirection();
        animator.SetBool("IsMooving", true);
    }

    private void Update()
    {
        Animator animator = GetComponent<Animator>();
        timer += Time.deltaTime;

        if (timer >= changeDirectionTime)
        {
            if (animator != null)
            {
                animator.SetBool("IsMooving", true);
            }
            ChangeDirection();
            timer = 0f;
        }

        transform.position += direction * (speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        float randomX = UnityEngine.Random.Range(-1f, 1f);
        float randomZ = UnityEngine.Random.Range(-1f, 1f);
        direction = new Vector3(randomX, 0f, randomZ).normalized;
        transform.LookAt(transform.position + direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null && player.isAttacking)
        {
            mySpawner.EnemyDestroyed();

            OnHit?.Invoke(this);
            DropCoin();
            Destroy(gameObject);
        }
    }
    
    void DropCoin()
    {
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }

    public event Action<Enemy> OnHit;

    public void Initialize(Spawner spawner)
    {
        mySpawner = spawner;
    }
    
}