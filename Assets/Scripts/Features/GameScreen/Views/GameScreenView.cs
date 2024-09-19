using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GTN.Base.Data;
using GTN.ViewSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GTN.Features.GameScreen
{
    public class GameScreenView : MonoBehaviour, IDisposable
    {
        private const float SHOW_HIDE_TIME = 0.2f;
        private const float COIN_ROTATION_TIME = 1f;

        public event Action OnCloseButtonClicked;

        private ViewState ViewState { get; set; }

        [field: SerializeField] public RectTransform AvatarsTransform { get; private set; }

        [SerializeField] private Button _homeButton;
        [SerializeField] private CanvasGroup _menuCanvasGroup;
        [SerializeField] private RectTransform _homeButtonHoder;
        [SerializeField] private RectTransform _coinTransform;
        [SerializeField] private RectTransform _historyHolderTransform;
        [SerializeField] private HistoryBarView _historyBarView;
        [SerializeField] private TMP_Text _greaterLessText;
        [SerializeField] private TMP_Text _coinText;
        
        private Sequence _showSequence;
        private Sequence _hideSequence;
        
        private float _homeButtonStartPosY;
        private float _historyBarStartPosX;
        private float _avatarsBarStartPosY;
        
        private float _homeButtonHidePosY;
        private float _historyBarHidePosX;
        private float _avatarsBarHidePosY;

        [Inject] private ViewRegistry _viewRegistry;

        private void Awake()
        {
            ViewState = ViewState.Hidden;
            
            _homeButtonStartPosY = _homeButtonHoder.localPosition.y;
            _avatarsBarStartPosY = AvatarsTransform.localPosition.y;
            _historyBarStartPosX = _historyHolderTransform.localPosition.x;

            _homeButtonHidePosY = Screen.height / 2f + _homeButtonHoder.sizeDelta.y / 2f;
            _historyBarHidePosX = Screen.width / 2f + _historyHolderTransform.sizeDelta.x / 2f;
            _avatarsBarHidePosY = -Screen.height / 2f - AvatarsTransform.sizeDelta.y / 2f;

            _homeButtonHoder.localPosition = new Vector3(_homeButtonHoder.localPosition.x, 
                _homeButtonHidePosY, 
                _homeButtonHoder.localPosition.z);
            _historyHolderTransform.localPosition = new Vector3(_historyBarHidePosX, 
                _historyHolderTransform.localPosition.y, 
                _historyHolderTransform.localPosition.z);
            AvatarsTransform.localPosition = new Vector3(AvatarsTransform.localPosition.x, 
                _avatarsBarHidePosY, 
                AvatarsTransform.localPosition.z);

            _coinTransform.localScale = new Vector3(0, 0, 0);
        }

        private void Start()
            => _homeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());

        public async UniTask Show(CancellationToken cancellationToken)
        {
            _showSequence = DOTween.Sequence();
            
            if (ViewState is ViewState.Showing or ViewState.Shown or ViewState.Destroyed) return;

            if (ViewState is ViewState.Hiding) _hideSequence?.Kill();
            
            ViewState = ViewState.Showing;
            
            SetMenuVisible(true);
            SetMenuInteractable(false);

            await _showSequence.Join(_homeButtonHoder.DOLocalMoveY(_homeButtonStartPosY, SHOW_HIDE_TIME)
                    .SetEase(Ease.Linear))
                .Join(_historyHolderTransform.DOLocalMoveX(_historyBarStartPosX, SHOW_HIDE_TIME)
                    .SetEase(Ease.Linear))
                .Join(AvatarsTransform.DOLocalMoveY(_avatarsBarStartPosY, SHOW_HIDE_TIME)
                    .SetEase(Ease.Linear))
                .Join(_coinTransform.DOScale(new Vector3(1, 1, 1), SHOW_HIDE_TIME * 2)
                    .SetEase(Ease.InOutCubic))
                .Join(_coinTransform.DORotate(new Vector3(0, _coinTransform.localRotation.y + 1800), 
                        COIN_ROTATION_TIME, RotateMode.FastBeyond360)
                    .SetEase(Ease.InOutCubic))
                .WithCancellation(cancellationToken);
            
            ViewState = ViewState.Shown;
            
            SetMenuInteractable(true);
        }

        public async UniTask Hide(CancellationToken cancellationToken)
        {
            _hideSequence = DOTween.Sequence();

            if (ViewState is ViewState.Hidden or ViewState.Hiding or ViewState.Destroyed) return;
            
            if (ViewState is ViewState.Showing) _showSequence?.Kill();

            ViewState = ViewState.Hiding;
            
            SetMenuInteractable(false);

            await _hideSequence.Join(_homeButtonHoder.DOLocalMoveY(_homeButtonHidePosY, SHOW_HIDE_TIME)
                    .SetEase(Ease.Linear))
                .Join(_historyHolderTransform.DOLocalMoveX(_historyBarHidePosX, SHOW_HIDE_TIME)
                    .SetEase(Ease.Linear))
                .Join(AvatarsTransform.DOLocalMoveY(_avatarsBarHidePosY, SHOW_HIDE_TIME)
                    .SetEase(Ease.Linear))
                .Join(_coinTransform.DOScale(new Vector3(0, 0, 0), SHOW_HIDE_TIME * 2)
                    .SetEase(Ease.InOutCubic))
                .Join(_coinTransform.DORotate(new Vector3(0, _coinTransform.localRotation.y - 1800), 
                        COIN_ROTATION_TIME, RotateMode.FastBeyond360)
                    .SetEase(Ease.InOutCubic))
                .WithCancellation(cancellationToken);

            ViewState = ViewState.Hidden;

            SetMenuVisible(false);
        }

        public async UniTask RotateCoin(int inputNumber, int randomGuessedNumber, CancellationToken cancellationToken)
        {
            if (inputNumber == randomGuessedNumber)
                _coinText.text = randomGuessedNumber.ToString();
            
            await _coinTransform.DORotate(new Vector3(0, _coinTransform.localRotation.y + 1800), 
                    COIN_ROTATION_TIME, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutCubic)
                .AwaitForComplete(cancellationToken: cancellationToken);
        }

        public async UniTask UpdateHistory(int inputNumber, int randomGuessedNumber, CancellationToken cancellationToken)
        {
            if (inputNumber == randomGuessedNumber)
                _greaterLessText.text = "=";
            else if (inputNumber > randomGuessedNumber)
                _greaterLessText.text = ">";
            else
                _greaterLessText.text = "<";
            
            _historyBarView.AddNumberGuessElement(inputNumber);
        }
        
        private void SetMenuInteractable(bool interactable)
            => _menuCanvasGroup.interactable = interactable;

        private void SetMenuVisible(bool visible)
            => _menuCanvasGroup.alpha = visible ? 1 : 0;

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}