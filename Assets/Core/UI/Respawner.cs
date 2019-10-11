using Network;
using Network.Packet.Serverbound.Play;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    public class Respawner : MonoBehaviour {
        public void Respawn() {
            ClientStatusPacket csp = new ClientStatusPacket(ClientStatusPacket.Status.PerformRespawn);
            var foundObjects = FindObjectsOfType<ServerConnection>();
            foreach (var o in foundObjects) {
                csp.Send(o);
            }
            SceneManager.UnloadSceneAsync("DeathScene");
        }

        public void Disconnect() {

        }
    }
}
