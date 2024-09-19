using GTN.Utilities;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.GameScreen
{
    [Preserve]
    public class GameScreenInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallGameRule<GameModeRule>();
            Container.InstallModel<PlayersInputModel>();
            Container.InstallFactory<AvatarIconProvider>();
            Container.InstallFactory<GameScreenViewFactory>();
        }
    }
}