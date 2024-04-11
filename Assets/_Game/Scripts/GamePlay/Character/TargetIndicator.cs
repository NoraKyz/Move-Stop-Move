using _SDK.Pool.Scripts;
using _SDK.UI.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Character
{
    public class TargetIndicator : PoolUnit
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private Image iconImg;
        [SerializeField] private Image directImg;
        [SerializeField] private RectTransform direct;
        [SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private TextMeshProUGUI scoreTxt;

        [SerializeField] private CanvasGroup canvasGroup;

        private Transform _target;
        private Vector3 _screenHalf = new Vector2(Screen.width, Screen.height) / 2; 

        private Vector3 _viewPoint;

        private Vector2 _viewPointX = new Vector2(0.075f, 0.925f);
        private Vector2 _viewPointY = new Vector2(0.05f, 0.85f);
    
        private Vector2 _viewPointInCameraX = new Vector2(0.075f, 0.925f);
        private Vector2 _viewPointInCameraY = new Vector2(0.05f, 0.95f);

        private UnityEngine.Camera Camera => UnityEngine.Camera.main;

        private bool IsInCamera => _viewPoint.x > _viewPointInCameraX.x && _viewPoint.x < _viewPointInCameraX.y && _viewPoint.y > _viewPointInCameraY.x && _viewPoint.y < _viewPointInCameraY.y;

        private Player.Player _player;

        private void LateUpdate()
        {
            _viewPoint = Camera.WorldToViewportPoint(_target.position);
            direct.gameObject.SetActive(!IsInCamera);
            nameTxt.gameObject.SetActive(IsInCamera);
            
            if(_viewPoint.z < 0)
            {
                _viewPoint *= -1;
            }

            _viewPoint.x = Mathf.Clamp(_viewPoint.x, _viewPointX.x, _viewPointX.y);
            _viewPoint.y = Mathf.Clamp(_viewPoint.y, _viewPointY.x, _viewPointY.y);

            Vector3 targetSPoint = Camera.ViewportToScreenPoint(_viewPoint) - _screenHalf;
            Vector3 playerSPoint = Camera.WorldToScreenPoint(_player.TF.position) - _screenHalf;      
            rect.anchoredPosition = targetSPoint;

            direct.up = (targetSPoint - playerSPoint).normalized;
        }

        private void OnInit()
        {
            if (_player == null)
            {
                _player = CharacterManager.Ins.Player;
            }
            
            SetScore(0);
            SetColor(new Color(Random.value, Random.value, Random.value, 1));
            SetAlpha(GameManager.IsState(GameState.GamePlay) ? 1 : 0);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            OnInit();
        }

        public void SetScore(int score)
        {
            scoreTxt.SetText(score.ToString());
        }

        public void SetName(string nName)
        {
            nameTxt.SetText(nName);
        }

        private void SetColor(Color color)
        {
            iconImg.color = color;
            nameTxt.color = color;
        }

        public void SetAlpha(float alpha)
        {
            canvasGroup.alpha = alpha;
        }
    }
}
