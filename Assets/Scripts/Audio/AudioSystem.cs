using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private PoolMono<AudioSource> sourcePool;

    private static AudioSystem instance;
    
    public static AudioSystem Instance => instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        var template = new GameObject("AudioSource").AddComponent<AudioSource>();
        template.volume = 0.2f;
        sourcePool = new PoolMono<AudioSource>(template, 15, true, transform);
    }

    public void PlaySoundOnce(AudioClip clip, Transform parentObj)
    {
        var source = sourcePool.GetFreeElement();
        source.transform.SetParent(parentObj);
        source.transform.localPosition = Vector3.zero;
        source.volume = 0.2f;
        source.PlayOneShot(clip);
        StartCoroutine(WaitForEndOfClip(source, clip.length));
    }

    private IEnumerator WaitForEndOfClip(AudioSource source, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        source.transform.SetParent(transform);
        source.gameObject.SetActive(false);
    }
}