using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MidiJack;

namespace PianoToRhythmGame
{
    public class MidiInputIndicator : MonoBehaviour
    {
        [SerializeField]
        int _noteNumber;

        public int NoteNumber { get => this._noteNumber; set => this._noteNumber = value; }

        [SerializeField]
        float _width = 1f;

        public float Width
        {
            get => _width;
            set
            {
                _width = value;
                transform.localScale = new Vector3(_width, transform.localScale.y, transform.localScale.z);
            }
        }

        [SerializeField]
        float _minHeight = 1f;

        public float MinHeight
        {
            get => _minHeight;
            set => _minHeight = Mathf.Max(Mathf.Min(MaxHeight, value), 0.0001f);
        }

        [SerializeField]
        float _maxHeight = 2f;

        public float MaxHeight
        {
            get => _maxHeight;
            set => _maxHeight = Mathf.Max(MinHeight, value);
        }

        [SerializeField]
        Color _offColor = Color.white;

        [SerializeField]
        Color _onColor = Color.red;

        SpriteRenderer _spriteRenderer;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            ChangeColor(_offColor);
        }

        void ChangeColor(Color color)
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = color;
            }
        }

        // Update is called once per frame
        void Update()
        {
            var noteValue = MidiMaster.GetKey(NoteNumber);
            UpdateIndicator(noteValue);
        }

        void UpdateIndicator(float noteValue)
        {
            var bottom = transform.position.y - transform.localScale.y / 2.0f;
            var minToMax = MaxHeight - MinHeight;
            var height = MinHeight + minToMax * noteValue;
            transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);

            var posY = bottom + height / 2.0f;
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);

            ChangeColor(Color.Lerp(_offColor, _onColor, noteValue));
        }
    }
}
