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

        [SerializeField]
        int _numTotalNotes = 128;

        public int NumTotalNotes { get => this._numTotalNotes; set => this._numTotalNotes = value; }

        Vector3 ScreenBottomLeft
        {
            get => Camera.main.ScreenToWorldPoint(Vector3.zero);
        }

        Vector3 ScreenTopRight
        {
            get => Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        }

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
            var bottomLeft = ScreenBottomLeft;
            var topRight = ScreenTopRight;

            var bottom = bottomLeft.y;
            var left = bottomLeft.x;
            var right = topRight.x;
            var top = topRight.y;

            var width = Mathf.Abs(left - right) / (float)_numTotalNotes;

            var minToMax = MaxHeight - MinHeight;
            var height = MinHeight + minToMax * noteValue;
            transform.localScale = new Vector3(width, height, transform.localScale.z);

            var posX = left + width * NoteNumber + width / 2.0f;
            var posY = bottom + height / 2.0f;
            transform.position = new Vector3(posX, posY, transform.position.z);

            ChangeColor(Color.Lerp(_offColor, _onColor, noteValue));
        }
    }
}
