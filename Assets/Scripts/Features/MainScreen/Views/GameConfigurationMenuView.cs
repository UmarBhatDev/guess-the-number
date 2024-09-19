using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GTN.ViewSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GTN.Features.MainScreen
{
    public class GameConfigurationMenuView : MonoBehaviour, IDisposable
    {
        private const float SHOW_HIDE_TIME = 0.5f;

        public event Action OnBackButtonPressed;
        public event Action<(int from, int to), (int aiPlayers, int realPlayers)> OnPlayButtonPressed;

        private ViewState ViewState { get; set; }
        
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private CanvasGroup _menuCanvasGroup;
        [SerializeField] private RectTransform _backButtonTransform;
        [SerializeField] private RectTransform _playButtonTransform;
        [SerializeField] private RectTransform _randomRangeConfigurationPanelTransform;
        [SerializeField] private RectTransform _playersConfigurationPanelViewTransform;
        [SerializeField] private RandomRangeConfigurationPanel _randomRangeConfigurationPanel;
        [SerializeField] private PlayersConfigurationPanelView _playersConfigurationPanelView;

        private Sequence _showSequence;
        private Sequence _hideSequence;
        
        private float _backButtonHidePosY;
        private float _playButtonHidePosY;
        private float _playersConfigurationPanelHidePosY;
        private float _randomRangeConfigurationPanelHidePosY;

        private float _backButtonStartPosY;
        private float _playButtonStartPosY;
        private float _playersConfigurationPanelStartPosY;
        private float _randomRangeConfigurationPanelStartPosY;
        
        private void Start()
        {
            ViewState = ViewState.Hidden;

            _playButton.interactable = false;
            
            _backButtonHidePosY = Screen.height / 2f + _backButtonTransform.sizeDelta.y / 2;
            _playButtonHidePosY = -Screen.height / 2f - _playButtonTransform.sizeDelta.y / 2;
            _playersConfigurationPanelHidePosY = Screen.height / 2f + _playersConfigurationPanelViewTransform.sizeDelta.y / 2;
            _randomRangeConfigurationPanelHidePosY = Screen.height / 2f + _randomRangeConfigurationPanelTransform.sizeDelta.y / 2;
            
            _backButtonStartPosY = _backButtonTransform.localPosition.y;
            _playButtonStartPosY = _playButtonTransform.localPosition.y;
            _playersConfigurationPanelStartPosY = _randomRangeConfigurationPanelTransform.localPosition.y;
            _randomRangeConfigurationPanelStartPosY = _playersConfigurationPanelViewTransform.localPosition.y;

            _backButton.onClick.AddListener(() => OnBackButtonPressed?.Invoke());
            _playButton.onClick.AddListener(() 
                => OnPlayButtonPressed?.Invoke(_randomRangeConfigurationPanel.GetInputData(), 
                    _playersConfigurationPanelView.GetInputData()));

            _playersConfigurationPanelView.OnAvailabilityChanged +=
                isAvailable => _playButton.interactable = isAvailable;
            
            _randomRangeConfigurationPanelTransform.localPosition = new Vector3(_randomRangeConfigurationPanelTransform.transform.localPosition.x,
                _randomRangeConfigurationPanelHidePosY, 
                _randomRangeConfigurationPanelTransform.position.z);
            
            _playersConfigurationPanelViewTransform.localPosition = new Vector3(_playersConfigurationPanelViewTransform.transform.localPosition.x,
                _playersConfigurationPanelHidePosY, 
                _playersConfigurationPanelViewTransform.position.z);

            _backButtonTransform.localPosition = new Vector3(_backButtonTransform.localPosition.x,
                _backButtonHidePosY, _backButtonTransform.localPosition.z);
            
            _playButtonTransform.localPosition = new Vector3(_playButtonTransform.localPosition.x,
                _playButtonHidePosY , _playButtonTransform.localPosition.z);
        }
        
        public async UniTask Show()
        {
            _showSequence = DOTween.Sequence();
            
            if (ViewState is ViewState.Showing or ViewState.Shown or ViewState.Destroyed) return;

            if (ViewState is ViewState.Hiding) _hideSequence?.Kill();
            
            ViewState = ViewState.Showing;

            SetMenuVisible(true);
            SetMenuInteractable(false);

            var backButtonShowTween = _backButtonTransform
                .DOLocalMoveY( _backButtonStartPosY, SHOW_HIDE_TIME).SetEase(Ease.OutCubic);
            var randomRangeConfigurationPanelShowTween = _randomRangeConfigurationPanelTransform.transform
                .DOLocalMoveY( _playersConfigurationPanelStartPosY, SHOW_HIDE_TIME).SetEase(Ease.OutCubic);
            var playersConfigurationPanelShowTween = _playersConfigurationPanelViewTransform.transform
                .DOLocalMoveY( _randomRangeConfigurationPanelStartPosY, SHOW_HIDE_TIME).SetEase(Ease.OutCubic);
            var playButtonShowTween = _playButtonTransform
                .DOLocalMoveY(_playButtonStartPosY, SHOW_HIDE_TIME).SetEase(Ease.OutCubic);

            await _showSequence.Join(playButtonShowTween)
                .Join(playersConfigurationPanelShowTween)
                .Join(randomRangeConfigurationPanelShowTween)
                .Join(backButtonShowTween);
            
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

            var backButtonHideTween = _backButtonTransform
                .DOLocalMoveY( _backButtonHidePosY, SHOW_HIDE_TIME).SetEase(Ease.InCubic);
            var randomRangeConfigurationPanelHideTween = _randomRangeConfigurationPanelTransform.transform
                .DOLocalMoveY( _backButtonHidePosY, SHOW_HIDE_TIME).SetEase(Ease.InCubic);
            var playersConfigurationPanelHideTween = _playersConfigurationPanelViewTransform.transform
                .DOLocalMoveY( _backButtonHidePosY, SHOW_HIDE_TIME).SetEase(Ease.InCubic);
            var playButtonHideTween = _playButtonTransform
                .DOLocalMoveY(_playButtonHidePosY, SHOW_HIDE_TIME).SetEase(Ease.InCubic);

            await _hideSequence.Join(backButtonHideTween)
                .Join(randomRangeConfigurationPanelHideTween)
                .Join(playersConfigurationPanelHideTween)
                .Join(playButtonHideTween);
            
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