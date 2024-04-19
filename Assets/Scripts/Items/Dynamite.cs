using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : ActiveItem
{
   [Header("Dynamite")] 
   [SerializeField] private float _explodeRadius = 2f;
   [SerializeField] private float _explodeForce = 350f;

   [SerializeField] private GameObject _areaVisual;
   [SerializeField] private GameObject _effectPrefab;

   protected override void Start()
   {
      base.Start();
      
      _areaVisual.SetActive(false);
   }

   private void OnValidate()
   {
      _areaVisual.transform.localScale = Vector3.one * _explodeRadius * 2f;
   }

   private IEnumerator ExplosionProcess()
   {
      _areaVisual.SetActive(true);
      _animator.enabled = true;

      yield return new WaitForSeconds(1f);
      
      Collider[] colliders = Physics.OverlapSphere(transform.position, _explodeRadius);

      for (int i = 0; i < colliders.Length; i++)
      {
         PassiveItem passiveItem = colliders[i].GetComponentInParent<PassiveItem>();
         Rigidbody rigidbody = colliders[i].attachedRigidbody;

         if (rigidbody)
         {
            Vector3 fromTo = (rigidbody.transform.position - transform.position).normalized;
            rigidbody.AddForce(fromTo * _explodeForce + Vector3.up * _explodeForce / 2);
         }

         if (passiveItem)
         {
            passiveItem.OnAffect();
         }
      }

      Instantiate(_effectPrefab, transform.position, Quaternion.identity);
      Destroy(gameObject);
   }

   public override void DoEffect()
   {
      StartCoroutine(ExplosionProcess());
   }
}
