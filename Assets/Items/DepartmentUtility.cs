using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class DepartmentUtility : MonoBehaviour
    {
        private static readonly Dictionary<Department, GameObject> departmentLogoPrefabs = new Dictionary<Department, GameObject>();
        private static readonly Dictionary<Department, AudioClip> departmentSoundPrefabs = new Dictionary<Department, AudioClip>();

        public static Dictionary<Department, GameObject> DepartmentLogoPrefabs => departmentLogoPrefabs;
        public static Dictionary<Department, AudioClip> DepartmentSoundPrefabs => departmentSoundPrefabs;

        [SerializeField] private GameObject filmPrefab;
        [SerializeField] private GameObject artPrefab;
        [SerializeField] private GameObject audioPrefab;
        [SerializeField] private GameObject proggerPrefab;
        
        [SerializeField] private AudioClip filmSound;
        [SerializeField] private AudioClip artSound;
        [SerializeField] private AudioClip audioSound;
        [SerializeField] private AudioClip proggerSound;
        
        private void Awake()
        {
            FillDictionary(Department.Film, filmPrefab);
            FillDictionary(Department.Art, artPrefab);
            FillDictionary(Department.Audio, audioPrefab);
            FillDictionary(Department.Progger, proggerPrefab);
            
            FillDictionary(Department.Film, filmSound);
            FillDictionary(Department.Art, artSound);
            FillDictionary(Department.Audio, audioSound);
            FillDictionary(Department.Progger, proggerSound);
        }

        private void FillDictionary(Department department, GameObject prefab)
        {
            if (!departmentLogoPrefabs.ContainsKey(department))
            {
                departmentLogoPrefabs.Add(department, prefab);
            }
            else
            {
                departmentLogoPrefabs[department] = prefab;
            }
        }
        
        private void FillDictionary(Department department, AudioClip prefab)
        {
            if (!departmentSoundPrefabs.ContainsKey(department))
            {
                departmentSoundPrefabs.Add(department, prefab);
            }
            else
            {
                departmentSoundPrefabs[department] = prefab;
            }
        }
    }
}