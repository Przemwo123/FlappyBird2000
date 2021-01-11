using UnityEngine;

public class Tube : MonoBehaviour
{
    public GameObject soundScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Bird>() != null)
        {
            GameManager.S.Score();
            GetComponent<BoxCollider2D>().enabled = false;
            Instantiate(soundScore);
        }
    }
}