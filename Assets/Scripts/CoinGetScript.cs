using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGetScript : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Coin collected!");
            player.AddCoin();
        }
    }
}
