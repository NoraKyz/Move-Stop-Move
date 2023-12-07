using System;
using _Framework.Pool.Scripts;
using _UI.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class CharacterInfor : GameUnit
    {
        private Transform _mainCamera;
        private void OnEnable()
        {
            _mainCamera = GameManager.Instance.MainCamera;
        }

        private void LateUpdate()
        {
            Vector3 lookDirection = TF.position - _mainCamera.position;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);

            TF.rotation = Quaternion.Lerp(TF.rotation, rotation, Time.deltaTime * 10f);
        }
    }
}
