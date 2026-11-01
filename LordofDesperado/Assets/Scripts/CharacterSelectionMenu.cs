using UnityEngine;

public class CharacterSelectionMenu : MonoBehaviour
{
    public GameObject characterSelectPanel;
    public GameObject startButton;
    public GameObject quitButton;

    public void OpenSelection()
    {
        startButton.SetActive(false);
        quitButton.SetActive(false);

        characterSelectPanel.SetActive(true);
    }

    public void CloseSelection()
    {
        characterSelectPanel.SetActive(false);

        startButton.SetActive(true);
        quitButton.SetActive(true);
    }
}