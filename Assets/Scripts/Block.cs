using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Hits = 1;
    public int ScoreValue = 100;

    GameController gameController;
    public AudioClip OnBreakAudio;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void OnHit()
    {
        Hits--;

        if (Hits <= 0)
        {
            gameController.AddScore(ScoreValue);
            gameController.AudioController.PlayClip(OnBreakAudio);
            Instantiate(gameController.ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); 
        }
    }
}
