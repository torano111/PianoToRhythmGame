using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace PianoToRhythmGame.Piano
{
    [RequireComponent(typeof(PianoKey))]
    public class PianoKeyColorChanger : MonoBehaviour
    {
        [SerializeField]
        Color _colorOnPress;

        Color _initColor;

        PianoKey _key;

        void Awake()
        {
            _key = GetComponent<PianoKey>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _initColor = _key.KeyRenderer.color;
            _key.IsPressingReactiveProperty
                .DistinctUntilChanged()
                .Where(pressing => pressing)
                .Subscribe(_ =>
                {
                    _initColor = _key.KeyRenderer.color;
                    ChangeKeyColor(_colorOnPress);
                });

            _key.IsPressingReactiveProperty
                .DistinctUntilChanged()
                .Where(pressing => !pressing)
                .Subscribe(_ =>
                {
                    ChangeKeyColor(_initColor);
                });
        }

        void ChangeKeyColor(Color color)
        {
            _key.KeyRenderer.color = color;
        }
    }
}