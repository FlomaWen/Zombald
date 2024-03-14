using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody body;

    public float chaseRange = 1f;
    public float attackRange = 2f;
    public float moveSpeed = 5f;
    public HealthScript healthScript;

    public bool awake;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!awake) return;
        var distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Hit2", true);
        }
        else if (distanceToPlayer <= chaseRange && distanceToPlayer > attackRange)
        {
            animator.SetBool("Hit2", false);
            animator.SetBool("Run", true);
            animator.SetBool("IdleAction", false);
            ChasePlayer();
        }
        else
        {
            animator.SetBool("Hit2", false);
            animator.SetBool("Run", false);
            animator.SetBool("IdleAction", true);
        }
    }

    public void wakeUp()
    {
        awake = true;
        animator.SetBool("IdleAction", true);
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        transform.LookAt(player);
    }
}