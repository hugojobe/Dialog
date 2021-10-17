using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Dialog/DialogObject")]
public class DialogObject : ScriptableObject
{
    [TextArea(5, 5)] public string[] dialog;
    public string[] speakers;
    public Color[] colors;
    public UnityEvent[] dialogStartEvent;
    public float dialogStartDelay;
    public UnityEvent[] dialogEndEvent;

    public string[] Dialog => dialog;
    public string[] Speakers => speakers;
    public Color[] Colors => colors;
    public UnityEvent[] DialogStartEvent => dialogStartEvent;
    public float DialogStartDelay => dialogStartDelay;
    public UnityEvent[] DialogEndEvent => dialogEndEvent;


    public PlayableDirector timeline;
    public void PauseTimeline(){
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(0f);
    }

    public void ResumeTimeline(){
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(1f);
    }
}
