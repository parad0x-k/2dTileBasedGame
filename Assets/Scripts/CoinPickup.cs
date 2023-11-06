using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSfx;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
