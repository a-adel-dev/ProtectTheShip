
using System;
using com.ARTillery.Inventory;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace com.ARTillery.UI
{
    public class PopulateUI : MonoBehaviour
    {
        private VisualElement _parentElement; // The parent element to attach the text elements to

        private VisualElement root;
        private StyleSheet resourceStyleSheet;


        void Start()
        {
            
            root = GetComponent<UIDocument>().rootVisualElement;
            root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/UI/PTSHUD.uxml"));



            _parentElement = root.Q<VisualElement>("Resources");
            
            Debug.Log(_parentElement);
            foreach (int i in Enum.GetValues(typeof(ResourceType)))
            {
                VisualElement element = new();
                element.AddToClassList("Resource");
                _parentElement.Add(element);
            
                string resourceName = Enum.GetName(typeof(ResourceType), i);
            
                var resourceNameTextElement = new Label($"{resourceName}:");
                resourceNameTextElement.AddToClassList("ResourceLabel");
                element.Add(resourceNameTextElement);
            
                var resourceValueTextElement = new Label("00");
                resourceValueTextElement.AddToClassList("ResourceValue");
                element.Add(resourceValueTextElement);
                
                
            }
        }

    }
}


