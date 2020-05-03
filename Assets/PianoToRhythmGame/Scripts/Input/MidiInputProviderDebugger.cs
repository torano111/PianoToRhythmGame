using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

namespace PianoToRhythmGame.Input
{
    public class MidiInputProviderDebugger : MonoBehaviour
    {
        [Inject]
        IMidiInputProvider _inputProvider;

        // Start is called before the first frame update
        void Start()
        {
            for (var noteNumber = 0; noteNumber < _inputProvider.NumTotalNotes; noteNumber++)
            {
                var noteSt = noteNumber.ToString();

                _inputProvider.GetVelocity(noteNumber)
                              .Where(v => v >= 0.1f)
                              .Subscribe(v =>
                              {
                                  OutputLog($"note:{noteSt}, velocity:{v}");
                              });
            }
        }

        void OutputLog(string message)
        {
            Debug.Log(message);
        }
    }
}
