using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movimentação : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpStrength;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] TrailRenderer tr;

    [SerializeField] private float dashSpeed;
    private float dashTime;
    [SerializeField] private float startDashTime;
    private int direction;

    private float _movement = 0f;

    private bool _isGrounded = true;
    private bool _parry = false;

    private bool _isFacingRight = true;
    private Animator _playerAnimator;

    private bool agir = true;

    private void Awake ()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        dashTime = startDashTime;
        _playerAnimator = GetComponent<Animator>();
    } 

    void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.CompareTag("Vento")){
            _rigidBody.velocity = new Vector2( _speed, _rigidBody.velocity.y);
        }
        if(other.gameObject.CompareTag("Seiva")){
            _rigidBody.velocity = new Vector2(0,0);
            transform.position = _rigidBody.position;
            _speed = _speed/2;
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.CompareTag("Parry")){
            _parry = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Seiva")){
            _speed = _speed*2;
            Debug.Log(_speed);
        }
        if(other.gameObject.CompareTag("Parry")){
            _parry = false;
        }
    }

    private void Update ()
    {
        _movement = Input.GetAxisRaw("Horizontal");
        _isGrounded = CheckGroundedStatus();

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && agir)
            Jump();

        if (Input.GetMouseButtonDown(2) && _parry && agir){
            Parry();
        }   
        if(direction == 0){
            if(Input.GetMouseButtonDown(1) && _isFacingRight){
                direction = 1;
            } else if(Input.GetMouseButtonDown(1) && !_isFacingRight){
                direction = 2;
            } 
        } else {
            if(dashTime <= 0){
                direction = 0;
                dashTime =  startDashTime;
                _rigidBody.velocity = Vector2.zero;
            } else {
                dashTime -= Time.deltaTime;

                if(direction == 1)
                {
                    agir = false;
                    _rigidBody.velocity = Vector2.right * dashSpeed;
                    agir = true;
                } else if (direction == 2)
                {
                    agir = false;
                    _rigidBody.velocity = Vector2.left * dashSpeed;
                    agir = true;
                }
            }
        }
    }

    private void FixedUpdate ()
    {
        if (_movement != 0f && agir)
            Move();
    }

    private void Parry (){
        _rigidBody.velocity += new Vector2(0f, ((_jumpStrength*70)/100));
        _parry = false;
    }

    private bool CheckGroundedStatus ()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,
            0f, Vector2.down, 0.1f, _groundMask);

        return (raycastHit.collider != null);
    }

    private void Move ()
    {
        _rigidBody.velocity = new Vector2(_movement * _speed, _rigidBody.velocity.y);
        if (!_isFacingRight && _movement > 0)
        {
            Flip(1);
        }else if (_isFacingRight && _movement < 0)
        {
            Flip(-1);
        }
        
        _playerAnimator.SetBool("isWalking", true);
    }

    private void Jump ()
    {
        _rigidBody.velocity += new Vector2(0f, _jumpStrength);
        GetComponent<PlayerSoundEffects>().PlayJumpSound();
        _playerAnimator.SetBool("hasJumped", true);
    }

    private void OnDrawGizmosSelected ()
    {
        if (_boxCollider == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + (Vector3)(Vector2.down * 0.1f), _boxCollider.bounds.size);
    }

    private void Flip(int direction)
    {
        if (_isFacingRight)
            _isFacingRight = false;
        else
            _isFacingRight = true;
        Vector2 Scale = (Vector2)transform.localScale;
        Scale.x = direction;
        transform.localScale = Scale;
    }
}
