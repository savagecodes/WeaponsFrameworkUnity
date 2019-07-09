using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPAnimationComponent : WPBaseWeaponComponent
    {
        private Dictionary<EWPShootType, WPBaseTriggerComponent> WPBaseTriggerComponents = new Dictionary<EWPShootType, WPBaseTriggerComponent>();

        private bool _isPrimaryShooting;
        private bool _isSecondaryShooting;
        
        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);
            WPBaseTriggerComponents = GetComponents<WPBaseTriggerComponent>().ToDictionary(x => x.TriggerType, y => y);
            
            BaseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_SHOOT, x =>
            {
                if (((EWPShootType) x[0]) == EWPShootType.PRIMARY)
                {
                    _isPrimaryShooting = true;
                }
                else
                {
                    _isSecondaryShooting = true;
                }

                StartCoroutine(ResetShootState());
            });
            
        }

        public virtual bool IsPrimaryShooting()
        {
            return _isPrimaryShooting;
        }

        public virtual bool isSecondaryShooting()
        {
            return _isSecondaryShooting;
        }
        
        IEnumerator ResetShootState()
        {
            yield return new WaitForSeconds(0.05f);
            _isSecondaryShooting = false;
            _isPrimaryShooting = false;
        }
    }
}
