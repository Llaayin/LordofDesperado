using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectButton : MonoBehaviour
{
    public string characterName;

    public void SelectCharacter()
    {
        CharacterSelectionData.SelectedCharacter = characterName;
        SceneManager.LoadScene("Camp");
    }
}