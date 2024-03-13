using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] private float speed = 0.8f;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody body;
    [SerializeField] private float changeDirectionTime = 8f; 
    private Vector3 direction;
    private float timer;

    private void Start()
    {
        ChangeDirection();
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

        // Déplace le monstre dans la direction actuelle
        body.velocity = direction * speed;

        // Obtient la rotation vers la direction de déplacement
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        
        // Tourne le monstre vers la direction de déplacement
        body.MoveRotation(targetRotation);

        // Joue l'animation de marche
        animator.SetFloat("Walk",0.2f);
    }

    private void ChangeDirection()
    {
        // Génère une nouvelle direction aléatoire sur le plan horizontal
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        direction = new Vector3(randomX, 0f, randomZ).normalized;
    }
}