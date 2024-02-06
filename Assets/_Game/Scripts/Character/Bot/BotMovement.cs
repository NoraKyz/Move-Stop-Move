using _Pattern;
using _Pattern.Pool.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Character.Bot
{
    public class BotMovement : GameUnit, IBotMovement
    {
        #region Config
        
        [Header("References")]
        [SerializeField] private NavMeshAgent navMeshAgent;
        
        [Header("Config")]
        [SerializeField] private float moveSpeed = 5f;
        
        [Header("Validation")]
        [SerializeField] private bool isFailedConfig;
        
        private Vector3 _destination;
        
        public bool IsDestination => Vector3.Distance(TF.position, _destination + (TF.position.y - _destination.y) * Vector3.up) < 0.1f;

#if UNITY_EDITOR
        private void OnValidate() 
        {
            Common.Warning(navMeshAgent != null, this, "Missing reference: navMeshAgent");
            isFailedConfig = navMeshAgent == null;
        }
#endif
        
        #endregion
        
        #region Init
        
        private void OnEnable() 
        {
            if (isFailedConfig)
            {
                return;
            }
            
            OnInit();
        }
        private void OnInit()
        {
            navMeshAgent.speed = moveSpeed;
        }
        
        #endregion

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