
using com.ARTillery.Combat;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Burst.CompilerServices;
using UnityEngine;


namespace com.ARTillery.Core
{
    public static class ClickTargetFinder
    {
        public static TargetType GetClickTargetType(out RaycastHit target)
        {
            RaycastHit[] hits;
            RaycastHit hit = new();
            if (IsCursorHit(out hits) is false)
            {
                target = hit;
                return TargetType.Nothing;
            }
            if (DetectObjectType<CombatTarget>(hits, out target))
            {
                return TargetType.CombatTarget;
            }
            
            if (DetectObjectType<ResourceNode>(hits, out target))
            {
                return TargetType.ResourceNode;
            }

            target = GetReachableLocation();
            return TargetType.ReachableLocation;

        }

        private static RaycastHit GetReachableLocation()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.CompareTag("Walkable"))
                {
                    return hit;
                }
            }
            return new RaycastHit();
        }

        private static bool DetectObjectType<T>(RaycastHit[] hits, out RaycastHit target)
        {
            RaycastHit objectHit = new();
            bool objectTypePresent = false;
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.GetComponent<T>() is not null)
                {
                    objectHit = hit;
                    objectTypePresent = true;
                }
            }
            target = objectHit;
            return objectTypePresent;
        }


        private static bool IsCursorHit(out RaycastHit[] targetHits)
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            if (hits.Length == 0)
            {
                //Debug.Log("nothing to do");
                targetHits = null;
                return false;
            }
            else
            {
                targetHits = hits;
                return true;
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
