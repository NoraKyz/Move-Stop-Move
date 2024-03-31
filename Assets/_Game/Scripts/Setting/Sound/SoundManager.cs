using System.Collections.Generic;
using _Game.Scripts.Data;
using _SDK.ServiceLocator.Scripts;
using UnityEngine;

namespace _Game.Scripts.Setting.Sound
{
    public class SoundManager : GameService
    {
         [Header("References")]
         [SerializeField] private AudioSource audioSource;
         
         [Header("Config")]
         [SerializeField] private List<SoundData> listSounds = new();
        
         private Dictionary<SoundType, SoundData> _sounds = new();
         
         private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;

         private void Awake()
         {
             for (int i = 0; i < listSounds.Count; i++)
             {
                 _sounds.Add(listSounds[i].soundType, listSounds[i]);
             }
         }
         
         public void Play(SoundType soundType)
         {
             if (PlayerData.IsSound == 0)
             {
                 return;
             }
             
             if (_sounds.TryGetValue(soundType, out var soundData))
             {
                 audioSource.PlayOneShot(soundData.audioClip, audioSource.volume);
             }
         }
    }
}