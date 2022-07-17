using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource[] audios;

    public void Start()
    {
        //PLAY MUSIQUE
        AudioSource audio = Instantiate(audios[0], transform.position, Quaternion.identity);
        audio.Play();
    }
    public void PlaySound(int ID,Vector3 pos)
    {
        AudioSource audio = Instantiate(audios[ID], pos, Quaternion.identity);
        audio.Play();
        StartCoroutine(discardSound(audio.gameObject));
    }

    public void PlaySoundVariant(int ID, Vector3 pos)
    {
        AudioSource audio = Instantiate(audios[ID], pos, Quaternion.identity);
        audio.pitch = Random.Range(1f,1.7f);
        audio.Play();
        StartCoroutine(discardSound(audio.gameObject));
    }

    IEnumerator discardSound(GameObject audioSource)
    {
        yield return new WaitForSeconds(3);
        Destroy(audioSource);
    }
}
