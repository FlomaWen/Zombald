using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeScript : MonoBehaviour
{
    [SerializeField] Player _player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _player.targetEnemy = other.GetComponent<Enemy>();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _player.targetEnemy = null;
        }
    }
}
