using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TutorialFase {
    Start,
    Jump,
    Shoot
}

public class Tutorial : MonoBehaviour
{
    TutorialFase tutorialFase;
    Text tutorialText;
    float tutorialTime;
    float tutorialStartTime;


    void Start()
    {
        tutorialText = GetComponent<Text>();
        tutorialFase = TutorialFase.Start;
    }


    void Update()
    {

        switch (tutorialFase)
        {
            case TutorialFase.Start:
                tutorialText.text = "Press Space to Start";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    tutorialFase = TutorialFase.Jump;
                }
                break;
            case TutorialFase.Jump:
                tutorialText.text = "Press Space to Jump";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    tutorialFase = TutorialFase.Shoot;
                    tutorialStartTime = Time.time;
                }
                break;
            case TutorialFase.Shoot:
                tutorialText.text = "Click the Screen to Shoot";
                tutorialTime = Time.time - tutorialStartTime;
                if (Input.GetKey(KeyCode.Mouse0) || tutorialTime > 10)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
