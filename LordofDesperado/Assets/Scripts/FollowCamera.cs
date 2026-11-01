using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -8f);

    void LateUpdate()
    {
        // Check whether the current target is valid.
        if (target == null ||
            !target.gameObject.activeInHierarchy ||
            target.gameObject.name != CharacterSelectionData.SelectedCharacter)
        {
            FindTarget();
        }

        if (target == null)
            return;

        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    void FindTarget()
    {
        // Look for the character selected in the Main Menu.
        GameObject selected = GameObject.Find(
            CharacterSelectionData.SelectedCharacter
        );

        if (selected != null && selected.activeInHierarchy)
        {
            target = selected.transform;
            return;
        }

        // Fallback: find whichever character is active.
        GameObject player = GameObject.Find("Player");
        GameObject narrator = GameObject.Find("TheNarrator");

        if (player != null && player.activeInHierarchy)
            target = player.transform;
        else if (narrator != null && narrator.activeInHierarchy)
            target = narrator.transform;
        else
            target = null;
    }
}