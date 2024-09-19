using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GTN.Base.Interfaces;
using GTN.Features.PlayerBehaviour;
using UnityEngine;
using UnityEngine.Scripting;

namespace GTN.Features.GameScreen
{
    [Preserve]
    public class GameModeRule : IGameRule
    {
        private readonly PlayersInputModel _playersInputModel;

        public GameModeRule(PlayersInputModel playersInputModel)
        {
            _playersInputModel = playersInputModel;
        }

        public async UniTask AwaitLevelCompletion(Queue<IPlayerBehaviour> players, (int from, int to) guessRange, 
            GameScreenView gameScreenView, CancellationToken cancellationToken)
        {
            _playersInputModel.Clear();

            var randomGuessedNumber = Random.Range(guessRange.from, guessRange.to);

            var currentPlayer = await PlayCycle(players, guessRange, randomGuessedNumber, gameScreenView, cancellationToken);

            await currentPlayer.Celebrate(cancellationToken);
        }

        private async UniTask<IPlayerBehaviour> PlayCycle(Queue<IPlayerBehaviour> players,
            (int from, int to) guessRange, int randomGuessedNumber, 
            GameScreenView gameScreenView, CancellationToken cancellationToken)
        {
            int playerInput;
            IPlayerBehaviour currentPlayer;

            do
            {
                currentPlayer = players.Dequeue();

                await currentPlayer.RaiseHighlight(cancellationToken);
                
                playerInput = await currentPlayer.AskMovementResult(guessRange, cancellationToken);
                
                ResultComparedToGuessed resultComparedToGuessed ;

                if (playerInput == randomGuessedNumber)
                    resultComparedToGuessed = ResultComparedToGuessed.Equal;
                else if (playerInput > randomGuessedNumber)
                {
                    resultComparedToGuessed = ResultComparedToGuessed.Greater;
                    await currentPlayer.HideHighlight(cancellationToken);
                }
                else
                {
                    resultComparedToGuessed = ResultComparedToGuessed.Less;
                    await currentPlayer.HideHighlight(cancellationToken);
                }
                
                _playersInputModel.AddPlayerInputResult(playerInput, resultComparedToGuessed);

                await gameScreenView.UpdateHistory(playerInput, randomGuessedNumber, cancellationToken);
                await gameScreenView.RotateCoin(playerInput, randomGuessedNumber, cancellationToken);
                
                players.Enqueue(currentPlayer);
                
            } while (playerInput != randomGuessedNumber);
            
            return currentPlayer;
        }
    }
}