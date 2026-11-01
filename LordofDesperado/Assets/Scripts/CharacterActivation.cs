using UnityEngine;

public class CharacterActivation : MonoBehaviour
{
    public GameObject heldBytheilt;
    public GameObject narrator;

    void Awake()
    {
        bool playNarrator =
            CharacterSelectionData.SelectedCharacter == "TheNarrator";

        heldBytheilt.SetActive(!playNarrator);
        narrator.SetActive(playNarrator);
    }
}