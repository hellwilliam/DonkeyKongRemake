using UnityEngine;
using System.Collections;

public class SendButton : MonoBehaviour {
    public Chat2 chat;

    public void Send()
    {
        chat.SendMessage();
    }
}
