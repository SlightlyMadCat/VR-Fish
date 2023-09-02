using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVRRigController : MonoBehaviour
{
    [SerializeField] Transform m_RigParent;
    [SerializeField] Transform m_CameraTransform;
    [SerializeField] float m_MovementSpeed = .1f;

    void Update()
    {
        var desireMovementSpeed = MicrophoneInputController.Instance.GetLoudnessFromMicrophone() * m_MovementSpeed;
        //Debug.LogError($"movement speed is {desireMovementSpeed}");
        m_RigParent.position += m_CameraTransform.forward * desireMovementSpeed;
    }
}
