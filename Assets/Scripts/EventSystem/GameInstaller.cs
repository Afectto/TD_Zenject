using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]private Tower tower; 
    [SerializeField]private Shop shop; 
    [SerializeField]private MoneyManager moneyManager; 
    
    public override void InstallBindings()
    {
        Container.BindInstance(tower);
        Container.BindInstance(moneyManager);
        Container.BindInstance(shop);
        
        Container.Bind<EventManager>().AsSingle().NonLazy();
        
        BindFactory();
    }

    private void BindFactory()
    {
        Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        Container.Bind<TowerWeaponFactory>().AsSingle().NonLazy();
    }
}