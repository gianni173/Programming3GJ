using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject components = null; 

    private GameManager GameManager;

    private void Start()
    {
        GameManager = GameManager.Instance;
        if(GameManager)
        {
            GameManager.OnMainPlayerSpawned += LinkToMainPlayer;
            components.SetActive(true);
        }
    }

    private void LinkToMainPlayer(Character mainPlayer)
    {
        mainPlayer.Movement.OnLookAt += UpdatePosition;
    }

    private void UpdatePosition(Vector3 newPos)
    {
        transform.position = newPos + Vector3.up * 3;
    }
}
