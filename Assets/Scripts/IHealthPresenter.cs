using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// POLYMORPHISM: IHealthPresenter is an interface implemented in derived classes
public interface IHealthPresenter
{
    void UpdateHealth(uint health, uint maxHealth);
}
