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

        if (Input.GetKeyDown(KeyCode.P) && !playerData.isDead)
        {
            Pause();
        }

        pauseMenu.SetActive(isPaused);

        if (!playerData.isDead) 
            Player.GetComponent<Animator>().enabled = !isPaused;

    }

    public void Pause()
    {
        isPaused = !isPaused;
    }
}
