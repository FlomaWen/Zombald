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
            Debug.Log("ChangeDirection");
            if (animator != null)
            {
                animator.SetBool("IsMooving", true);
            }
            ChangeDirection();
            //animator.SetBool("IsMooving", false);
            timer = 0f;
        }

        transform.position += direction * (speed * Time.deltaTime);
        transform.LookAt(transform.position + direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null && player.isAttacking)
        {
            mySpawner.EnemyDestroyed();

            OnHit?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public event Action<Enemy> OnHit;

    public void Initialize(Spawner spawner)
    {
        mySpawner = spawner;
    }

    private void ChangeDirection()
    {
        var randomX = Random.Range(-1f, 1f);
        var randomZ = Random.Range(-1f, 1f);
        direction = new Vector3(randomX, 0f, randomZ).normalized;
    }
}