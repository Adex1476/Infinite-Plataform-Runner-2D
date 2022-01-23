using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroPreGame : MonoBehaviour
{
    [SerializeField] GameObject warning;
    [SerializeField] GameObject headphones;
    [SerializeField] float time;
    // Start is called before the first frame update
    void Start()
    {
        warning.SetActive(true);
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;

        if (time > 5)
        {
            warning.SetActive(false);
            headphones.SetActive(true);
        }

        if (time > 10)
        {
            SceneManager.LoadScene(1);
        }
    }
}
