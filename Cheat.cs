using STRAFTSHAT.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace STRAFTSHAT
{
    public class Cheat : MonoBehaviour
    {
        private Cache _cache = new Cache(1);
        private Vector2 _watermarkPos = new Vector2(10, 10);

        private bool _menuOpen = true;
        private Rect _windowRect = new Rect(100, 100, 200, 200);
        public Cache Cache { get => _cache; }
        public static Cheat Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
                Instance = this;

            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _menuOpen = !_menuOpen;

            Cache.Update();

            WeaponMods.Update();
        }

        private void Menu(int id)
        {
            Config.Instance.Draw();
            GUI.DragWindow();
        }

        private void OnGUI()
        {
            Utils.DrawText(_watermarkPos, "STRAFTSHAT", Color.cyan, 14);

            if (_menuOpen) // todo: block input to game & lock cursor
                _windowRect = GUI.Window(0, _windowRect, Menu, "STRAFTSHAT - Insert");

            ESP.OnGUI();
        }
    }
}
