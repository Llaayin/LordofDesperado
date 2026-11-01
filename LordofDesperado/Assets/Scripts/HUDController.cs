using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TMP_Text hpLabel;
    public TMP_Text desperadoLabel;

    private CharacterHealth activeHealth;

    void Update()
    {
        FindActiveCharacter();

        if (activeHealth == null)
            return;

        int hp = activeHealth.currentHealth;
        int maxHp = activeHealth.maxHealth;

        hpLabel.text = "HP: " + hp + " / " + maxHp;

        int desperado = activeHealth.desperadoMeter;

        desperadoLabel.text = "DESPERADO\n" +
                              desperado + " / 100";
    }

    void FindActiveCharacter()
    {
        GameObject player = GameObject.Find("Player");
        GameObject narrator = GameObject.Find("TheNarrator");

        CharacterHealth foundHealth = null;

        if (player != null && player.activeInHierarchy)
            foundHealth = player.GetComponent<CharacterHealth>();
        else if (narrator != null && narrator.activeInHierarchy)
            foundHealth = narrator.GetComponent<CharacterHealth>();

        activeHealth = foundHealth;
    }
}