using UnityEngine;
using System.Collections;

public class DoNotDestroy : MonoBehaviour {

	void Awake () {
        Object.DontDestroyOnLoad(gameObject);
	}

}
