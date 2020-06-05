using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using PianoToRhythmGame.Utility;
using System.Linq;
using PianoToRhythmGame.Game;

namespace PianoToRhythmGame.Piano
{
    public class PianoKeyboard : MonoBehaviour
    {
        const int NoteLength = 128;

        [SerializeField]
        ScreenIndicator _screenIndicator;

        [SerializeField]
        PianoKey _whiteKeyPrefab, _blackKeyPrafab;

        // todo make ReactiveProperty of PianoKeyboardType and use it so a keyboard can be changed dynamically.
        [SerializeField]
        PianoKeyboardType _keyboardType = PianoKeyboardType.Key88;

        [SerializeField]
        float _keyboardWidthOffset = 0.09f;

        [SerializeField]
        float _keyboardHeightOffset = 0.05f;

        [SerializeField]
        float _spaceBtwKeys = 0f;

        // note number and piano key
        Dictionary<int, PianoKey> _keys = new Dictionary<int, PianoKey>();

        public int NumKeys => _keys.Count;
        public int FirstNoteNumber => _keys.Count > 0 ? _keys.First().Key : -1;

        Subject<Unit> _onBuildKeyboard => new Subject<Unit>();
        public IObservable<Unit> OnBuildKeyboard => _onBuildKeyboard;

        public PianoKey GetKey(int noteNumber)
        {
            if (_keys.TryGetValue(noteNumber, out var key))
            {
                return key;
            }

            return null;
        }

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
            // todo destroy old keys

            // initialize transform before building a keyboard
            transform.localScale = Vector3.one;
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            var octaveLength = PianoUtility.NumKeysInOctave;
            var startKeyIndex = octaveLength - offset;
            var numWhiteKeys = 0;

            var numTotalWhiteKeys = 0;
            for (var i = 0; i < numKeys; i++)
            {
                var keyColor = PianoUtility.GetPianoKeyColor(startKeyIndex + i);

                if (keyColor == PianoKeyColor.White)
                {
                    numTotalWhiteKeys++;
                }
            }

            PianoKey firstKey = null;
            var sumWhiteKeyWidths = 0f;
            for (var i = 0; i < numKeys; i++)
            {
                var noteNumber = startNoteNumber + i;

                if (noteNumber >= NoteLength)
                {
                    break;
                }

                var keyColor = PianoUtility.GetPianoKeyColor(startKeyIndex + i);

                if (i == 0 && keyColor != PianoKeyColor.White)
                {
                    Debug.LogAssertion("The first key should be white in order to calculate black keys' position and scale properly.");
                }

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

                if (i == 0)
                {
                    firstKey = key;
                }

                key.NoteNumber = noteNumber;
                key.transform.SetParent(this.transform);


                // make it sure that it is the unit scale.
                key.transform.localScale = Vector3.one;

                float posX, posY;
                if (keyColor == PianoKeyColor.White)
                {
                    posX = (key.KeyCollider.bounds.size.x + _spaceBtwKeys) * numWhiteKeys + key.PositionOffset.x + _keyboardWidthOffset;
                    posY = key.PositionOffset.y + _keyboardHeightOffset;

                    sumWhiteKeyWidths += key.KeyCollider.bounds.size.x + _spaceBtwKeys;
                }
                else
                {
                    posX = (firstKey.KeyCollider.bounds.size.x + _spaceBtwKeys) * numWhiteKeys + key.PositionOffset.x + _keyboardWidthOffset - key.KeyCollider.bounds.size.x / 2.0f - _spaceBtwKeys / 2.0f;
                    posY = key.PositionOffset.y + _keyboardHeightOffset + firstKey.KeyCollider.bounds.size.y - key.KeyCollider.bounds.size.y;
                }

                key.transform.position = new Vector3(posX, posY, 0f);

                key.transform.SetParent(this.transform);

                _keys.Add(noteNumber, key);

                if (keyColor == PianoKeyColor.White)
                {
                    numWhiteKeys++;
                }
            }

            // scale keyboard so it fits screen
            var scaleMultiplierX = _screenIndicator.Width / (sumWhiteKeyWidths + _keyboardWidthOffset * 2);

            var keyHeight = _screenIndicator.Height / 5.0f;
            var scaleMultiplierY = keyHeight / firstKey.KeyCollider.bounds.size.y;

            var keyboardScale = transform.localScale;
            keyboardScale.x *= scaleMultiplierX;
            keyboardScale.y *= scaleMultiplierY;

            this.transform.localScale = keyboardScale;

            // locate keyboard
            var keyboardPosX = _screenIndicator.Center.x - _screenIndicator.Width / 2.0f;
            var keyboardPosY = _screenIndicator.Center.y - _screenIndicator.Height / 2.0f;

            this.transform.position = new Vector3(keyboardPosX, keyboardPosY, 0f);

            // finally set keyboard's parent to screen indicator
            this.transform.SetParent(_screenIndicator.transform);

            _onBuildKeyboard.OnNext(Unit.Default);
        }

        PianoKey InstantiateKey(PianoKey pianoKeyPrefab)
        {
            var key = Instantiate(pianoKeyPrefab) as PianoKey;
            return key;
        }
    }
}
