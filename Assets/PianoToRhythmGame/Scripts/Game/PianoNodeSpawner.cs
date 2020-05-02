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
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
