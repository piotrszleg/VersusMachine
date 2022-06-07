using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Text))]
public class TextScaler : MonoBehaviour {

    public float percentSize=0.01f;

	void Update()
    {
        GetComponent<Text>().fontSize = (int)(Screen.width * percentSize);
    }
}
