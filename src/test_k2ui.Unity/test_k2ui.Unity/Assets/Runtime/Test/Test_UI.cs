// This script attaches the tabbed menu logic to the game.
using UnityEngine;
using UnityEngine.UIElements;

using System.Collections.Generic;

using K2UI;
using K2UI.Tabs;
using KTools;

namespace K2D2.UI.Tests
{
    public class TimerPanel: K2Page
    {
        public TimerPanel()
        {
            // this code is used to find the TapPage in the Xml tree
            code = "timer";
        }

        Label my_label;

        public override bool onInit()
        {
            // find the element in the tree
            my_label = panel.Q<Label>("timer_label");     
            return true;
        }

        public override bool onUpdateUI()
        {
            // called on each frame when the UI is visible 
            if (!base.onUpdateUI())
                // the base class return false if the UI is hidden
                return false;

            Debug.Log("calling update");
            my_label.text = $"time is {Time.time:n1} s";

            return true;
        }
    }

    public class DisablePage: K2Page
    {
        public DisablePage()
        {
            code = "disable";
        }

        public override bool onInit()
        {
            var parent_tabs = panel.GetFirstAncestorOfType<K2TabsView>();

            var toogle = panel.Q<K2Toggle>("controls");     
            toogle.RegisterCallback<ChangeEvent<bool>>( evt => parent_tabs.Enable("controls", !evt.newValue));
            toogle = panel.Q<K2Toggle>("timer");     
            toogle.RegisterCallback<ChangeEvent<bool>>( evt => parent_tabs.Enable("timer", !evt.newValue));
            return true;
        }
    }

    public class AboutPage: K2Page
    {
        public AboutPage()
        {
            code = "about";
        }


        void onSettingsChanged(bool visible)
        {
            enabled = visible;
        }


        public override bool onInit()
        {
            // enable only if settings is visible
            GlobalSetting.settings_visible.listen(onSettingsChanged);
            
            Button reset_bt = panel.Q<Button>("reset_settings");  
            reset_bt.listenClick( () => SettingsFile.Instance.Reset());

            return true;
        }
    }

  
    //Inherits from class `MonoBehaviour`. This makes it attachable to a game object as a component.
    public class Test_UI : MonoBehaviour
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
                new TestAllControls(),
                // new TestSettings(),
                new TimerPanel(),
                new DisablePage(),
                new AboutPage(),
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