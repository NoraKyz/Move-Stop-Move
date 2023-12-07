using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Character.Bot
{
    public class Bot : Character
    {
        [Header("Components")] 
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private GameObject circleTargetIndicator;
        
        private Vector3 _destination;
        public bool IsDestination => Vector3.Distance(TF.position, _destination + (TF.position.y - _destination.y) * Vector3.up) < 0.1f;
        
        public void ShowCircleTargetIndicator()
        {
            circleTargetIndicator.SetActive(true);
        }

        public void HideCircleTargetIndicator()
        {
            circleTargetIndicator.SetActive(false);
        }
    }
}
