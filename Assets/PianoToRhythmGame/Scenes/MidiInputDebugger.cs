using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PianoToRhythmGame
{
    public class MidiInputDebugger : MonoBehaviour
    {
        [SerializeField]
        MidiInputIndicator _indicatorPrefab;

        [SerializeField]
        float _indicatorMinHeight = 1f;

        [SerializeField]
        float _indicatorMaxHeight = 10f;

        [SerializeField]
        int _numTotalNotes = 128;

        // Start is called before the first frame update
        void Start()
        {
            InitializeIndicators();
        }

        void InitializeIndicators()
        {
            for (var noteIndex = 0; noteIndex < _numTotalNotes; noteIndex++)
            {
                var indicator = Instantiate(_indicatorPrefab, Vector3.zero, Quaternion.identity) as MidiInputIndicator;
                indicator.NumTotalNotes = _numTotalNotes;
                indicator.NoteNumber = noteIndex;
            }
        }
    }
}
