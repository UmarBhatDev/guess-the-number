using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GTN.Features.InGameKeyboard
{
    public class InGameKeyboardKeyView : MonoBehaviour
    {
        public event Action<string> OnInGameKeyPressed;
        [field: SerializeField] public  TMP_Text KeyText { get; private set; }

        [SerializeField] private Button _keyButton;

        private void Start()
        {
            _keyButton.onClick.AddListener(() => OnInGameKeyPressed?.Invoke(KeyText.text));
        }
    }
}