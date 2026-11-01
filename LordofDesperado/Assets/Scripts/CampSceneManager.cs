using UnityEngine;
using UnityEngine.SceneManagement;

public class CampSceneManager : MonoBehaviour
{
    public void StartExpedition()
    {
        SceneManager.LoadScene("Gameplay");
    }
}