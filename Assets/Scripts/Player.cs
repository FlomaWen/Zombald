using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int Hiting = Animator.StringToHash("hiting");
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 200.0f;
    public bool isAttacking;
    private Animator _animator;
    private Vector3 _moveDirection;
    public HealthScript healthScript;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Animator animator = GetComponent<Animator>();
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection.Normalize();

        transform.Translate(movementDirection * (moveSpeed * Time.deltaTime), Space.World);

        if (movementDirection != Vector3.zero)
        {
            if (animator != null)
            {
                animator.SetBool("IsMooving", true);
            }

            var toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else {
            animator.SetBool("IsMooving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space)) Hit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthScript.TakeDamage(1);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            healthScript.TakeDamage(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null) OnHit?.Invoke();
    }

    public event Action OnHit;

    public void Hit()
    {
        isAttacking = true;
        _animator.SetBool("hiting", true);
    }

    public void StopHit()
    {
        _animator.SetBool("hiting", false);
        isAttacking = false;
    }
}