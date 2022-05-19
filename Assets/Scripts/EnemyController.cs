using UnityEngine;
public class EnemyController : MonoBehaviour
{
    // Movement
    public float movementSpeed = 2.5f;
    public bool vertical;
    public float directionDuration = 3.0f;
    int _currentDirection = 1;

    float _elapsedDirectionDuration;

    // Collision Box
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _elapsedDirectionDuration = directionDuration;
    }

    void Update()
    {
        _elapsedDirectionDuration -= Time.deltaTime;

        if (_elapsedDirectionDuration < 0)
        {
            _currentDirection = -_currentDirection;
            _elapsedDirectionDuration = directionDuration;
        }
    }

    // Frame-rate independent update method
    void FixedUpdate()
    {
        Vector2 position = _rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * movementSpeed * _currentDirection;
        }
        else
        {
            position.x = position.x + Time.deltaTime * movementSpeed * _currentDirection;
        }

        _rigidbody2D.MovePosition(position);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.UpdateHealth(-1);
        }
    }
}