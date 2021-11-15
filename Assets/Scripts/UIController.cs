using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour 
{
    Text distanceText;
    Player player;

    GameObject results;
    GameObject distGO;

    Text finalDistanceText; 
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distGO = GameObject.Find("distanceText");
        distanceText = distGO.GetComponent<Text>();


        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<Text>();
        results = GameObject.Find("Results");
        results.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int distance = (int)player.distance;
        distanceText.text = distance + " m";

        if (player.isDead)
        {
            results.SetActive(true);
            distGO.SetActive(false);
            finalDistanceText.text = distance + " pts";
        }
    }

    public void Retry ()
    {
        SceneManager.LoadScene("GameScene");
    }
}
