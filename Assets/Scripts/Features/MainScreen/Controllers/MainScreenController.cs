using System;
using GTN.Base.FSM;
using GTN.Features.SceneTransitions;
using GTN.Utilities;
using UnityEditor;
using UnityEngine.Scripting;
using Zenject;
#if UNITY_EDITOR
using UnityEngine.Device;
#endif

namespace GTN.Features.MainScreen
{
    [Preserve]
    public class MainScreenController : IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private readonly IFactory<MainMenuView> _mainMenuViewFactory;
        private readonly IFactory<GameConfigurationMenuView> _gameConfigurationMenuViewFactory;
        
        private MainMenuView _mainMenuView;
        private GameConfigurationMenuView _gameConfigurationMenuView;

        public MainScreenController(IStateMachine stateMachine,
            IFactory<MainMenuView> mainMenuViewFactory, 
            IFactory<GameConfigurationMenuView> gameConfigurationMenuViewFactory)
        {
            _stateMachine = stateMachine;
            _mainMenuViewFactory = mainMenuViewFactory;
            _gameConfigurationMenuViewFactory = gameConfigurationMenuViewFactory;
        }

        public void Initialize()
        {
            _mainMenuView = _mainMenuViewFactory.Create();
            _gameConfigurationMenuView = _gameConfigurationMenuViewFactory.Create();
            
            _mainMenuView.OnPlayButtonPressed += async () =>
            {
                await _mainMenuView.Hide();
                await _gameConfigurationMenuView.Show();
            };
            
            _gameConfigurationMenuView.OnBackButtonPressed += async () =>
            {
                await _gameConfigurationMenuView.Hide();
                await _mainMenuView.Show();
            };

            _gameConfigurationMenuView.OnPlayButtonPressed += async (guessRange, playersCount) =>
            {
                await _gameConfigurationMenuView.Hide();

                _stateMachine.GoGame(numbersRange: guessRange, playersCount: playersCount, CurtainType.BlackFade);
            };

            _mainMenuView.OnExitButtonPressed += () =>
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            };
        }

        public void Dispose()
        {
            _mainMenuView.Dispose();
            _gameConfigurationMenuView.Dispose();
        }
    }
}