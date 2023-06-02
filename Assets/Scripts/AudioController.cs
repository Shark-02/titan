using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class AudioController : MonoBehaviour
{

    [SerializeField] UIFadeController UIC;
    [SerializeField] BlurController BC;
    public Button playButton;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool isPlaying = true;
    public XRController controller;
    public Transform seaend;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playButton.onClick.AddListener(OnPlayButtonClicked);

        //StartCoroutine(PlayAudioClips());
    }

    public IEnumerator PlayAudioClips()
    {
        //BC.StartBlur();
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (isPlaying)
            {
                if (currentClipIndex < audioClips.Length - 1 && currentClipIndex > 0)
                {

                    audioSource.clip = audioClips[currentClipIndex];
                    audioSource.Play();
                    yield return new WaitForSeconds(audioSource.clip.length);
                    audioSource.Stop();
                    UIC.UI_FadeIn_Event();
                }
                else if (currentClipIndex == 0)
                {
                    audioSource.clip = audioClips[currentClipIndex];
                    audioSource.Play();
                    yield return new WaitForSeconds(audioSource.clip.length);
                    audioSource.Stop();
                    UIC.UI_FadeIn_Event();
                    //yield return new WaitForSeconds(5f);
                    //UIC.UI_FadeOut_Event();
                }
                else if (currentClipIndex == audioClips.Length - 1)
                {
                    audioSource.clip = audioClips[currentClipIndex];
                    audioSource.Play();
                    yield return new WaitForSeconds(audioSource.clip.length);
                    audioSource.Stop();
                }
                isPlaying = false;
                if (currentClipIndex == 1)
                {
                    //yield return new WaitForSeconds(audioSource.clip.length/2);
                    NavPathArrow.Instance.points[1] = seaend;
                    NavPathArrow.Instance.ShowPath();
                }
                else if(currentClipIndex == 2)
                {
                    //BC.StopBlur();
                }

            }

            yield return null;
        }
    }

    public void OnPlayButtonClicked()
    {
        if (!isPlaying && currentClipIndex < audioClips.Length)
        {
            isPlaying = true;
            //UIC.UI_FadeOut_Event();

            currentClipIndex++;
            UIC.UI_FadeOut_Event();

        }
    }
}