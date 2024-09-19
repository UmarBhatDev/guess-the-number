using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GTN.ViewSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace GTN.Features.InGameKeyboard
{
    //initially there also should be an abstraction between player and keyboard, so that we could use different implementations of keyboard if we want
    public class InGameKeyboardView : MonoBehaviour
    {
        private const float SHOW_HIDE_TIME = 0.5f;
        
        public event Action<int> OnSubmitPressed;
        private ViewState ViewState { get; set; }

        [SerializeField] private Button _clearButton;
        [SerializeField] private Button _minusButton;
        [SerializeField] private Button _submitButton;
        [SerializeField] private TMP_Text _inputFieldText;
        [SerializeField] private CanvasGroup _menuCanvasGroup;
        [SerializeField] private RectTransform _keyBoardTransform;
        [SerializeField] private List<InGameKeyboardKeyView> _inGameKeyboardKeysViews;

        private Sequence _showSequence;
        private Sequence _hideSequence;
        
        private float _keyBoardStartPosY;
        private float _keyBoardHidePosY;
        
        private void Awake()
        {
            ViewState = ViewState.Hidden;
            _keyBoardStartPosY = _keyBoardTransform.localPosition.y;
            _keyBoardHidePosY = -Screen.height / 2f - _keyBoardTransform.sizeDelta.y / 2f;

            _keyBoardTransform.localPosition = new Vector3(_keyBoardTransform.localPosition.x, _keyBoardHidePosY,
                _keyBoardTransform.localPosition.z);
        }

        private void Start()
        {
            _minusButton.onClick.AddListener(MinusKeyPressed);
            _clearButton.onClick.AddListener(ClearInputField);
            _submitButton.onClick.AddListener(SubmitClicked);

            foreach (var inGameKeyboardKeysView in _inGameKeyboardKeysViews)
                inGameKeyboardKeysView.OnInGameKeyPressed += InGameKeyPressed;
        }

        public async UniTask Show(CancellationToken cancellationToken)
        {
            ShuffleKeyboard();

            _showSequence = DOTween.Sequence();
            
            if (ViewState is ViewState.Showing or ViewState.Shown or ViewState.Destroyed) return;

            if (ViewState is ViewState.Hiding) _hideSequence?.Kill();
            
            ViewState = ViewState.Showing;
            
            SetMenuVisible(true);
            SetMenuInteractable(false);

            await _showSequence.Join(_keyBoardTransform.DOLocalMoveY(_keyBoardStartPosY, SHOW_HIDE_TIME)
                    .SetEase(Ease.OutCubic))
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

            await _hideSequence.Join(_keyBoardTransform.DOLocalMoveY(_keyBoardHidePosY, SHOW_HIDE_TIME)
                    .SetEase(Ease.InCubic))
                .WithCancellation(cancellationToken);
            
            ViewState = ViewState.Hidden;

            SetMenuVisible(false);
        }

        private void InGameKeyPressed(string inputString)
        {
            int inputNumber;
            int currentNumber;

            if (int.TryParse(inputString, out var inputNumberParsed))
                inputNumber = inputNumberParsed;
            else return;
                    
            if (int.TryParse(_inputFieldText.text, out var currentNumberParsed)) 
                currentNumber = currentNumberParsed;
            else
            {
                ClearInputField();
                currentNumber = 0;
            }

            var newNumber = currentNumber * 10 + inputNumber;

            _inputFieldText.text = newNumber.ToString();
        }
        
        private void MinusKeyPressed()
        {
            int currentNumber;
                    
            if (int.TryParse(_inputFieldText.text, out var currentNumberParsed)) 
                currentNumber = currentNumberParsed;
            else
            {
                ClearInputField();
                currentNumber = 0;
            }

            var newNumber = currentNumber * -1;

            _inputFieldText.text = newNumber.ToString();
        }
        
        private void SubmitClicked()
        {
            OnSubmitPressed?.Invoke(int.TryParse(_inputFieldText.text, out var currentNumber)
                ? currentNumber
                : 0);
            
            ClearInputField();
        }

        private void ShuffleKeyboard()
        {
            var random = new Random();
            var n = _inGameKeyboardKeysViews.Count;
            
            while (n > 1)
            {
                n--;
                var k = random.Next(n + 1);
                
                (_inGameKeyboardKeysViews[k].KeyText.text, _inGameKeyboardKeysViews[n].KeyText.text) 
                    = (_inGameKeyboardKeysViews[n].KeyText.text, _inGameKeyboardKeysViews[k].KeyText.text);
            }
        }

        private void ClearInputField() 
            => _inputFieldText.text = "";

        private void SetMenuInteractable(bool interactable)
        {
            _menuCanvasGroup.interactable = interactable;
            _menuCanvasGroup.blocksRaycasts = interactable;
        }

        private void SetMenuVisible(bool visible)
            => _menuCanvasGroup.alpha = visible ? 1 : 0;
    }
}