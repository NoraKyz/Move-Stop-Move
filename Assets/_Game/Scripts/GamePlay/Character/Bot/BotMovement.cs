using _SDK.Pool.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.GamePlay.Character.Bot
{
    public class BotMovement : GameUnit
    {
        #region Config
        
        [Header("References")]
        [SerializeField] private NavMeshAgent navMeshAgent;
        
        [Header("Config")]
        [SerializeField] private float moveSpeed = 5f;
        
        private Vector3 _destination;
        public bool IsDestination => Vector3.Distance(TF.position, _destination + (TF.position.y - _destination.y) * Vector3.up) < 0.1f;

        #endregion

        public void OnInit()
        {
            navMeshAgent.speed = moveSpeed;
        }

        public void MoveToPosition(Vector3 position)
        {
            _destination = position;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(_destination);
        }
        
        public void StopMove()
        {
            navMeshAgent.enabled = false;
        }
    }
}