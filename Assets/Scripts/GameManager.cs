using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private GameObject Player;
    private Player playerData;
    [SerializeField]
    private GameObject EndGameUI;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
    }

    void Start()
    {
        Player = GameObject.Find("Player");
        playerData = playerData.GetComponent<Player>();
    }


    // Update is called once per frame
    void Update()
    {
        PlayerDeath();
    }

    private void PlayerDeath()
    {
        /*if ((playerData.health <= 0) || (Player.GetComponent<Transform>().position.y < Player.GetComponent<Player>().playerCamera.transform.position.y - 18))
        {
            EndGameUI.SetActive(true);
            Player.GetComponent<Player>().isDead = true;
        }*/

    }
}
