using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorCollider : Interactable
{
    public GameObject Player;
    public SceneController sceneController;

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collide");
        Debug.Log(collision.gameObject);
        Debug.Log(Player);
        if (collision.gameObject == Player)
        {
            sceneController.endGame = true;
        }
    }
}
