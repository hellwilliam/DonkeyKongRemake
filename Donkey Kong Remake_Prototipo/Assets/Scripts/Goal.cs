using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            CharacterMovement player = collider.gameObject.GetComponent<CharacterMovement>();
            player.Win();
        }
    }
}
