using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class AudioController : MonoBehaviour
{
    public Button playButton;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool isPlaying = true;
    public XRController controller;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playButton.onClick.AddListener(OnPlayButtonClicked);

        //StartCoroutine(PlayAudioClips());
    }

    public IEnumerator PlayAudioClips()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (isPlaying)
            {
                audioSource.clip = audioClips[currentClipIndex];
                audioSource.Play();

                yield return new WaitForSeconds(audioSource.clip.length);

                audioSource.Stop();
                isPlaying = false;
            }

            yield return null;
        }
    }

    public void OnPlayButtonClicked()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            currentClipIndex++;

            if (currentClipIndex >= audioClips.Length)
            {
                currentClipIndex = 0;
            }
        }
    }
}