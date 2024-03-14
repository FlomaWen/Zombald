using UnityEngine;

public class Golem : MonoBehaviour
{
    // [SerializeField] private float speed = 0.8f;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody body;
    
    public float chaseRange = 1f; // Distance at which the monster starts chasing
    public float attackRange = 2f; // Distance at which the monster attacks
    public float moveSpeed = 5f; // Movement speed of the monster

    private Transform player;
    // private bool isChasing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // isChasing = false;
            animator.SetBool("Run", false);
            animator.SetBool("IdleAction", true);
            AttackPlayer();
        }
        else if (distanceToPlayer <= chaseRange && distanceToPlayer > attackRange)
        {
            // isChasing = true;
            animator.SetBool("Run", true);
            animator.SetBool("IdleAction", false);
            ChasePlayer();
        }
        else
        {
            // isChasing = false;
            animator.SetBool("Run", false);
            animator.SetBool("IdleAction", true);
            // Optionally, stop moving when the player is out of range
        }
    }
    
    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        transform.LookAt(player);
    }

    void AttackPlayer()
    {
        // Attack logic here
        Debug.Log("Attacking the player!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InvisibleWall"))
        {
            animator.SetBool("IdleAction", true);
        }
    }
}