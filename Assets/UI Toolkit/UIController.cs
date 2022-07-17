using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public Button btnPlay;
    public Button btnQuit;
  

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        btnPlay = root.Q<Button>("btnPlay");
        btnQuit = root.Q<Button>("btnQuit");

      

        btnPlay.clicked += btnPlayPressed;
        btnQuit.clicked += btnQuitPressed;


    }

    

    //loads in game scene and starts game
    void btnPlayPressed() {
        SceneManager.LoadScene("SampleScene");
        SceneManager.UnloadSceneAsync("MainMenu");

    }

    void btnQuitPressed() {

        Application.Quit();
    } 
}
