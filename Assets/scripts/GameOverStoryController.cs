using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverStoryController : MonoBehaviour
{
    int index = 0;

    List<string> messages;

    Label label;

    AudioSource deathVoice;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        deathVoice = audioSources[0];

        var root = GetComponent<UIDocument>().rootVisualElement;

        label = root.Q<Label>("message-label");

        messages = new List<string>();
        messages.Add("");
        messages.Add("Better luck next life. - Death");

        ShowNext();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ShowNext()
    {
        string message = messages[index++];

        label.text = message;

        if (message == "")
        {
            Invoke("ShowNext", 2);
            return;
        }

        int delay = Mathf.Max(3, message.Length / 10);
        Debug.Log("delay " + delay);

        deathVoice.Play();
        Invoke("StopVoice", delay - 1);

        if (index == messages.Count)
        {
            Invoke("ReturnToMenu", delay + 2);
            return;
        }

        Invoke("ShowNext", delay);
    }

    void StopVoice()
    {
        deathVoice.Stop();
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene("menu");
        SceneManager.UnloadSceneAsync("game_over");
    }
}
