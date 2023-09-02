using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInputController : MonoBehaviour
{
    public static MicrophoneInputController Instance;
    [SerializeField] int m_SampleWindow;
    
    AudioClip m_MicrophoneClip;
    [SerializeField] float m_MicrophoneSensetivity = 100f;
    [SerializeField] float m_MicrophoneThreshold = .1f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MicrophoneToAudioClip();
    }

    void Update()
    {
        var currentLoudness = GetLoudnessFromMicrophone() * m_MicrophoneSensetivity;

        if (currentLoudness < m_MicrophoneThreshold)
        {
            currentLoudness = 0f;
        }
        
        Debug.LogError($"microphone input = {currentLoudness}");
    }

    void MicrophoneToAudioClip()
    {
        var microphoneName = Microphone.devices[0];
        m_MicrophoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone()
    {
        var clipPosition = Microphone.GetPosition(Microphone.devices[0]);
        var startPosition = clipPosition - m_SampleWindow;

        if (startPosition < 0)
        {
            return 0;
        }

        var waveData = new float[m_SampleWindow];
        m_MicrophoneClip.GetData(waveData, startPosition);

        var totalLoudness = 0f;

        for (var i = 0; i < m_SampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / m_SampleWindow;
    }
}
