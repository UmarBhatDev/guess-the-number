using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GTN.ViewSystem;
using UnityEngine;
using UnityEngine.UI;
using Screen = UnityEngine.Device.Screen;

namespace GTN.Features.MainScreen
{
    public class MainMenuView : MonoBehaviour, IDisposable
    {
        private const float SHOW_HIDE_TIME = 0.5f;
        
        public event Action OnPlayButtonPressed;
        public event Action OnExitButtonPressed;

        private ViewState ViewState { get; set; }
        
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private CanvasGroup _menuCanvasGroup;
        
        private Sequence _showSequence;
        private Sequence _hideSequence;
        
        private float _buttonsHidePosition;

        private float _exitButtonStartPosY;
        private float _playButtonStartPosY;
        
        private void Awake()
        {
            ViewState = ViewState.Shown;
            
            SetMenuInteractable(true);
            
            _buttonsHidePosition = Screen.height / 2f;
            _exitButtonStartPosY = _exitButton.transform.localPosition.y;
            _playButtonStartPosY = _playButton.transform.localPosition.y;

            _playButton.onClick.AddListener(() => OnPlayButtonPressed?.Invoke());
            _exitButton.onClick.AddListener(() => OnExitButtonPressed?.Invoke());
        }
        
        public async UniTask Show()
        {
            _showSequence = DOTween.Sequence();
            
            if (ViewState is ViewState.Showing or ViewState.Shown or ViewState.Destroyed) return;

            if (ViewState is ViewState.Hiding) _hideSequence?.Kill();
            
            ViewState = ViewState.Showing;

            SetMenuVisible(true);
            SetMenuInteractable(false);

            var exitButtonShowTween = _exitButton.transform
                .DOLocalMoveY( _exitButtonStartPosY, SHOW_HIDE_TIME).SetEase(Ease.InCubic);
            var playButtonShowTween = _playButton.transform
                .DOLocalMoveY(_playButtonStartPosY, SHOW_HIDE_TIME).SetEase(Ease.InCubic);

            await _showSequence.Join(playButtonShowTween).Join(exitButtonShowTween.SetDelay(0.1f));
            
            ViewState = ViewState.Shown;
            
            SetMenuInteractable(true);
        }
        
        public async UniTask Hide()
        {
            _hideSequence = DOTween.Sequence();

            if (ViewState is ViewState.Hidden or ViewState.Hiding or ViewState.Destroyed) return;
            
            if (ViewState is ViewState.Showing) _showSequence?.Kill();

            ViewState = ViewState.Hiding;
            
            SetMenuInteractable(false);

            var exitButtonHideTween = _exitButton.transform
                .DOLocalMoveY( -_buttonsHidePosition - _exitButton.image.rectTransform.sizeDelta.y / 2, SHOW_HIDE_TIME).SetEase(Ease.OutCubic);
            var playButtonHideTween = _playButton.transform
                .DOLocalMoveY(-_buttonsHidePosition - _playButton.image.rectTransform.sizeDelta.y / 2, SHOW_HIDE_TIME).SetEase(Ease.OutCubic);

            await _hideSequence.Join(exitButtonHideTween).Join(playButtonHideTween.SetDelay(0.1f));
            
            ViewState = ViewState.Hidden;

            SetMenuVisible(false);
        }

        private void SetMenuInteractable(bool interactable)
            => _menuCanvasGroup.interactable = interactable;

        private void SetMenuVisible(bool visible)
            => _menuCanvasGroup.alpha = visible ? 1 : 0;
        
        public void Dispose()
        {
            ViewState = ViewState.Destroyed;
            Destroy(gameObject);
        }
    }
}
