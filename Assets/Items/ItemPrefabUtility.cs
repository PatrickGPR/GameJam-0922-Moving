using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    public class ItemPrefabUtility : MonoBehaviour
    {
        private static readonly Dictionary<Department, List<GameObject>> departmentPrefabs = new Dictionary<Department, List<GameObject>>();
        
        [SerializeField] private List<GameObject> filmPrefabs;
        [SerializeField] private List<GameObject> artPrefabs;
        [SerializeField] private List<GameObject> audioPrefabs;
        [SerializeField] private List<GameObject> proggerPrefabs;
        
        private void Awake()
        {
            FillDictionary(Department.Film, filmPrefabs);
            FillDictionary(Department.Art, artPrefabs);
            FillDictionary(Department.Audio, audioPrefabs);
            FillDictionary(Department.Progger, proggerPrefabs);
        }

        public static GameObject GetRandomItemPrefab(Department department)
        {
            var departmentList = departmentPrefabs[department];
            return departmentList[Random.Range(0, departmentList.Count)];
        }
        
        private void FillDictionary(Department department, List<GameObject> prefabs)
        {
            if (!departmentPrefabs.ContainsKey(department))
            {
                departmentPrefabs.Add(department, prefabs);
            }
            else
            {
                foreach (var prefab in prefabs)
                {
                    departmentPrefabs[department].Add(prefab);
                }
            }
        }
    }
}