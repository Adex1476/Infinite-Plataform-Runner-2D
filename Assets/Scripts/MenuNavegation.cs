using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavegation : MonoBehaviour
{
    [SerializeField]
    private GameObject[] canvasMenus;

    [SerializeField]
    private Button[] buttonsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buttonsMenu[0].onClick.AddListener(LoadGame);
        buttonsMenu[1].onClick.AddListener(LoadShop);
        buttonsMenu[2].onClick.AddListener(LoadGarage);
        buttonsMenu[3].onClick.AddListener(LoadSettings);
        //buttonsMenu[4].onClick.AddListener(LoadGame);
    }

    private void LoadMenu() => canvasMenus[0].SetActive(true);
    private void UnloadMenu() => canvasMenus[0].SetActive(false);
    private void LoadGame() => SceneManager.LoadScene(1);
    private void LoadShop() { UnloadMenu(); canvasMenus[1].SetActive(true); }
    private void LoadGarage() { UnloadMenu(); canvasMenus[2].SetActive(true); }
    private void LoadSettings() { UnloadMenu(); canvasMenus[3].SetActive(true); }
    private void LoadArchivements() { }
}
