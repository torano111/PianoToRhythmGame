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
                var velocity = MidiMaster.GetKey(noteIndex);
                if (velocity > 0f)
                {
                    Debug.Log($"Note: {noteIndex}, Velocity: {velocity}");
                }
            }
        }
    }
}
