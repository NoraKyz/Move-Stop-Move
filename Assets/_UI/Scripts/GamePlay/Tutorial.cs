using UnityEngine;

namespace _UI.Scripts.GamePlay
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Transform hand;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float amplitude = 2f;
        
        private float _timer;
        private Vector3 _newPos;
        
        void Update()
        {
            MoveInInfinityShape();
        }

        void MoveInInfinityShape()
        {
            _timer += Time.deltaTime;
            
            float x = amplitude * Mathf.Sin( speed * _timer);
            float y = amplitude * Mathf.Cos(2 * speed * _timer) / 2;

            Vector3 currentPos = hand.position;
            
            _newPos.Set(currentPos.x + x, currentPos.y + y, currentPos.z);

            hand.position = _newPos;
        }
    }
}