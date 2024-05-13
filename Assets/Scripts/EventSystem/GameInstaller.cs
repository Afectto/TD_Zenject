using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]private Tower tower; 
    
    public override void InstallBindings()
    {
        Container.BindInstance(tower);
        
        Container.Bind<EventManager>().AsSingle().NonLazy();
        
        BindFactory();
    }

    private void BindFactory()
    {
        Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        Container.Bind<TowerWeaponFactory>().AsSingle().NonLazy();
    }
}