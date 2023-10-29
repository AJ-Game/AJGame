using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingTerrain : MonoBehaviour
{

    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            PlayerController player = other.GetComponent<PlayerController>();
            if(player != null){
                player.Health -= damage;
            }
        }
    }
}
