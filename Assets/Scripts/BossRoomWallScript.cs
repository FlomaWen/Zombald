using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomWallScript : MonoBehaviour
{
    [SerializeField] private Golem golem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            golem.wakeUp();
        }
    }
}
