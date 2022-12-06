using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPlatformBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D _boxCollider;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private PolygonCollider2D _backgroundCollider2D;
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            _boxCollider.isTrigger = false;
        }
        _cameraController.ChangeConfiner(_backgroundCollider2D);
    }
}
