using FishNet.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace STRAFTSHAT
{
    public class PlayerCache
    {
        private readonly int FONT_SIZE = 14;
        private readonly Vector3 OFFSET = new Vector3(0, 0.5f, 0);


        public GameObject GameObject { get; private set; }
        public Transform HeadTransform { get; private set; }
        public Collider Collider { get; private set; }
        public PlayerHealth PlayerHealth { get; private set; }
        public string PlayerName { get; private set; } = "Unknown";
        public bool IsValid { get; private set; } = false;

        public PlayerCache(GameObject gameObject)
        {
            this.GameObject = gameObject;

            PlayerHealth = gameObject.GetComponent<PlayerHealth>();
            Collider = gameObject.GetComponent<Collider>();

            HeadTransform = Utils.RecursiveFind(gameObject.transform, "Head_Col"); // check unity explorer for rest of bones i cba

            PlayerName = PlayerHealth.playerValues.playerClient.PlayerName;

            IsValid = true;
        }

        public void Draw(Camera camera)
        {
            if (GameObject == null || GameObject.transform == null || camera == null || Collider == null || PlayerHealth == null)
                return;

            Vector3 screenPos = camera.WorldToScreenPoint(GameObject.transform.position - OFFSET);
            if (screenPos.z < 0)
                return;
            screenPos.y = Screen.height - screenPos.y;

            Utils.SetupExtentsBounds(Collider.bounds);
            Utils.Draw3DBox(camera, Color.red);

            int idx = 0;

            // name
            Utils.DrawText(new Vector2(screenPos.x, screenPos.y + FONT_SIZE * idx++), PlayerName, Color.white, FONT_SIZE, true);

            // hp
            int hp = Mathf.Clamp((int)(PlayerHealth.health * 25), 0, 100);
            Utils.DrawText(new Vector2(screenPos.x, screenPos.y + FONT_SIZE * idx++), hp + " HP", Utils.DoubleColorLerp(hp / 100.0f, Color.green, Color.yellow, Color.red), FONT_SIZE, true);
        }
    }
}
