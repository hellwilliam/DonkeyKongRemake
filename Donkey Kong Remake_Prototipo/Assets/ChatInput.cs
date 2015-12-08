using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChatInput : MonoBehaviour {
    public Chat2 chat;
    private InputField input;

    void Start()
    {
        input = GetComponent<InputField>();
    }

    public void UpdateMessage(string message)
    {
        chat.currentMessage = message;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            input.text = input.text.Trim();
            if (!string.IsNullOrEmpty(input.text))
            {
                chat.SendMessage();
            }
            input.text = "";
            input.Select();
        }
    }
}
