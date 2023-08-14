using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;
        
        [Range(1f, 500f)]
        public float Health = 10f;
        
        [Range(0f, 500f)]
        public float Damage = 10f;

        [Range(0.1f, 10f)]
        public float AttackRadius = 0.5f;
        
        [Range(0.1f, 10f)]
        public float EffectiveDistance = 0.5f;

        [Range(0f, 50f)]
        public float MoveSpeed = 5f;

        [Range(0f, 20f)]
        public float AttackCooldown;

        public int MinLoot;
        
        public int MaxLoot;
        
        public GameObject Prefab;
    }
}