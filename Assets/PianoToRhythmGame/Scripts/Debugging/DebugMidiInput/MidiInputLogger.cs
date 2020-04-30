using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using TMPro;

namespace PianoToRhythmGame.Debugging
{
    public class MidiInputLogger : MonoBehaviour
    {
        [SerializeField]
        int numTotalNotes = 128;

        [SerializeField]
        TextMeshProUGUI _logText;


        // Update is called once per frame
        void Update()
        {
            string log = "";
            for (var noteIndex = 0; noteIndex < numTotalNotes; noteIndex++)
            {
                var velocity = MidiMaster.GetKey(noteIndex);
                if (velocity > 0f)
                {
                    log += $"Note({noteIndex}) Velocity: {velocity}\n";
                }
            }

            DisplayLog(log);
        }

        void DisplayLog(string message)
        {
            Debug.Log(message);

            if (_logText != null)
            {
                _logText.text = message;
            }
        }
    }
}
