using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulo2 : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    // Mudanças para pulo
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private void Awake ()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_rigidBody.velocity.y < 0){
            _rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier-1) * Time.deltaTime;
        } else if (_rigidBody.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space)){
            _rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier-1) * Time.deltaTime;
        }
    }

}