﻿using FishNet.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace STRAFTSHAT
{
    public class Cache
    {
        private readonly FieldInfo PlayerPickup_weaponInHand = typeof(PlayerPickup).GetField("weaponInHand", BindingFlags.NonPublic | BindingFlags.Instance);
        private readonly FieldInfo PlayerPickup_weaponInLeftHand = typeof(PlayerPickup).GetField("weaponInLeftHand", BindingFlags.NonPublic | BindingFlags.Instance);

        private float _lastTimeUpdated = 0;
        private float _updateInterval;
        private List<PlayerCache> _players = new List<PlayerCache>();

        public PlayerCache LocalPlayer { get; private set; }
        public Weapon LocalWeaponLeft { get; private set; }
        public Weapon LocalWeaponRight { get; private set; }
        public FirstPersonController LocalController { get; private set; }
        public Camera MainCamera { get; private set; }
        public List<PlayerCache> Players => _players;

        public Cache(float interval) => _updateInterval = interval;

        private void UpdateCache()
        {
            LocalController = Settings.Instance.localPlayer;

            if (!LocalController)
                return;

            MainCamera = LocalController.playerCamera;
            LocalPlayer = new PlayerCache(LocalController.gameObject);

            if (LocalPlayer.isValid)
            {
                PlayerPickup playerPickup = LocalController.GetComponent<PlayerPickup>();
                if (playerPickup)
                {
                    LocalWeaponLeft = (Weapon)PlayerPickup_weaponInLeftHand.GetValue(playerPickup);
                    LocalWeaponRight = (Weapon)PlayerPickup_weaponInHand.GetValue(playerPickup);
                }
            }

            _players.Clear();

            // ideally hook when players are initialized and destroyed to keep track of them
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (gameObject == LocalPlayer.gameObject)
                    continue;

                PlayerCache player = new PlayerCache(gameObject);

                if (player.isValid)
                    _players.Add(player);
            }
        }

        public void Update()
        {
            if (Time.time - _lastTimeUpdated > _updateInterval)
            {
                UpdateCache();
                _lastTimeUpdated = Time.time;
            }
        }
    }
}
