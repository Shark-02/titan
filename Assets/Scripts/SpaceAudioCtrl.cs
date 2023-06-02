using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceAudioCtrl : MonoBehaviour
{
    [SerializeField] UIFadeController UIC;
    //[SerializeField] UIFadeController UIC_Accept;
    //[SerializeField] UIFadeController UIC_Refuse;
    //[SerializeField] BlurController BC;
    public Button playButton;
    //public Button AcceptButton;
    //public Button RefuseButton;
    public AudioClip[] audioClips;//对话列表
    private AudioSource audioSource;
    private int currentClipIndex = 0;//当前对话片段
    private bool isPlaying = true;//是否需要播放声音
    //public XRController controller;
    void Start()
    {
        StartCoroutine(wake(1.0f));
        //CurtainController.Instance.ShowCurtain(0);
        audioSource = GetComponent<AudioSource>();
        //CurtainController.Instance.HideCurtain(1);
        
    }

    public IEnumerator wake(float time)
    {
        CurtainController.Instance.ShowCurtain(0);
        yield return new WaitForSeconds(time);
        CurtainController.Instance.HideCurtain(1);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(PlayAudioClips());
    }
    public IEnumerator PlayAudioClips()
    {

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
                    currentClipIndex++;
                    //UIC.UI_FadeIn_Event();
                }
                else if (currentClipIndex == 0)
                {
                    audioSource.clip = audioClips[currentClipIndex];
                    audioSource.Play();
                    yield return new WaitForSeconds(audioSource.clip.length);
                    audioSource.Stop();
                    currentClipIndex++;
                }
                else if (currentClipIndex == audioClips.Length - 1)
                {
                    audioSource.clip = audioClips[currentClipIndex];
                    audioSource.Play();
                    yield return new WaitForSeconds(audioSource.clip.length);
                    audioSource.Stop();
                    isPlaying = false;

                }

            }

            yield return null;
        }
    }

}