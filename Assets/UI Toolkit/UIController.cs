using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button btnPlay;

    public Button btnQuit;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        btnPlay = root.Q<Button>("play-button");
        btnQuit = root.Q<Button>("quit-button");

        btnPlay.clicked += btnPlayPressed;
        btnQuit.clicked += btnQuitPressed;
    }

    //loads in game scene and starts game
    void btnPlayPressed()
    {
        SceneManager.LoadScene("SampleScene");
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    void btnQuitPressed()
    {
        Application.Quit();
    }
}
