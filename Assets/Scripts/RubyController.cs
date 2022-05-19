using UnityEngine;
public class RubyController : MonoBehaviour
{

    #region Update Stats

    public void UpdateHealth(int amount)
    {
        if (amount < 0)
        {
            if (_isInvincible)
                return;

            _isInvincible = true;
            _invincibleTimer = invincibleDuration;
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
        Debug.Log($"Current Health {CurrentHealth}/{maxHealth}");
    }

    #endregion

    #region Character Setup

    // Stats
    public int maxHealth = 5;
    public int initialHealth = 5;
    public int CurrentHealth { get; private set; }

    // Short term invincibility to control rate of health loss
    bool _isInvincible;
    float _invincibleTimer;
    public float invincibleDuration = 1.0f;

    // Movement Speed
    public float movementSpeed = 5.0f;

    // Collision Box
    Rigidbody2D _rigidbody2D;

    // Position
    float _horizontal;
    float _vertical;

    // Animations
    Animator _animator;
    Vector2 _lookDirection = new Vector2(1, 0);

    #endregion

    #region Unity Events

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(initialHealth);

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get movement input values
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        // Determine direction character is facing
        Vector2 move = new Vector2(_horizontal, _vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            _lookDirection.Set(move.x, move.y);
            _lookDirection.Normalize();
        }

        // Send look direction to animator
        _animator.SetFloat("Look X", _lookDirection.x);
        _animator.SetFloat("Look Y", _lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);

        // Temporary invincibility timer
        if (_isInvincible)
        {
            _invincibleTimer -= Time.deltaTime;
            if (_invincibleTimer < 0)
            {
                _isInvincible = false;
            }
        }
    }

    // Frame-rate independent update method
    void FixedUpdate()
    {
        Vector2 position = _rigidbody2D.position;

        position.x = position.x + movementSpeed * _horizontal * Time.deltaTime;
        position.y = position.y + movementSpeed * _vertical * Time.deltaTime;

        _rigidbody2D.MovePosition(position);
    }

    #endregion

}