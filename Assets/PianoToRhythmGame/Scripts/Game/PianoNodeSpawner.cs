using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using PianoToRhythmGame.Piano;

namespace PianoToRhythmGame.Game
{
    public class PianoNodeSpawner : MonoBehaviour
    {
        const int NoteLength = 128;

        [SerializeField]
        PianoKeyboard _keyboard;


        // Start is called before the first frame update
        void Start()
        {
            _keyboard.OnBuildKeyboard
                     .Subscribe(_ =>
                     {
                         for (var i = _keyboard.FirstNoteNumber; i < _keyboard.FirstNoteNumber + _keyboard.NumKeys; i++)
                         {
                             var key = _keyboard.GetKey(i);
                         }
                     });
        }

        IDisposable SetupSpawnNodeStream(PianoKey key)
        {
            var result = key.IsPressingReactiveProperty
                            .Where(pressing => pressing)
                            .Select(_ => key.Velocity)
                            .Subscribe(velocity =>
                            {

                            });

            return result;
        }
    }
}
