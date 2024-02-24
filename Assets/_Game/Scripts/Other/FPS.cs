#if UNITY_EDITOR

using UnityEngine;

namespace _Game.Scripts.Other
{
    public class FPS : MonoBehaviour
    {
        public float updateInterval = 0.5f;

        private float _accum = 0;
        private int _frames = 0;
        private float _timeLeft = 0;
        private float _fps = 0;

        private GUIStyle _textStyle = new GUIStyle();

        private void Start()
        {
            _timeLeft = updateInterval;

            _textStyle.fontSize = 60;
            _textStyle.fontStyle = FontStyle.Bold;
            _textStyle.normal.textColor = Color.white;
        }

        private void Update()
        {
            _timeLeft -= Time.deltaTime;
            _accum += Time.timeScale / Time.deltaTime;
            _frames++;

            if (_timeLeft <= 0f)
            {
                _fps = _accum / _frames;
                _timeLeft = updateInterval;
                _accum = 0;
                _frames = 0;
            }
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(5, 5, 100, 25), _fps.ToString("F2") + " FPS", _textStyle);
        }
    }
}

#endif