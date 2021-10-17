using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public float typewriteSpeed;

    public bool isRunning {get; set;}
    private readonly List<Punctuation> punctuations = new List<Punctuation>(){
        new Punctuation(new HashSet<char>(){'.', '!', '?'}, 0.7f),
        new Punctuation(new HashSet<char>(){',', ';', ':'}, 0.4f),
    };

    private Coroutine typingCoroutine;

    public void Run(string textToType, string speaker, Color color, TMP_Text labelText, TMP_Text speakerText){
        typingCoroutine = StartCoroutine(TypeText(textToType, speaker, color, labelText, speakerText));
    }

    public void Stop(){
        StopCoroutine(typingCoroutine);
        isRunning = false;
    }

    public IEnumerator TypeText(string textToType, string speaker, Color color, TMP_Text labelText, TMP_Text speakerText){
        isRunning = true;
        labelText.text = string.Empty;
        
        speakerText.text = "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + speaker + "<color=#ffffff>";

        float t = 0;
        int charIndex = 0;
        while(charIndex < textToType.Length){
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typewriteSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for(int i = lastCharIndex; i < charIndex; i++){
                bool isLast = i >= textToType.Length - 1;
                labelText.text = textToType.Substring(0, i + 1);
                if(IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _)){
                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }

        isRunning = false;
        labelText.text = textToType;
    }

    public bool IsPunctuation(char character, out float waitTime){
        foreach(Punctuation punctuationCategory in punctuations){
            if(punctuationCategory.Punctuations.Contains(character)){
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation{
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime){
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
