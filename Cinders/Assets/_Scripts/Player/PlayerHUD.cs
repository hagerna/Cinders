using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] TextMeshPro messageDisplay, tooltipDisplay;
    private Queue<string> messageQueue = new Queue<string>();
    public float displayTime = 9f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayQueuedMessages());
        messageDisplay.text = "";
        tooltipDisplay.text = "";
    }

    public void QueueMessage(string message)
    {
        messageQueue.Enqueue(message);
    }

    /// <summary>
    /// Show the given message immediately, clearing the Queue before restarting it with the new message.
    /// </summary>
    /// <param name="message">The message to be displayed immediately on the HUD</param>
    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        messageDisplay.text = "";
        messageQueue.Clear();
        QueueMessage(message);
        StartCoroutine(DisplayQueuedMessages());
    }


    IEnumerator DisplayQueuedMessages()
    {
        if (messageQueue.Count > 0)
        {
            messageDisplay.text = messageQueue.Dequeue();
            yield return new WaitForSeconds(displayTime);
            StartCoroutine(DisplayQueuedMessages());
        }
        else
        {
            messageDisplay.text = "";
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(DisplayQueuedMessages());
        }
    }

    public void ShowTooltip(string message, float startDelay = 0, bool waitToClear = false)
    {
        StartCoroutine(DisplayTooltipMessage(message, startDelay, waitToClear));
    }

    public void ClearTooltip()
    {
        StopCoroutine(nameof(DisplayTooltipMessage));
        tooltipDisplay.text = "";
    }

    IEnumerator DisplayTooltipMessage(string message, float startDelay, bool waitToClear)
    {
        yield return new WaitForSeconds(startDelay);
        tooltipDisplay.text = message;
        if (!waitToClear)
        {
            yield return new WaitForSeconds(displayTime);
        }
    }
}
