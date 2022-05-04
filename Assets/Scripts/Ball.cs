using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject lastObjectHit;
    CircleCollider2D circleCollider;
    
    public Vector2 Velocity = new Vector2(4, 4);

    GameController gameController;

    public AudioClip OnWallHitAudio;
    public AudioClip OnPaddleHitAudio;
    public AudioClip OnRespawn;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        gameController = FindObjectOfType<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Velocity * Time.deltaTime);
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, circleCollider.radius, Velocity, (Velocity * Time.deltaTime).magnitude);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != circleCollider && hit.transform.gameObject != lastObjectHit)
            {
                // New respawn method based on killzones
                if (hit.transform.GetComponent<Killzone>())
                {
                    gameController.BallLost();
                    Instantiate(gameController.BallVFX, transform.position, Quaternion.identity);
                    gameController.AudioController.PlayClip(OnRespawn);
                }
                else
                {
                    lastObjectHit = hit.transform.gameObject;
                    Velocity = Vector2.Reflect(Velocity, hit.normal);
                    if (hit.transform.GetComponent<Paddle>())
                    {
                        Velocity.y = Mathf.Abs(Velocity.y);
                        gameController.AudioController.PlayClip(OnPaddleHitAudio);
                    }
                    if (hit.transform.GetComponent<Block>())
                    {
                        hit.transform.GetComponent<Block>().OnHit();
                    }
                    gameController.AudioController.PlayClip(OnWallHitAudio);
                }
            }
            // Old respawn method based on camera
            //    if (transform.position.y < -Camera.main.orthographicSize)
            //{
            //    gameController.BallLost();
            //    Instantiate(gameController.BallVFX, transform.position, Quaternion.identity);
            //    gameController.AudioController.PlayClip(OnRespawn);
        }
    }
}
