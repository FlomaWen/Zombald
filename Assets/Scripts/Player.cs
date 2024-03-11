using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 200.0f;
    public bool isAttacking = false;
    private Animator animator;
    private Vector3 moveDirection;
    public event Action OnHit;
    private static readonly int Hiting = Animator.StringToHash("hiting");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection.Normalize();
        
        transform.Translate(movementDirection* (moveSpeed * Time.deltaTime),Space.World);

        if(movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation=Quaternion.RotateTowards(transform.rotation,toRotation,rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit();
        }

    }

    public void Hit()
    {
        isAttacking = true;
        animator.SetBool("hiting", true);
    }

    public void StopHit()
    {
        animator.SetBool("hiting",false);
        isAttacking = false; 
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            OnHit?.Invoke();
        }
    }

}

