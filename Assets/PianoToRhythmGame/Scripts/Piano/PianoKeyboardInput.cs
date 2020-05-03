using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using PianoToRhythmGame.Input;
using Zenject;

namespace PianoToRhythmGame.Piano
{
    [RequireComponent(typeof(PianoKeyboard))]
    public class PianoKeyboardInput : MonoBehaviour
    {
        [Inject]
        IMidiInputProvider _inputProvider;

        [SerializeField]
        float _pressThreshold = 0.1f;

        [SerializeField]
        float _releaseThreshold = 0.1f;

        PianoKeyboard _keyboard;

        void Awake()
        {
            _keyboard = GetComponent<PianoKeyboard>();
        }

        void Start()
        {
            SetupStreams();
        }

        void SetupStreams()
        {
            for (var i = 0; i < _inputProvider.NumTotalNotes; i++)
            {
                SetupStream(i);
            }
        }

        void SetupStream(int noteNumber)
        {
            var velocityRP = _inputProvider.GetVelocity(noteNumber);

            velocityRP.Pairwise()
                      .Where(v => v.Previous < _pressThreshold && _pressThreshold <= v.Current)
                      .Subscribe(v =>
                      {
                          Press(noteNumber, v.Current);
                      });


            velocityRP.Pairwise()
                      .Where(v => v.Current < _releaseThreshold && _releaseThreshold <= v.Previous)
                      .Subscribe(_ =>
                      {
                          Release(noteNumber);
                      });
        }

        void Press(int noteNumber, float velocity)
        {
            var key = _keyboard.GetKey(noteNumber);

            if (key != null)
            {
                key.PressKey(velocity);
            }
        }

        void Release(int noteNumber)
        {
            var key = _keyboard.GetKey(noteNumber);

            if (key != null)
            {
                key.ReleaseKey();
            }
        }
    }
}
