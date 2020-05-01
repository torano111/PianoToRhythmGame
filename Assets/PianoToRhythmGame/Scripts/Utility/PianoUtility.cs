using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PianoToRhythmGame.Piano;

namespace PianoToRhythmGame.Utility
{
    public static class PianoUtility
    {
        public static int NumKeysInOctave => NumWhiteKeysInOctave + NumBlackKeysInOctave;
        public readonly static int NumWhiteKeysInOctave = 7;
        public readonly static int NumBlackKeysInOctave = 5;

        /// <summary>
        /// get the color of a key 
        /// <summary>
        /// <param name = "keyIndex"> number of a key starting from 0. e.g. if 3 is given, then it is Re #, so return PianoKeyColor.Black </param>
        public static PianoKeyColor GetPianoKeyColor(int keyIndex)
        {
            keyIndex = keyIndex % NumKeysInOctave;

            PianoKeyColor result;

            switch (keyIndex)
            {
                // do
                case 0:
                default:
                    result = PianoKeyColor.White;
                    break;
                // do #
                case 1:
                    result = PianoKeyColor.Black;
                    break;
                // re
                case 2:
                    result = PianoKeyColor.White;
                    break;
                // re #
                case 3:
                    result = PianoKeyColor.Black;
                    break;
                // mi
                case 4:
                    result = PianoKeyColor.White;
                    break;
                // fa
                case 5:
                    result = PianoKeyColor.White;
                    break;
                // fa #
                case 6:
                    result = PianoKeyColor.Black;
                    break;
                // sol
                case 7:
                    result = PianoKeyColor.White;
                    break;
                // sol #
                case 8:
                    result = PianoKeyColor.Black;
                    break;
                // la
                case 9:
                    result = PianoKeyColor.White;
                    break;
                // la #
                case 10:
                    result = PianoKeyColor.Black;
                    break;
                // ti
                case 11:
                    result = PianoKeyColor.White;
                    break;
            }

            return result;
        }
    }
}
