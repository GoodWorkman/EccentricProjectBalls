using UnityEngine;

[CreateAssetMenu]
public class BallSettings : ScriptableObject
{
   [SerializeField] private Material[] BallMaterials;
   [SerializeField] private Material[] ProjectionMaterials;

   public Material GetNormalMaterial(int level)
   {
      return BallMaterials[level];
   }
   
   public Material GetProjectionMaterial(int level)
   {
      return ProjectionMaterials[level];
   }
}
