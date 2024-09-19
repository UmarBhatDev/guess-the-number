using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GTN.Base.FSM;
using GTN.Features.PlayerBehaviour;
using GTN.Features.SceneTransitions;
using GTN.Utilities;
using UnityEngine.AddressableAssets;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.GameScreen
{
    [Preserve]
    public class GameState : IGameState<GameState.Payload>
    {
        private const string SCENE_NAME = "GameScene";
        
        private readonly IFactory<GameScreenView> _gameScreenViewFactory;
        private readonly CurtainViewFactory _curtainViewFactory;
        private readonly PlayersFactory _playersFactory;
        private readonly IStateMachine _stateMachine;
        private readonly GameModeRule _gameModeRule;

        private CancellationTokenSource _cancellationTokenSource;

        public GameState(CurtainViewFactory curtainViewFactory, IFactory<GameScreenView> gameScreenViewFactory,
            GameModeRule gameModeRule, PlayersFactory playersFactory, IStateMachine stateMachine)
        {
            _gameScreenViewFactory = gameScreenViewFactory;
            _curtainViewFactory = curtainViewFactory;
            _playersFactory = playersFactory;
            _stateMachine = stateMachine;
            _gameModeRule = gameModeRule;
        }

        public async UniTaskVoid Enter(Payload payload)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var curtainType = payload.CurtainOverride ?? CurtainType.DefaultCurtain;

            if (payload.CurtainOverride != null)
                await Transition.ToScene(SCENE_NAME, _curtainViewFactory, curtainType);
            else
                await Addressables.LoadSceneAsync(SCENE_NAME);
            
            var gameScreenView = _gameScreenViewFactory.Create();

            gameScreenView.OnCloseButtonClicked +=  () =>
            {
                gameScreenView.Dispose();
                _stateMachine.GoMainMenu(curtainType: CurtainType.BlackFade);
            };
            
            var players = _playersFactory.CreatePlayerBehaviours(payload.PlayersCount, gameScreenView.AvatarsTransform);
            
            await gameScreenView.Show(_cancellationTokenSource.Token);

            await _gameModeRule.AwaitLevelCompletion(players, payload.NumbersRange, gameScreenView,
                _cancellationTokenSource.Token);

            await gameScreenView.Hide(_cancellationTokenSource.Token);
            
            //show winscreen?
            
            //it would be better to supress cancellation throw at this point (in case if player decides to exit) but for some reason I was not able to
            // i will leave to TODO
            
            gameScreenView.Dispose();

            if (!_cancellationTokenSource.IsCancellationRequested) 
                _stateMachine.GoMainMenu(curtainType: CurtainType.BlackFade);
        }

        public void Exit()
        {
            _cancellationTokenSource?.Cancel();
        }

        public struct Payload
        {
            public CurtainType? CurtainOverride;
            public (int from, int to) NumbersRange;
            public (int real, int ai) PlayersCount;
        }
    }
}