using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private Transform _platform;

    [SerializeField] private PlayerSoundEffects _playerSoundEffects;
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType() == typeof(CapsuleCollider2D))
        {
            _playerSoundEffects.PlayDeathSound();
            _player.position = new Vector3(_platform.position.x, _platform.position.y + 1f, 0);
        }
    }
}
