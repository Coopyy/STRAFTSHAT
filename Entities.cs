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

        public bool isValid = false;

        public string playerName = string.Empty;

        public GameObject gameObject = null;
        public PlayerHealth playerHealth = null;

        public Collider collider = null;
        public Transform headTransform = null;

        public PlayerCache(GameObject gameObject)
        {
            this.gameObject = gameObject;

            playerHealth = gameObject.GetComponent<PlayerHealth>();
            collider = gameObject.GetComponent<Collider>();

            headTransform = Utils.RecursiveFind(gameObject.transform, "Head_Col"); // check unity explorer for rest of bones i cba

            playerName = playerHealth.playerValues.playerClient.PlayerName;

            isValid = true;
        }

        public void Draw(Camera camera)
        {
            if (gameObject == null || gameObject.transform == null || camera == null || collider == null || playerHealth == null)
                return;

            Vector3 screenPos = camera.WorldToScreenPoint(gameObject.transform.position - OFFSET);
            if (screenPos.z < 0)
                return;
            screenPos.y = Screen.height - screenPos.y;

            Utils.SetupExtentsBounds(collider.bounds);
            Utils.Draw3DBox(camera, Color.red);

            int idx = 0;

            // name
            Utils.DrawText(new Vector2(screenPos.x, screenPos.y + FONT_SIZE * idx++), playerName, Color.white, FONT_SIZE, true);

            // hp
            int hp = Mathf.Clamp((int)(playerHealth.health * 25), 0, 100);
            Utils.DrawText(new Vector2(screenPos.x, screenPos.y + FONT_SIZE * idx++), hp + " HP", Utils.DoubleColorLerp(hp / 100.0f, Color.green, Color.yellow, Color.red), FONT_SIZE, true);
        }
    }
}
