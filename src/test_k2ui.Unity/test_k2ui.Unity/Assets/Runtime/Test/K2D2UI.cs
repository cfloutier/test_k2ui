// This script attaches the tabbed menu logic to the game.
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

using K2UI;
using K2UI.Tabs;
using KTools;

namespace K2D2.UI.Tests
{
    public class Test_K2D2UI : MonoBehaviour
    {
        private K2TabsView pages_controler;

        public string start_selected = "controls";

        // public List<Panel> panels;
        private void OnEnable()
        {

        }

        private void Start()
        {
            UIDocument menu = GetComponent<UIDocument>();
            VisualElement root = menu.rootVisualElement;
           

            SettingsFile.Init(this, "settings.json");
            List<K2Page> panels = new()
            {
               
            };

            var settings_toggle = root.Q<ToggleButton>("settings-toggle");
            settings_toggle.Bind(GlobalSetting.settings_visible);

            pages_controler = root.Q<K2TabsView>();
            pages_controler.Init(panels);
            pages_controler.Select(start_selected);
        }

        public void Update()
        {
            if (pages_controler != null)
                pages_controler.Update();
        }
    }
}