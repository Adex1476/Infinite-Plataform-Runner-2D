using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Player playerData;
    private GameObject EndGameUI;

    [SerializeField]
    private GameObject healthBar;

    
    public bool isPaused = false;

    [SerializeField]
    private GameObject pauseMenu;

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
        healthBar = GameObject.Find("HealthBar");
        healthBar.GetComponent<HealthBar>().SetMaxHealth(playerData.health);
    }


    // Update is called once per frame
    void Update()
    {
        GameObject.Find("HealthBar").GetComponent<HealthBar>().SetHealth(playerData.health);
        PlayerDeath();

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        pauseMenu.SetActive(isPaused);

        Player.GetComponent<Animator>().enabled = !isPaused;



    }

    public void Pause()
    {
        isPaused = !isPaused;
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
