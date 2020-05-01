using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PianoToRhythmGame.Piano
{
    // number of keys of a keyboard
    public enum PianoKeyboardType
    {
        Key88,
        Key61,
    }

    // color of a key
    public enum PianoKeyColor
    {
        // while tone
        White,

        // half tone
        Black,
    }

    public enum PianoWhiteKey
    {
        Do,
        Re,
        Mi,
        Fa,
        Sol,
        La,
        Ti,
    }
}
