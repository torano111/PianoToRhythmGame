using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace PianoToRhythmGame.Input
{
    public interface IMidiInputProvider
    {
        int NumTotalNotes { get; }

        IReadOnlyReactiveProperty<float> GetVelocity(int noteNumber);
    }
}
