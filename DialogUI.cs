using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogUI : MonoBehaviour
{
    public TypewriterEffect typewriterEffect;

    public TMP_Text labelText;
    public TMP_Text speakerText;
    public GameObject dialogBox;

    public bool IsOpen {get; set;}

    private void Start() {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogBox(null);
    }

    public IEnumerator ShowDialog(DialogObject dialogObject){
        foreach(UnityEvent startEvent in dialogObject.dialogStartEvent){
            startEvent.Invoke();
            yield return null;
        }
        yield return new WaitForSeconds(dialogObject.DialogStartDelay);
        IsOpen = true;
        dialogBox.SetActive(true);
        StartCoroutine(StepThroughDialog(dialogObject));
    }

    public IEnumerator StepThroughDialog(DialogObject dialogObject){

        for(int i =0; i <dialogObject.Dialog.Length; i++){
            string dialog = dialogObject.Dialog[i];
            string speaker = dialogObject.Speakers[i];
            Color color = dialogObject.Colors[i];
            yield return RunTypingCoroutine(dialog, speaker, color, speakerText);

            labelText.text = dialog;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        
        CloseDialogBox(dialogObject);
    }

    public IEnumerator RunTypingCoroutine(string dialog, string speaker, Color color, TMP_Text speakerText){
        typewriterEffect.Run(dialog, speaker, color, labelText, speakerText);

        while(typewriterEffect.isRunning){
            yield return null;

            if(Input.GetKeyDown(KeyCode.X)){
                typewriterEffect.Stop();
            }
        }
    }

    public void CloseDialogBox(DialogObject dialogObject){
        IsOpen = false;
        dialogBox.SetActive(false);
        labelText.text = string.Empty;
        if(dialogObject != null){
            foreach(UnityEvent endEvent in dialogObject.dialogEndEvent){
                endEvent.Invoke();
            }
        }
    }
}
