
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CampInteractor : MonoBehaviour
{
    [Header("Interaction Settings")]
    public CampFacility[] facilities;
    public float interactionRange = 3.5f;

    [Header("UI References")]
    public GameObject interactionPrompt;
    public TMP_Text promptText;

    [Header("Regular Facility Screen")]
    public GameObject facilityPanel;
    public TMP_Text facilityTitle;
    public TMP_Text facilityDescription;

    [Header("Tent Screen")]
    public GameObject tentPanel;
    public GameObject startExpeditionButton;

    private CampFacility nearbyFacility;
    private bool panelOpen;

    void Start()
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);

        if (facilityPanel != null)
            facilityPanel.SetActive(false);

        if (tentPanel != null)
            tentPanel.SetActive(false);

        if (startExpeditionButton != null)
            startExpeditionButton.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (panelOpen)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame ||
                Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ClosePanel();
            }

            return;
        }

        FindNearestFacility();

        if (nearbyFacility != null)
        {
            if (interactionPrompt != null)
                interactionPrompt.SetActive(true);

            if (promptText != null)
                promptText.text =
                    "Press E to interact with " +
                    nearbyFacility.facilityName;

            if (Keyboard.current.eKey.wasPressedThisFrame)
                OpenPanel(nearbyFacility);
        }
        else
        {
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
    }

    void FindNearestFacility()
    {
        nearbyFacility = null;
        float closestDistance = interactionRange;

        foreach (CampFacility facility in facilities)
        {
            if (facility == null ||
                !facility.gameObject.activeInHierarchy)
                continue;

            Vector3 direction =
                facility.transform.position - transform.position;

            direction.y = 0f;

            float distance = direction.magnitude;

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                nearbyFacility = facility;
            }
        }
    }

    void OpenPanel(CampFacility facility)
    {
        bool isTent = facility.facilityName == "TENT";

        if (facilityPanel != null)
            facilityPanel.SetActive(!isTent);

        if (tentPanel != null)
            tentPanel.SetActive(isTent);

        if (startExpeditionButton != null)
            startExpeditionButton.SetActive(isTent);

        if (!isTent)
        {
            if (facilityTitle != null)
                facilityTitle.text = facility.facilityName;

            if (facilityDescription != null)
                facilityDescription.text = facility.description;
        }

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);

        panelOpen = true;
    }

    void ClosePanel()
    {
        if (facilityPanel != null)
            facilityPanel.SetActive(false);

        if (tentPanel != null)
            tentPanel.SetActive(false);

        if (startExpeditionButton != null)
            startExpeditionButton.SetActive(false);

        panelOpen = false;
    }
}
