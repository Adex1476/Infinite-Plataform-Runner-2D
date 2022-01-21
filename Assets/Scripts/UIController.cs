using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour 
{
    Text distanceText;
    
    Player player;

    GameManager gameManager;

    GameObject results;

    GameObject win;

    GameObject distGO;

    Text finalDistanceText;

    private int distance;
    private void Awake()
    {
        distance = 0;
        
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        distGO = GameObject.Find("distanceText");
        distanceText = distGO.GetComponent<Text>();

        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<Text>();
        results = GameObject.Find("Results");
        win = GameObject.Find("Win");
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

        if (player.isDead)
        {
            results.SetActive(true);
            distGO.SetActive(false);
            finalDistanceText.text = distance + " pts";
        }
    }

    public void EndGame ()
    {
        gameManager.Win();
        win.SetActive(true);
        distGO.SetActive(false);
    }


    public void Retry () => SceneManager.LoadScene("GameScene");
    

    public void Exit () => SceneManager.LoadScene("MainMenu");
}
