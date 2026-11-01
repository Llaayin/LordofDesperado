
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ObjectiveInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public GameObject interactionPrompt;

    private Transform player;
    private bool objectiveCompleted = false;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        GameObject narratorObject = GameObject.Find("TheNarrator");

        if (playerObject != null && playerObject.activeInHierarchy)
        {
            player = playerObject.transform;
        }
        else if (narratorObject != null && narratorObject.activeInHierarchy)
        {
            player = narratorObject.transform;
        }
        else
        {
            Debug.LogError("No active playable character found!");
        }

        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null || objectiveCompleted)
            return;

        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        bool isInRange = distance <= interactionRange;

        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(isInRange);
        }

        if (isInRange &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            objectiveCompleted = true;
            SceneManager.LoadScene("Victory");
        }
    }
}
