using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    GameController gameController;
    Paddle paddle;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        paddle = FindObjectOfType<Paddle>();
    }

    private void Update()
    {
        if (gameController.IsPlaying && gameController.IsPaused)
        {
            if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Escape))
            {
                gameController.UnpauseGame();
            }
        }
        else if (gameController.IsPlaying && !gameController.IsPaused)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
               paddle.Move(Vector2.left);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                paddle.Move(Vector2.right);
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                gameController.PauseGame();
            }
;
        }
    }
}
