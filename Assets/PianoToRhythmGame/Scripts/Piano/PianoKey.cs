using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace PianoToRhythmGame.Piano
{
    public class PianoKey : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer _keyRenderer;

        public SpriteRenderer KeyRenderer => _keyRenderer;

        /// <summary>
        /// transform which represents the top right corner of a key
        /// </summary>
        [SerializeField]
        Transform _topRightTransform;

        public Vector3 TopRightPosition => _topRightTransform.position;

        /// <summary>
        /// transform which represents the bottom left corner of a key
        /// </summary>
        [SerializeField]
        Transform _bottomLeftTransform;

        public Vector3 BottomLeftPosition => _bottomLeftTransform.position;

        public float Width
        {
            get
            {
                return TopRightPosition.x - BottomLeftPosition.x;
            }
        }

        public float Height
        {
            get
            {
                return TopRightPosition.y - BottomLeftPosition.y;
            }
        }

        public Vector3 CenterPosition
        {
            get
            {
                return (TopRightPosition + BottomLeftPosition) / 2.0f;
            }
        }

        /// <summary>
        /// used to get input from MidiMaster
        /// </summary>
        public int NoteNumber { get; set; }

        ReactiveProperty<bool> _isPressingRP = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsPressingReactiveProperty => _isPressingRP;
        public bool IsPressing => IsPressingReactiveProperty.Value;

        public void PressKey()
        {
            if (!IsPressing)
            {
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

        // Start is called before the first frame update
        void Start()
        {
            Validate();
        }

        void Validate()
        {
            UnityEngine.Assertions.Assert.IsNotNull(_topRightTransform, "_topRightTransform is null");
            UnityEngine.Assertions.Assert.IsNotNull(_bottomLeftTransform, "_bottomLeftTransform is null");
            UnityEngine.Assertions.Assert.IsTrue(_topRightTransform.position.x > _bottomLeftTransform.position.x, $"right:{_topRightTransform.position.x}, left:{_bottomLeftTransform.position.x}");
            UnityEngine.Assertions.Assert.IsTrue(_topRightTransform.position.y > _bottomLeftTransform.position.y, $"top:{_topRightTransform.position.y}, bottom:{_bottomLeftTransform.position.y}");
        }
    }
}
