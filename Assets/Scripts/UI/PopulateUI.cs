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

        private VisualElement _root;
        private StyleSheet _resourceStyleSheet;


        void Start()
        {
            SetUpResourceHUD();
            ResourceManager.Instance.OnResourcesUpdated += UpdateResourceValue;
        }
        

        private void UpdateResourceValue(ResourceType type, int value)
        {

            Label targetLabel = _parentElement.Q<Label>($"{type}Value");
            targetLabel.text = value.ToString("00000");
        }

        private void SetUpResourceHUD()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/UI/PTSHUD.uxml"));


            _parentElement = _root.Q<VisualElement>("Resources");
            
            foreach (int i in Enum.GetValues(typeof(ResourceType)))
            {
                VisualElement element = new();
                element.AddToClassList("Resource");
                _parentElement.Add(element);

                string resourceName = Enum.GetName(typeof(ResourceType), i);

                var resourceNameTextElement = new Label($"{resourceName}:");
                resourceNameTextElement.name = resourceName;
                resourceNameTextElement.AddToClassList("ResourceLabel");
                element.Add(resourceNameTextElement);

                var resourceValueTextElement = new Label("00000");
                resourceValueTextElement.name = $"{resourceName}Value";
                resourceValueTextElement.AddToClassList("ResourceValue");
                element.Add(resourceValueTextElement);
            }
        }
    }
}


