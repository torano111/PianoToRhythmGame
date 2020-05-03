using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using MidiJack;

namespace PianoToRhythmGame.Input
{
    public class PianoInputProvider : MonoBehaviour, IMidiInputProvider
    {
        Dictionary<int, ReactiveProperty<float>> _notesAndInputs = new Dictionary<int, ReactiveProperty<float>>();

        public int NumTotalNotes => 128;

        bool _initialized = false;

        protected ReactiveProperty<float> GetVelocityInner(int noteNumber)
        {
            InitializeIfNotYet();

            if (_notesAndInputs.TryGetValue(noteNumber, out var input))
            {
                return input;
            }

            return null;
        }

        public IReadOnlyReactiveProperty<float> GetVelocity(int noteNumber)
        {
            return GetVelocityInner(noteNumber);
        }

        void Awake()
        {
            InitializeIfNotYet();
        }

        void InitializeIfNotYet()
        {
            if (_initialized)
            {
                return;
            }

            Init();
            _initialized = true;
        }

        void Init()
        {
            for (var noteNumber = 0; noteNumber < NumTotalNotes; noteNumber++)
            {
                _notesAndInputs.Add(noteNumber, new ReactiveProperty<float>(0f));
            }
        }

        void Start()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    UpdateInputs();
                });
        }

        void UpdateInputs()
        {
            for (var noteNumber = 0; noteNumber < NumTotalNotes; noteNumber++)
            {
                var velocity = MidiMaster.GetKey(noteNumber);
                GetVelocityInner(noteNumber).SetValueAndForceNotify(velocity);
            }
        }
    }
}
