using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EventManager>().AsSingle().NonLazy();
        Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        
    }
    
}