using GTN.Features.GameScreen;
using GTN.Features.InGameKeyboard;
using GTN.Features.MainScreen;
using UnityEngine;

namespace GTN.Base.Data
{
    [CreateAssetMenu(fileName = "ViewRegistry", menuName = "Registries/ViewRegistry")]
    public class ViewRegistry : ScriptableObject
    {
        [field: SerializeField] public MainMenuView MainMenuPanel { get; private set; }
        [field: SerializeField] public GameScreenView GameScreenView { get; private set; }
        [field: SerializeField] public InGameKeyboardView InGameKeyboardView { get; private set; }
        [field: SerializeField] public Canvas GameplayCanvas { get; private set; }
        [field: SerializeField] public AvatarIconView AvatarIconView { get; private set; }
        [field: SerializeField] public NumberGuessElementView NumberGuessElementView { get; private set; }
        [field: SerializeField] public GameConfigurationMenuView GameConfigurationMenuPanel { get; private set; }
    }
}

