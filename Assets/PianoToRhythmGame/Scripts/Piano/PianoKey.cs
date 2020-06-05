using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace PianoToRhythmGame.Piano
{
    [RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
    public class PianoKey : MonoBehaviour
    {
        public BoxCollider2D KeyCollider { get; private set; }

        public SpriteRenderer KeyRenderer { get; private set; }

        [SerializeField]
        Vector3 _positionOffset;

        public Vector3 PositionOffset => _positionOffset;

        /// <summary>
        /// Returns width of a key.
        /// Note that this length is in the local space since Collider.bounds is used for calculation.
        /// </summary>
        public float Width
        {
            get
            {
                return KeyCollider.bounds.size.x;
            }
        }

        /// <summary>
        /// Returns height of a key.
        /// Note that this length is in the local space since Collider.bounds is used for calculation.
        /// </summary>
        public float Height
        {
            get
            {
                return KeyCollider.bounds.size.y;
            }
        }

        /// <summary>
        /// used to get input from MidiMaster
        /// </summary>
        public int NoteNumber { get; set; }

        public float Velocity { get; private set; }
        ReactiveProperty<bool> _isPressingRP = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsPressingReactiveProperty => _isPressingRP;
        public bool IsPressing => IsPressingReactiveProperty.Value;


        public void PressKey()
        {
            PressKey(1f);
        }

        public void PressKey(float velocity)
        {
            if (!IsPressing)
            {
                this.Velocity = velocity;
                _isPressingRP.Value = true;
            }
        }

        public void ReleaseKey()
        {
            if (IsPressing)
            {
                _isPressingRP.Value = false;
            }
        }

        void Awake()
        {
            this.KeyCollider = GetComponent<BoxCollider2D>();
            this.KeyRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
