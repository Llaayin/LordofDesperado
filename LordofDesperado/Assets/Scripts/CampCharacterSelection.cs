
using UnityEngine;
using TMPro;

public class CampCharacterSelection : MonoBehaviour
{
    [Header("Character Objects")]
    public GameObject heldBytheiltCharacter;
    public GameObject narratorCharacter;

    [Header("UI")]
    public TMP_Text selectedCharacterLabel;

    void Start()
    {
        ApplySelectedCharacter();
    }

    public void SelectHeldBytheilt()
    {
        CharacterSelectionData.SelectedCharacter = "Player";
        ApplySelectedCharacter();

        Debug.Log("Selected character: Held Bytheilt");
    }

    public void SelectNarrator()
    {
        CharacterSelectionData.SelectedCharacter = "TheNarrator";
        ApplySelectedCharacter();

        Debug.Log("Selected character: The Narrator");
    }

    void ApplySelectedCharacter()
    {
        bool narratorSelected =
            CharacterSelectionData.SelectedCharacter == "TheNarrator";

        if (heldBytheiltCharacter != null)
            heldBytheiltCharacter.SetActive(!narratorSelected);

        if (narratorCharacter != null)
            narratorCharacter.SetActive(narratorSelected);

        UpdateSelectedCharacterLabel();
    }

    void UpdateSelectedCharacterLabel()
    {
        if (selectedCharacterLabel == null)
            return;

        if (CharacterSelectionData.SelectedCharacter == "TheNarrator")
            selectedCharacterLabel.text = "SELECTED: THE NARRATOR";
        else
            selectedCharacterLabel.text = "SELECTED: HELD BYTHEILT";
    }
}
