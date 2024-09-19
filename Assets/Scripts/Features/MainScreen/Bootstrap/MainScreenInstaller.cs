using GTN.Utilities;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.MainScreen
{
    [Preserve]
    public class MainScreenInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFactory<MainMenuViewFactory>();
            Container.InstallFactory<GameConfigurationMenuViewFactory>();
            Container.BindFactory<MainScreenController, MainScreenControllerFactory>();
        }
    }
}