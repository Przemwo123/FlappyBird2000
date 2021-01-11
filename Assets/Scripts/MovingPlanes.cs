using UnityEngine;

public class MovingPlanes : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    public float sizeX = 16;
    public bool isGround = false;

    private Rigidbody2D _rigidbody2D;

    public float SetSpeed { set { _speed = value; } }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(-_speed,0);
    }

    void Update()
    {
        if (GameManager.S.isGameOver)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }

        if (transform.position.x < -sizeX)
        {
            Vector3 offset = new Vector3(sizeX * 2, 0, 0);
            transform.position = transform.position + offset;

            if (isGround)
            {
                GameManager.S.AddRepeating();

                if (!GameManager.S.biomList[GameManager.S.GetCurrentBiom()].name.Equals(this.tag))
                {
                    Instantiate(GameManager.S.biomList[GameManager.S.GetCurrentBiom()].ground, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }
}
