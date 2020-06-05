using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace PianoToRhythmGame.Game
{
    [DefaultExecutionOrder(-1)]
    public class ScreenIndicator : MonoBehaviour
    {
        Vector3 MainCameraScreenBottomLeft
        {
            get => Camera.main.ScreenToWorldPoint(Vector3.zero);
        }

        Vector3 MainCameraScreenTopRight
        {
            get => Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        }

        public Vector3 Center { get; private set; }
        public float Height { get; private set; }
        public float Width { get; private set; }

        void Awake()
        {
            UpdateScreenIndicator();
        }

        void Update()
        {
            UpdateScreenIndicator();
        }

        void UpdateScreenIndicator()
        {
            var topRight = MainCameraScreenTopRight;
            var bottomLeft = MainCameraScreenBottomLeft;

            var centerPos = (topRight + bottomLeft) / 2.0f;
            centerPos.z = 0f;

            // Vector2 topRightCorner = new Vector2(1, 1);
            // Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);

            // var height = edgeVector.y * 2;
            // var width = edgeVector.x * 2;

            var height = Camera.main.orthographicSize * 2f;
            float width = Screen.width * height / Screen.height;

            SetScreenInfo(centerPos, width, height);

            this.transform.position = Center;
            var scale = new Vector3(Width, Height, 1f);
            this.transform.localScale = scale;
        }

        void SetScreenInfo(Vector3 center, float width, float height)
        {
            this.Center = center;
            this.Height = Mathf.Abs(height);
            this.Width = Mathf.Abs(width);

            // Debug.Log($"w: {this.Width}, h: {this.Height}");
        }
    }
}