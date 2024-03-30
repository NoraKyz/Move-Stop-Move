using System;
using UnityEngine;

namespace _Game.Scripts.Sound
{
    [Serializable]
    public class SoundData
    {
        public AudioClip audioClip;
        public SoundType soundType;
    }

    public enum SoundType
    {
        None = 0,
        ClickButton = 1,
    }
}