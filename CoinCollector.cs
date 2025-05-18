using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinCollector : MonoBehaviour
{
    [SerializeField] AudioClip pickupsound;
    
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other){
        if( other.tag == "Player"){
            AudioSource.PlayClipAtPoint(pickupsound, Camera.main.transform.position);

            Destroy (gameObject);
            FindObjectOfType<GameSession>().CoinScore();

        }
    }
}
