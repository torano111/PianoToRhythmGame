using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

namespace PianoToRhythmGame.Debugging
{
    public class MidiInputLogger : MonoBehaviour
    {
        const int NUM_NOTES = 128;

        // Update is called once per frame
        void Update()
        {
            for (var noteIndex = 0; noteIndex < NUM_NOTES; noteIndex++)
            {
                var onNote = MidiMaster.GetKeyDown(noteIndex);
                if (onNote)
                {
                    Debug.Log($"On:{noteIndex}");
                }
            }
        }
    }
}
