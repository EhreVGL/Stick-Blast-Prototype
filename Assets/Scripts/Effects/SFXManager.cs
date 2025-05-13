using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Audio Clips")]
    public AudioClip clickSound;
    public AudioClip placeSound;
    public AudioClip failSound;
    public AudioClip squareSound;
    public AudioClip completeRawColumnSound;
    public AudioClip excellentSound;
    public AudioClip winSound;


    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne değişimlerinde silinmesin
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PlayClick() => PlaySFX(clickSound);
    public void PlayPlace() => PlaySFX(placeSound);
    public void PlayFail() => PlaySFX(failSound);
    public void PlaySquare() => PlaySFX(squareSound);
    public void PlayCompleteRawColumn() => PlaySFX(completeRawColumnSound);
    public void PlayExcellent() => PlaySFX(excellentSound);
    public void PlayWin() => PlaySFX(winSound);


    private void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
