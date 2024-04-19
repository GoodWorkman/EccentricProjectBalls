using UnityEngine;

public class Barrell : PassiveItem
{
   [Header("Barrell")]
   [SerializeField] private GameObject _dieEffect;

   public override void OnAffect()
   {
      base.OnAffect();
      Die();
   }

   private void Die()
   {
      Instantiate(_dieEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
      Destroy(gameObject);
   }
}
