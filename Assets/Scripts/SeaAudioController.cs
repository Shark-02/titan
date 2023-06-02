using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaAudioController : MonoBehaviour
{
    [SerializeField] UIFadeController UIC;
    [SerializeField] UIFadeController UIC_Accept;
    [SerializeField] UIFadeController UIC_Refuse;
    //[SerializeField] BlurController BC;
    public Button playButton;
    public Button AcceptButton;
    public Button RefuseButton;
    public AudioClip[] audioClips;//对话列表
    private AudioSource audioSource;
    private int currentClipIndex = 0;//当前对话片段
    private bool isPlaying = true;//是否需要播放声音
    //public XRController controller;
    public Transform seaend;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playButton.onClick.AddListener(OnPlayButtonClicked);
        AcceptButton.onClick.AddListener(OnAcceptButtonClicked);
        RefuseButton.onClick.AddListener(OnRefuseButtonClicked);

        StartCoroutine(PlayAudioClips());
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
                    UIC.UI_FadeOut_Event();
                    UIC_Accept.UI_FadeIn_Event();
                    UIC_Refuse.UI_FadeIn_Event();
                    isPlaying = false;
                    Debug.Log("UIC_Accept.UI_Fadein_Event");

                }
                isPlaying = false;

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
    public void OnAcceptButtonClicked()
    {
        
        UIC_Accept.UI_FadeOut_Event();
        Debug.Log("UIC_Accept.UI_FadeOut_Event");
    }
    public void OnRefuseButtonClicked()
    {
        
        UIC_Refuse.UI_FadeOut_Event();
        Debug.Log("UIC_Refuse.UI_FadeOut_Event");
    }
}