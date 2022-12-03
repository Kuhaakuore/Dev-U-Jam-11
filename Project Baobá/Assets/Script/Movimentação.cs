using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentação : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpStrength;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] TrailRenderer tr;

    private bool canDash = true;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private float _movement = 0f;

    private bool _isGrounded = true;
    private bool _parry = false;


    private void Awake ()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Parry")){
            _parry = true;
        }
        if(other.gameObject.CompareTag("Vento")){
            _rigidBody.velocity = new Vector2( _speed, _rigidBody.velocity.y);
        }
        if(other.gameObject.CompareTag("Seiva")){
            _rigidBody.velocity = new Vector2(0,0);
            transform.position = _rigidBody.position;
            _speed = _speed/2;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Seiva")){
            _speed = _speed*2;
            Debug.Log(_speed);
        }
    }

    private void Update ()
    {
        _movement = Input.GetAxisRaw("Horizontal");
        _isGrounded = CheckGroundedStatus();

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            Jump();
        
        if (Input.GetMouseButtonDown(1) && canDash){
            StartCoroutine(dash());
        }

        if (Input.GetMouseButtonDown(2) && _parry){
            Parry();
        }        
    }

    private void FixedUpdate ()
    {
        if (_movement != 0f)
            Move();
    }

    private IEnumerator dash(){
        canDash = false;
        float originalGravity = _rigidBody.gravityScale;
        _rigidBody.gravityScale = 0f;
        _rigidBody.velocity = new Vector2(transform.localScale.x * dashingPower,0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        _rigidBody.gravityScale = originalGravity;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
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
    }

    private void Jump ()
    {
        _rigidBody.velocity += new Vector2(0f, _jumpStrength);
    }

    private void OnDrawGizmosSelected ()
    {
        if (_boxCollider == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_boxCollider.bounds.center + (Vector3)(Vector2.down * 0.1f), _boxCollider.bounds.size);
    }
}
