using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PianoToRhythmGame
{
    public class MidiInputDebugger : MonoBehaviour
    {
        [SerializeField]
        MidiInputIndicator _indicatorPrefab;

        [SerializeField]
        float _indicatorMinHeight = 1f;

        [SerializeField]
        float _indicatorMaxHeight = 10f;

        const int NUM_NOTES = 128;

        Vector3 ScreenBottomLeft
        {
            get => Camera.main.ScreenToWorldPoint(Vector3.zero);
        }

        Vector3 ScreenTopRight
        {
            get => Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializeIndicators();
        }

        void InitializeIndicators()
        {
            var bottomLeft = ScreenBottomLeft;
            var topRight = ScreenTopRight;

            var bottom = bottomLeft.y;
            var left = bottomLeft.x;
            var right = topRight.x;
            var top = topRight.y;

            var width = Mathf.Abs(left - right) / (float)NUM_NOTES;
            var height = _indicatorMinHeight;

            for (var noteIndex = 0; noteIndex < NUM_NOTES; noteIndex++)
            {
                var posX = left + width * noteIndex + width / 2.0f;
                var posY = bottom + height / 2.0f;
                var pos = new Vector3(posX, posY, 0f);

                var indicator = Instantiate(_indicatorPrefab, pos, Quaternion.identity) as MidiInputIndicator;
                indicator.NoteNumber = noteIndex;
                indicator.Width = width;
                indicator.MaxHeight = _indicatorMaxHeight;
                indicator.MinHeight = _indicatorMinHeight;
            }
        }
    }
}
