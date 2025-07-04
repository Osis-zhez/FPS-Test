using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Code._FPS_Test_Code.Network
{
   public class PlayerJoinManager : SimulationBehaviour, IPlayerJoined, IPlayerLeft
   {
      [SerializeField]
      private NetworkPrefabRef _playerPrefab;

      private readonly Dictionary<PlayerRef, NetworkObject> _players = new Dictionary<PlayerRef, NetworkObject>();

      void IPlayerJoined.PlayerJoined(PlayerRef player)
      {
         if (!Runner.IsServer) return;
            
         NetworkObject playerObject = Runner.Spawn(_playerPrefab, Vector3.zero, Quaternion.identity, player);
         _players.Add(player, playerObject);
      }

      void IPlayerLeft.PlayerLeft(PlayerRef player)
      {
         if (!Runner.IsServer) return;
            
         if (_players.Remove(player, out NetworkObject playerObject))
            Runner.Despawn(playerObject);
      }
   }
}