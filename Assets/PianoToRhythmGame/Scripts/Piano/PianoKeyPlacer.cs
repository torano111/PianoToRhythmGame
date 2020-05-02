using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PianoToRhythmGame.Utility;

namespace PianoToRhythmGame.Piano
{
    [RequireComponent(typeof(PianoKey))]
    public class PianoKeyPlacer : MonoBehaviour
    {
        PianoKey _key;

        float _widthOffset;

        /// <summary>
        /// space between the right edge of the screen and the right edge of a keyboard.
        /// </summary>
        public float WidthOffset
        {
            get => _widthOffset;
            set => _widthOffset = value;
        }

        float _heightOffset;

        /// <summary>
        /// space between the bottom edge of the screen and the bottom edge of a keyboard.
        /// </summary>
        public float HeightOffset
        {
            get => _heightOffset;
            set => _heightOffset = value;
        }

        public int NumWhilteKeysBeforeThis { get; set; }

        public int NumTotalWhiteKeys { get; set; }

        public PianoKeyColor KeyColor { get; set; }

        Vector3 ScreenBottomLeft
        {
            get => Camera.main.ScreenToWorldPoint(Vector3.zero);
        }

        Vector3 ScreenTopRight
        {
            get => Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        }

        Vector2Int _lastResolution;
        bool _initial = true;

        void Awake()
        {
            _key = GetComponent<PianoKey>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_initial || _lastResolution.x != Screen.width || _lastResolution.y != Screen.height)
            {
                _initial = false;
                _lastResolution = new Vector2Int(Screen.width, Screen.height);
                UpdateKeyPosition();
            }
        }

        void UpdateKeyPosition()
        {
            var bottomLeft = ScreenBottomLeft;
            var topRight = ScreenTopRight;

            var screenWidth = Mathf.Abs(topRight.x - bottomLeft.x);

            var wOffset = screenWidth <= WidthOffset * 2 ? 0f : WidthOffset;
            var keyboardWidth = screenWidth - WidthOffset * 2;

            var left = bottomLeft.x + wOffset;
            var right = left + keyboardWidth;
            var bottom = bottomLeft.y + HeightOffset;

            var keyWidth = keyboardWidth / (float)NumTotalWhiteKeys;

            var scaleMultiplier = keyWidth / _key.Width;
            var curScale = _key.transform.localScale;
            _key.transform.localScale = new Vector3(curScale.x * scaleMultiplier, curScale.y * scaleMultiplier, curScale.z);

            var curPos = _key.transform.position;
            var posX = left + keyWidth * NumWhilteKeysBeforeThis + curPos.x - _key.BottomLeftPosition.x;
            var posY = bottom + curPos.y - _key.BottomLeftPosition.y;
            _key.transform.position = new Vector3(posX, posY, curPos.z);
        }
    }
}
