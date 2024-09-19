using System.Collections.Generic;
using GTN.Base.Data;
using UnityEngine;
using Zenject;

namespace GTN.Features.GameScreen
{
    public class HistoryBarView : MonoBehaviour
    {
        private const int MAX_ELEMENTS_COUNT = 5;
        private Queue<NumberGuessElementView> _numberGuessElementViews;
        
        [Inject] private ViewRegistry _viewRegistry;

        private void Awake()
        {
            _numberGuessElementViews = new Queue<NumberGuessElementView>();
        }

        public void AddNumberGuessElement(int inputNumber)
        {
            //todo create a acceptable factory and object pool. Also create a coin fly animation
            NumberGuessElementView currentElementView;

            if (_numberGuessElementViews.Count > MAX_ELEMENTS_COUNT)
            {
                currentElementView = _numberGuessElementViews.Dequeue();

                currentElementView.transform.SetAsLastSibling();
            }
            else currentElementView = Instantiate(_viewRegistry.NumberGuessElementView, transform);

            _numberGuessElementViews.Enqueue(currentElementView);
            
            currentElementView.SetResultNumber(inputNumber);
        }
    }
}