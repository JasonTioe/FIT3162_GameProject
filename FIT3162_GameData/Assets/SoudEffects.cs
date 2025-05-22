using UnityEngine;

public class SoudEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip click;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ButtonClicks()
    {
        src.clip = click;
        src.Play();
        print("SOUND");
        
    }
}
