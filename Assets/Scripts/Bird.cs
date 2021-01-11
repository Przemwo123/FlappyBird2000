using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    public GameObject soundJump;
    public GameObject soundKill;
    public float jumpForce = 10f;

    private bool _isDead = false;
    private Rigidbody2D _rigidbody2DBird;

    private void Start()
    {
        _rigidbody2DBird = GetComponent<Rigidbody2D>();
        _rigidbody2DBird.simulated = false;
    }

    void Update()
    {
        if (_isDead) return;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        transform.rotation = Quaternion.Euler(0,0, _rigidbody2DBird.velocity.y*2f);
    }

    private void Jump()
    {
        if (!GameManager.S.GetIsActive())
        {
            GameManager.S.IsActive();
            _rigidbody2DBird.simulated = true;
        }

        Instantiate(soundJump);

        _rigidbody2DBird.velocity = Vector2.up * jumpForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isDead)
        {
            _isDead = true;
            Instantiate(soundKill);
            _rigidbody2DBird.velocity = (Vector2.up * jumpForce) + Vector2.right;
            GameManager.S.Die();
            gameObject.layer = 11;
        }
    }
}