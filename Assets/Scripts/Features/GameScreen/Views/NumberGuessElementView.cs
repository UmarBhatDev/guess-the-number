using TMPro;
using UnityEngine;

namespace GTN.Features.GameScreen
{
    public class NumberGuessElementView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _resultNumber;

        public void SetResultNumber(int number)
        {
            _resultNumber.text = number.ToString();
        }
    }
}