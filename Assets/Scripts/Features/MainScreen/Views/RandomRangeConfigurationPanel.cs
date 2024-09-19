using TMPro;
using UnityEngine;

namespace GTN.Features.MainScreen
{
    public class RandomRangeConfigurationPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputFieldTo;
        [SerializeField] private TMP_InputField _inputFieldFrom;
        
        public (int from, int to) GetInputData()
        {
            var fromText = _inputFieldFrom.text;
            var toText = _inputFieldTo.text;

            return (
                int.TryParse(fromText, out var fromParsed) 
                    ? fromParsed 
                    : 0, 
                int.TryParse(toText, out var toParsed) 
                    ? toParsed 
                    : 100);
        }
    }
}