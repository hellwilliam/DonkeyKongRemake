using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour 
{
    public void Quit()
    {
        Debug.Log("I quit");
        Application.Quit();
    }
}
