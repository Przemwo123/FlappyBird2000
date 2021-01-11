using UnityEngine;

public class AudioLength : MonoBehaviour
{
    private float audioLength = 1;

    private void Start()
    {
        audioLength = GetComponent<AudioSource>().clip.length;
    }

    void Update()
    {
        Destroy(gameObject, audioLength);
    }
}
