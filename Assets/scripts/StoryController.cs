using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StoryController : MonoBehaviour
{
    int index = 0;

    List<string> messages;

    Label label;

    int state = -1;

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
        messages
            .Add("It's with a heavy heart that I must inform you that you have died.");
        messages
            .Add("The how or the why doesn't matter, you cannot change what has happened.");
        messages.Add("But I, on the other hand, can.");
        messages
            .Add("But you must best me in a game. Any game of your choosing.");
        messages.Add("Dungeons & Dragons? Very well.");
        messages
            .Add("I must warn you, my Dice seem to have a mind of their own. - Death");

        ShowNext();
    }

    // Update is called once per frame
    void Update()
    {
        // // fade in
        // if (state == -1)
        // {
        //     label.alpha += Time.deltaTime * 1.0f;
        // }
        // if(label.alpha >= 1) {
        //     state = 0;
        // }
        // // fade out
        // if (state == 1)
        // {
        //     label.alpha -= Time.deltaTime * 1.0f;
        // }
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
            Invoke("EnterWorld", delay + 2);
            return;
        }

        Invoke("ShowNext", delay);
    }

    void StopVoice()
    {
        deathVoice.Stop();
    }

    void EnterWorld()
    {
        SceneManager.LoadScene("SampleScene");
        SceneManager.UnloadSceneAsync("story");
    }
}
