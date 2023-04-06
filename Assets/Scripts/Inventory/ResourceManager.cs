
using System;
using System.Collections.Generic;

namespace com.ARTillery.Inventory
{
    public static class ResourceManager
    {
        private static Dictionary<ResourceType, int> resources = new();


        static ResourceManager()
        {
            // Initialize the resources dictionary with starting values of 0 for each resource type
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                resources[type] = 0;
            }
        }

        public static int GetResource(ResourceType type)
        {
            return resources[type];
        }

        public static void AddResource(ResourceType type, int amount)
        {
            resources[type] += amount;
        }

        public static void RemoveResource(ResourceType type, int amount)
        {
            resources[type] -= amount;
        }

        public static bool CanAfford(ResourceType type, int amount)
        {
            return GetResource(type) >= amount;
        }

        public static void Spend(ResourceType type, int amount)
        {
            if (CanAfford(type,amount))
            {
                RemoveResource(type, amount);
            }
        }
    }
}
