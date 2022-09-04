using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Movement movement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Obstacle"))
        {
            gameController.TextHeart();
            movement.MoveSpeedReset();
            if (gameController.HeartCount == 0)
            {
                gameController.GameOver(movement.transform.position.z);
            }
        }
        // else if (other.tag.Equals("Coin"))
        // {
        //     gameController.IncreaseCoinCount();
        // }
    }
}
