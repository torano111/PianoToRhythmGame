using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using PianoToRhythmGame.Utility;

namespace PianoToRhythmGame.Piano
{
    public class PianoKeyboard : MonoBehaviour
    {
        [SerializeField]
        PianoKey _whiteKeyPrefab, _blackKeyPrafab;


        // todo make ReactiveProperty of PianoKeyboardType and use it so a keyboard can be changed dynamically.
        [SerializeField]
        PianoKeyboardType _keyboardType = PianoKeyboardType.Key88;

        [SerializeField]
        FloatReactiveProperty _keyboardWidthOffset = new FloatReactiveProperty(0f);

        [SerializeField]
        FloatReactiveProperty _keyboardHeightOffset = new FloatReactiveProperty(0f);

        [SerializeField]
        FloatReactiveProperty _spaceBtwKeys = new FloatReactiveProperty(0f);

        List<PianoKey> _keys = new List<PianoKey>();
        List<PianoKeyPlacer> _keyPlacers = new List<PianoKeyPlacer>();

        // Start is called before the first frame update
        void Start()
        {
            InitKeyboard();
        }

        void InitKeyboard()
        {
            BuildKeyboard(_keyboardType);
        }

        void BuildKeyboard(PianoKeyboardType keyboardType)
        {
            var numKeys = 0;
            var offset = 0;
            var startNoteNumber = 0;

            switch (keyboardType)
            {
                case PianoKeyboardType.Key88:
                    numKeys = 88;
                    offset = 3;
                    startNoteNumber = 21;
                    break;
                case PianoKeyboardType.Key61:
                    numKeys = 61;
                    offset = 0;
                    startNoteNumber = 36;
                    break;
                default:
                    throw new NotImplementedException();
            }

            BuildKeyboard(numKeys, offset, startNoteNumber);
        }

        /// <summary>
        /// build a keyboard
        /// </summary>
        /// <param name = "numKeys"> number of keys of a keyboard </param>
        /// <param name = "offset"> number of keys before the first Do. e.g. 3 if the most left key is La. </param>
        /// <param name = "startNoteNumber"> the first note number which used to take an input from MidiMaster. </param>
        void BuildKeyboard(int numKeys, int offset, int startNoteNumber)
        {
            _keys.Clear();
            _keyPlacers.Clear();

            var octaveLength = PianoUtility.NumKeysInOctave;
            var keyIndex = octaveLength - offset;
            var numWhiteKeys = 0;

            for (var i = 0; i < numKeys; i++, keyIndex++)
            {
                var keyColor = PianoUtility.GetPianoKeyColor(keyIndex);

                PianoKey key;
                switch (keyColor)
                {
                    case PianoKeyColor.White:
                        key = InstantiateKey(_whiteKeyPrefab);
                        break;
                    case PianoKeyColor.Black:
                        key = InstantiateKey(_blackKeyPrafab);
                        break;
                    default:
                        throw new ArgumentException();
                }

                key.NoteNumber = startNoteNumber + i;
                key.transform.SetParent(this.transform);

                var keyPlacer = key.gameObject.GetOrAddComponent<PianoKeyPlacer>();
                keyPlacer.NumWhilteKeysBeforeThis = numWhiteKeys;
                keyPlacer.KeyColor = keyColor;

                _keyboardWidthOffset.Subscribe(widthOffset => keyPlacer.WidthOffset = widthOffset).AddTo(keyPlacer);
                _keyboardHeightOffset.Subscribe(heightOffset => keyPlacer.HeightOffset = heightOffset).AddTo(keyPlacer);

                _keys.Add(key);
                _keyPlacers.Add(keyPlacer);

                if (keyColor == PianoKeyColor.White)
                {
                    numWhiteKeys++;
                }
            }

            for (var i = 0; i < _keyPlacers.Count; i++)
            {
                var keyPlacer = _keyPlacers[i];
                keyPlacer.NumTotalWhiteKeys = numWhiteKeys;
            }
        }

        PianoKey InstantiateKey(PianoKey pianoKeyPrefab)
        {
            var key = Instantiate(pianoKeyPrefab) as PianoKey;
            return key;
        }
    }
}
