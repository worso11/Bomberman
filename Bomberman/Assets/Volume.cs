using UnityEngine;
using UnityEngine.Audio;

// Class reponsible for the volume in the game
public class Volume : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    // Function to set volume
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("Volume", value);
    }
}
