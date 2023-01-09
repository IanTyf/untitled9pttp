using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public Interaction interaction;
    public bool isStop;
    public int id;
    public int[] letterIndexes;
    public Color letterColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isStop)
            {
                Services.textDisplay.RemoveLetterGroup(id);
                interaction.SetActive(false);
            }
            else
            {
                Services.textDisplay.AddLetterGroup(id, letterIndexes, letterColor);
                interaction.SetActive(true);
            }
        }
    }
}
