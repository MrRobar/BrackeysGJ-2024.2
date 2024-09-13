using UnityEngine;

public class BGMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    
    private void Awake()
    {
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}