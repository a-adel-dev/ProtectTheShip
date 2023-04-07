
using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.ARTillery.Inventory
{
    public class ResourceManager : MonoBehaviour
    {
        private Dictionary<ResourceType, int> resources = new();
        private static ResourceManager _instance;
        public Action<ResourceType, int> OnResourcesUpdated;

        public static ResourceManager Instance
        {
            get => _instance;
            set => _instance = value;
        }

        private void Awake()
        {
            if (Instance is not null)
            {
                Instance = null;
            }
            Instance = this;
        }

        private void Start()
        {
            // Initialize the resources dictionary with starting values of 0 for each resource type
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                resources[type] = 0;
            }
        }

        public int GetResource(ResourceType type)
        {
            return resources[type];
        }

        public void AddResource(ResourceType type, int amount)
        {
            resources[type] += amount;
            OnResourcesUpdated?.Invoke(type, resources[type]);
        }

        public void RemoveResource(ResourceType type, int amount)
        {
            resources[type] -= amount;
        }

        public bool CanAfford(ResourceType type, int amount)
        {
            return GetResource(type) >= amount;
        }

        public void Spend(ResourceType type, int amount)
        {
            if (CanAfford(type,amount))
            {
                RemoveResource(type, amount);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
