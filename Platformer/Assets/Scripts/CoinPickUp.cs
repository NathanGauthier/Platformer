using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public AudioClip coinPickUpClip;

    bool wasCollected = false; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddScore();
            AudioSource.PlayClipAtPoint(coinPickUpClip,Camera.main.transform.position);
            Destroy(gameObject);
            
        }
    }
}
