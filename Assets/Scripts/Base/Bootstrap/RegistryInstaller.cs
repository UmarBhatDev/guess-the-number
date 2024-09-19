using GTN.Base.Data;
using GTN.Features.SceneTransitions;
using GTN.Utilities;
using UnityEngine;
using Zenject;

namespace GTN.Base.Bootstrap
{
    [CreateAssetMenu(fileName = "RegistryInstaller", menuName = "Installers/RegistryInstaller")]
    public class RegistryInstaller : ScriptableObjectInstaller<RegistryInstaller>
    {
        [SerializeField] private ViewRegistry _viewRegistry;
        [SerializeField] private CurtainRegistry _curtainRegistry;
        [SerializeField] private PlayerAvatarIconRegistry _playerAvatarIconRegistry;

        public override void InstallBindings()
        {
            Container.InstallRegistry(_viewRegistry);
            Container.InstallRegistry(_curtainRegistry);
            Container.InstallRegistry(_playerAvatarIconRegistry);
        }
    }
}