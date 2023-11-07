using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDestroyer : MonoBehaviour
{
    public GameManager gameManager;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            if(gameManager.gameOver == false){
                gameManager.NoteMissed();
            }
            Destroy(other.gameObject);
        }
    }
}
