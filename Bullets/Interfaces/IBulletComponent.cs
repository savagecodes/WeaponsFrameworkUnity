using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SavageCodes.Frameworks.Weapons
{
  public interface IBulletComponent
  {
    void InitializeComponent(Bullet bullet);
    void CustomUpdate();
    void DestroyComponent();
  }
}
