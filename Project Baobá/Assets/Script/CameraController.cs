using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner _confiner;
    // Start is called before the first frame update
    public void ChangeConfiner(PolygonCollider2D backgroundCollider2D)
    {
        _confiner.m_BoundingShape2D = backgroundCollider2D;
    }
}
