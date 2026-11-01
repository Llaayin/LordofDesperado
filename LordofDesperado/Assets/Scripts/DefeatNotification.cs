using UnityEngine;
using System.Collections;

public class DefeatNotification : MonoBehaviour
{
    public GameObject notificationPanel;
    public float displayDuration = 3f;

    private Coroutine hideCoroutine;

    void Awake()
    {
        if (notificationPanel != null)
            notificationPanel.SetActive(false);
    }

    public void Show()
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError("DefeatNotification is inactive in the Hierarchy!");
            return;
        }

        if (notificationPanel == null)
        {
            Debug.LogError("NotificationPanel has not been assigned!");
            return;
        }

        notificationPanel.SetActive(true);

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        notificationPanel.SetActive(false);
        hideCoroutine = null;
    }
}