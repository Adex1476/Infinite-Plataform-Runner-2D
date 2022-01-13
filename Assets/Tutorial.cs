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
    Text tutorialText;

    TutorialFase tutorialFase;
    // Start is called before the first frame update
    void Start()
    {
        tutorialText = GetComponent<Text>();
        tutorialFase = TutorialFase.Start;
    }

    // Update is called once per frame
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
                }
                break;
            case TutorialFase.Shoot:
                tutorialText.text = "Click the Screen to Shoot";
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
