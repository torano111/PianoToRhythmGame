using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using MidiJack;

namespace PianoToRhythmGame.Piano
{
    [RequireComponent(typeof(PianoKey))]
    public class PianoKeyInput : MonoBehaviour
    {
        PianoKey _key;

        float _preInput = 0f;
        const float PRESS_THRESHOLD = 0.1f;
        const float RELEASE_THRESHOLD = 0.1f;

        void Awake()
        {
            _key = GetComponent<PianoKey>();
        }

        // Update is called once per frame
        void Update()
        {
            var curInput = MidiMaster.GetKey(_key.NoteNumber);

            if (_preInput < PRESS_THRESHOLD && PRESS_THRESHOLD <= curInput)
            {
                _key.PressKey();
            }
            else if (curInput < RELEASE_THRESHOLD && RELEASE_THRESHOLD <= _preInput)
            {
                _key.ReleaseKey();
            }

            _preInput = curInput;
        }
    }
}
