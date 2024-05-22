using System;

public interface IBullet
{
    event Action<IBullet, int> OnSetDamage;
    event Action<IBullet> onBulletDestroy;
}