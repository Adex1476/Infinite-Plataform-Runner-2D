using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] Text distanceText;

    [SerializeField] Player player;

    [SerializeField] GameManager gameManager;

    [SerializeField] GameObject results;

    [SerializeField] GameObject win;

    [SerializeField] GameObject distGO;

    [SerializeField] Text finalDistanceText;

    private int distance;
    private void Awake()
    {
        distance = 0;
        
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        distGO = GameObject.Find("distanceText");
        distanceText = distGO.GetComponent<Text>();

        results.SetActive(false);
        win.SetActive(false);
    }
    
    
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        distance = (int)player.distance;
        distanceText.text = distance + " m";

        if (player.isDead && !player.isDone)
        {
            results.SetActive(true);
            distGO.SetActive(false);
            finalDistanceText.text = distance + " pts";
        }
    }

    public void EndGame ()
    {
        StartCoroutine(EndGameDelay());
    }

    private IEnumerator EndGameDelay()
    {
        yield return new WaitForSeconds(2f);
        gameManager.Win();
        win.SetActive(true);
        distGO.SetActive(false);
        player.isDead = true;
    }

    public void Retry () => SceneManager.LoadScene("GameScene");
    

    public void Exit () => SceneManager.LoadScene("MainMenu");
}
