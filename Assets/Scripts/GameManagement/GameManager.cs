using System;
using com.ARTillery.Control;
using com.ARTillery.Inventory;
using Unity.VisualScripting;
using UnityEngine;

namespace com.ARTillery.Management
{
    public class GameManager : MonoBehaviour
    {
        private PlayerBehavior _player;
        private ResourceManager _resourceManager;
        
        private void Start()
        {
            _player = FindObjectOfType<PlayerBehavior>();
            _resourceManager = ResourceManager.Instance;
            _player.OnResourceGathered += UpdateResourceManager;
        }

        private void UpdateResourceManager(ResourceType type, int value)
        {
            _resourceManager.AddResource(type, value);
        }
    }
    
    
}