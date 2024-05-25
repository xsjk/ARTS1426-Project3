using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicPlayerLogic : MonoBehaviour
{
    [SerializeField]

    List<AudioClip> audioClips;
    AudioSource audioSource;

    int currentAudioIndex = 0;

    [SerializeField]
    Animator playerButtonAnimator;

    bool isPlaying = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClips.Count == 0) {
            Debug.LogError("Not enough music");
        }
    }

    public void OnPressPlayButton() {
        if (isPlaying) {
            stop();
        } else {
            play();
        }
        isPlaying = !isPlaying;
    }

    public void SwitchSong(int index) {
        stop();
        currentAudioIndex = index;
        play();
    }
    public void OnPressNextButton() {
        SwitchSong((currentAudioIndex + 1) % audioClips.Count);
    }

    public void OnPressPreviousButton() {
        SwitchSong((currentAudioIndex - 1 + audioClips.Count) % audioClips.Count);
    }

    void play() {
        audioSource.PlayOneShot(audioClips[currentAudioIndex]);
        playerButtonAnimator.SetTrigger("Selected");
    }

    void stop() {
        playerButtonAnimator.SetTrigger("Normal");
        audioSource.Stop();
    }
}
