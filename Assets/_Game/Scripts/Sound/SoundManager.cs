using System.Collections.Generic;
using _SDK.ServiceLocator.Scripts;
using UnityEngine;

namespace _Game.Scripts.Sound
{
    public class SoundManager : GameService
    {
         [SerializeField] private AudioSource audioSource;
         [SerializeField] private List<SoundData> listSounds = new();
        
         private Dictionary<SoundType, SoundData> _sounds = new();

         private void Awake()
         {
             for (int i = 0; i < listSounds.Count; i++)
             {
                 _sounds.Add(listSounds[i].soundType, listSounds[i]);
             }
         }
         
         public void Play(SoundType soundType)
         {
             if (_sounds.TryGetValue(soundType, out var soundData))
             {
                 audioSource.PlayOneShot(soundData.audioClip);
             }
         }
    }
}