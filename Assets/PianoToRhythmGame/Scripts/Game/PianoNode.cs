using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using PianoToRhythmGame.Piano;

namespace PianoToRhythmGame.Game
{
    public class PianoNode : MonoBehaviour
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

        public PianoKey Key { get; set; }
    }
}
