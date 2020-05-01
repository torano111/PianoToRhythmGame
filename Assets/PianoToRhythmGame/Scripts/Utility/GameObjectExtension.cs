using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PianoToRhythmGame.Utility
{
    public static class GameObjectExtension
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var t = gameObject.GetComponent<T>();

            if (t != null)
            {
                return t;
            }

            return gameObject.AddComponent<T>();
        }
    }
}
