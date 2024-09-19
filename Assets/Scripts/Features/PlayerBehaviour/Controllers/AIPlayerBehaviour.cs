using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using GTN.Features.GameScreen;
using UnityEngine.Scripting;
using Random = UnityEngine.Random;

namespace GTN.Features.PlayerBehaviour
{
    [Preserve]
    public class AIPlayerBehaviour : IAIPlayerBehaviour
    {
        private readonly AvatarIconView _avatarIconView;
        private readonly PlayersInputModel _playersInputModel;

        public AIPlayerBehaviour(AvatarIconView avatarIconView, PlayersInputModel playersInputModel)
        {
            _avatarIconView = avatarIconView;
            _playersInputModel = playersInputModel;
        }

        public async UniTask<int> AskMovementResult((int from, int to) guessRange, CancellationToken cancellationToken)
        {
            await _avatarIconView.Raise(cancellationToken);

            await UniTask.WaitForSeconds(Random.Range(1f, 3f), cancellationToken: cancellationToken);

            var lowestPrediction = int.MinValue;
            var highestPrediction = int.MinValue;

            if (_playersInputModel.InputVariables.Any(x => x.ResultComparedToGuessed is ResultComparedToGuessed.Greater))
            {
                var highestPredictionFromHistory = _playersInputModel.InputVariables
                    .Where(x => x.ResultComparedToGuessed is ResultComparedToGuessed.Greater)
                    .Min(x => x.InputVariable);
                
                highestPrediction = Math.Max(highestPredictionFromHistory, guessRange.from);
            }
            
            if (_playersInputModel.InputVariables.Any(x => x.ResultComparedToGuessed is ResultComparedToGuessed.Less))
            {
                var lowestPredictionFromHistory = _playersInputModel.InputVariables
                    .Where(x => x.ResultComparedToGuessed is ResultComparedToGuessed.Less)
                    .Max(x => x.InputVariable);
                
                lowestPrediction = Math.Min(lowestPredictionFromHistory, guessRange.to);
            }
            
            int currentPredictionResult;

            do
            {
                if (lowestPrediction == int.MinValue)
                    lowestPrediction = guessRange.from - 1;
                if (highestPrediction == int.MinValue)
                    highestPrediction = guessRange.to + 1;
                
                currentPredictionResult = Random.Range(lowestPrediction + 1, highestPrediction - 1);
                
            } while (_playersInputModel.InputVariables.Any(x => x.InputVariable == currentPredictionResult));
            
            await _avatarIconView.Hide(cancellationToken);

            return currentPredictionResult;
        }

        public async UniTask RaiseHighlight(CancellationToken cancellationToken)
        {
            await _avatarIconView.Raise(cancellationToken);
        }

        public async UniTask HideHighlight(CancellationToken cancellationToken)
        {
            await _avatarIconView.Hide(cancellationToken);
        }

        public async UniTask Celebrate(CancellationToken cancellationToken)
        {
            await _avatarIconView.Raise(cancellationToken);
        }
    }
}