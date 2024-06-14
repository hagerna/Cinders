using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugVR : MonoBehaviour
{

    [SerializeField] TextMeshPro Display;

    public static DebugVR instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void DebugMessage(string message)
    {
        Display.text += message + "\n";
        Debug.Log("DebugVR: " + message);
    }
}
