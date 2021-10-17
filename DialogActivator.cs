using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogActivator : MonoBehaviour, Interactable
{
    public DialogObject dialogObject;
    public GameObject interactionIcon;

    [Header("Events Assignation")]
    public PlayableDirector director;

    private void Start() {
        if(interactionIcon != null) interactionIcon.SetActive(false);
        dialogObject.timeline = director;
    }

    public void Interact(PlayerController player){
        StartCoroutine(player.DialogUI.ShowDialog(dialogObject));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && other.TryGetComponent(out PlayerController player)){
            if(interactionIcon != null) interactionIcon.SetActive(true);
            player.interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player") && other.TryGetComponent(out PlayerController player)){
            if(player.interactable is DialogActivator dialogActivator && dialogActivator == this){
                if(interactionIcon != null) interactionIcon.SetActive(false);
                player.interactable = null;
            }
        }
    }
}
