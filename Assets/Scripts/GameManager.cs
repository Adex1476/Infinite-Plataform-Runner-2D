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
    public bool bossDefeated = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private bool settingsActive;
    [SerializeField] private EffectAudioController _effectAudioController;

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

        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !playerData.isDead && !settingsActive)
        {
            Pause();
            if (isPaused)
            {
                _effectAudioController.OnClickSound();
            }else
            {
                _effectAudioController.OnClickBackSound();
            }
        }

        if (!settingsActive)
            pauseMenu.SetActive(isPaused);

        if (!playerData.isDead) 
            Player.GetComponent<Animator>().enabled = !isPaused;


    }

    public void Pause() => isPaused = !isPaused;

    public void Win() => bossDefeated = !bossDefeated;

    public void Settings()
    {
        settingsActive = true;
        settings.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void SettingsNotActive()
    {
        settingsActive = false;
        isPaused = true;
        settings.SetActive(settingsActive);
    }
}