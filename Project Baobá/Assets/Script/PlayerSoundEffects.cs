using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _jumpSource;

    [SerializeField] private AudioSource _deathSource;
    // Start is called before the first frame update

    // Update is called once per frame
    public void PlayDeathSound()
    {
        _deathSource.Play();
    }

    public void PlayJumpSound()
    {
        _jumpSource.Play();
    }
}
