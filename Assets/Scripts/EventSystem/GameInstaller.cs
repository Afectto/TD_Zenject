using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]private Tower tower; 
    [SerializeField]private MoneyManager moneyManager; 
    [SerializeField]private Shop shop; 

    public override void InstallBindings()
    {
        BindInstance();
        
        Container.Bind<EventManager>().AsSingle().NonLazy();
        Container.Bind<BuffApplier>().AsSingle().NonLazy();
        Container.Bind<TowerWeaponMultiplayer>().AsSingle().NonLazy();

        BindFactory();
    }

    private void BindInstance()
    {
        Container.BindInstance(tower);
        Container.BindInstance(moneyManager);
        Container.BindInstance(shop);
    }
    
    private void BindFactory()
    {
        Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        Container.Bind<TowerWeaponFactory>().AsSingle().NonLazy();
    }
}