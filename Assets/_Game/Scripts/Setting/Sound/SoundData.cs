using System;
using UnityEngine;

namespace _Game.Scripts.Setting.Sound
{
    [Serializable]
    public class SoundData
    {
        public AudioClip audioClip;
        public SoundType soundType;
        [Range(0f, 1f)]
        public float volume;
    }

    public enum SoundType
    {
        None = 0,
        ClickButton = 1,
        SizeUp = 2,
        WeaponHit = 3,
        WeaponThrow = 4,
        Dead = 5,
        Win = 6,
        Lose = 7,
        BombExplosion = 8,
    }
}