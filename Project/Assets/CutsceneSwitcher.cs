using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneSwitcher : MonoBehaviour
{
    public PlayableDirector director;
    public TimelineAsset newTimeline;

    void Start()
    {
        director.stopped += OnCutsceneEnded;
    }

    void OnCutsceneEnded(PlayableDirector pd)
    {
        director.playableAsset = newTimeline;
        director.Play();
    }
}

