using UnityEngine;

public class MoveTube : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    
    private float _endPosition;
    private Rigidbody2D _rigidbody2D;

    public float SetSpeed { set { _speed = value; } }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(-_speed, 0);
        _endPosition = -transform.position.x;
    }

    void Update()
    {
        if (GameManager.S.isGameOver)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }

        if (transform.position.x <= _endPosition)
        {
            Destroy(gameObject);
        }
    }
}