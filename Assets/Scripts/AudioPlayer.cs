using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource sceneMusic;
    public AudioSource buttonPushSound;
    public AudioSource schoolBellSound;

    public void playSceneMusic()
    {
        sceneMusic.Play();
    }

    public void playButtonPushSound()
    {
        buttonPushSound.Play();
    }

    public void playSchoolBellSound()
    {
        schoolBellSound.Play();
    }
}
