using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    #region Internal Data
    private Vector2 _moveDir = Vector2.zero;
    private Rigidbody2D _rb;
    [SerializeField] private float moveSpeed = 5f;
     private Direction _direction = Direction.RIGHT;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private readonly int _animMoveRight = Animator.StringToHash("Anim_Player_Move_To_Right");
    private readonly int _animIdleRight = Animator.StringToHash("Anim_Player_Idle_Right");

    #endregion

    #region enum
    private enum Direction { UP, DOWN, LEFT, RIGHT }
    #endregion

    #region Unity Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        GatherInput();
        CalculateFacingDirection();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region Input Logic
    private void GatherInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");
        _moveDir.Normalize(); // supaya diagonal tidak lebih cepat
    }
    #endregion

    #region Movement Logic
    private void Move()
    {
        _rb.linearVelocity = _moveDir * moveSpeed;
    }
    #endregion

    #region AnimationLogic
    private void CalculateFacingDirection()
    {
        if (_moveDir.x != 0) {
            if (_moveDir.x > 0) // Moving Right
            {
                _direction = Direction.RIGHT;
            } else if (_moveDir.x < 0) // Moving Left
            {
                _direction = Direction.LEFT;
            }
        }

        Debug.Log(_direction);
    }

    private void UpdateAnimation()
    {
        if (_direction == Direction.RIGHT)
        {
            _spriteRenderer.flipX = false;
        } else if (_direction == Direction.LEFT)
        {
            _spriteRenderer.flipX = true;
        }

        if (_moveDir.sqrMagnitude > 0) {
            _animator.CrossFade(_animMoveRight, 0);
        } else
        {
            _animator.CrossFade(_animIdleRight, 0);
        }
    }

    #endregion


}
