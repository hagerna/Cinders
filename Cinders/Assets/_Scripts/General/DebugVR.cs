using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugVR : MonoBehaviour
{

    TextMeshPro Display;
    // Start is called before the first frame update
    void Start()
    {
        Display = GetComponent<TextMeshPro>();
    }

    public void Debug(string message)
    {
        Display.text += message + "\n";
    }
}
