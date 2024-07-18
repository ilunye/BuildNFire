using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Mirror
{
    /// <summary>Shows NetworkManager controls in a GUI at runtime.</summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/Network Manager HUD")]
    [RequireComponent(typeof(NetworkManager))]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-manager-hud")]
    public class NetworkManagerHUD : MonoBehaviour
    {
        NetworkManager manager;

        public int offsetX;
        public int offsetY;
        public int clientNo = 0;
        public static bool disable = false;
        private GUIStyle buttonStyle;
        private GUIStyle textFieldStyle;
        private GUIStyle labelStyle;

        void Awake()
        {
            disable = false;
            manager = GetComponent<NetworkManager>();
        }

        void OnGUI()
        {
            if(disable) return;
            // If this width is changed, also change offsetX in GUIConsole::OnGUI
            int width = Screen.width / 3;
            buttonStyle = new GUIStyle(GUI.skin.button);
            textFieldStyle = new GUIStyle(GUI.skin.textField);
            buttonStyle.fontSize = Screen.height / 40;
            textFieldStyle.fontSize = Screen.height / 40;
            labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = Screen.height / 40;

            Rect rect = new Rect(10 + offsetX, 40 + offsetY, width, 9999);
            GUILayout.BeginArea(rect);

            if (!NetworkClient.isConnected && !NetworkServer.active)
                StartButtons();
            else
                StatusLabels();

            if (NetworkClient.isConnected && !NetworkClient.ready)
            {
                if (GUILayout.Button("Client Ready", buttonStyle, GUILayout.Height(Screen.height / 20)))
                {
                    // client ready
                    NetworkClient.Ready();
                    if (NetworkClient.localPlayer == null)
                        NetworkClient.AddPlayer();
                }
            }

            StopButtons();

            GUILayout.EndArea();
        }

        void StartButtons()
        {
            if (!NetworkClient.active)
            {
// #if UNITY_WEBGL
//                 // cant be a server in webgl build
//                 if (GUILayout.Button("Single Player"))
//                 {
//                     NetworkServer.dontListen = true;
//                     manager.StartHost();
//                 }
// #else
                // Server + Client
                if (GUILayout.Button("Host (Server + Client)", buttonStyle, GUILayout.Height(Screen.height / 20)))
                    manager.StartHost();
// #endif

                // Client + IP (+ PORT)
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Client", buttonStyle, GUILayout.Height(Screen.height / 20)))
                    manager.StartClient();

                GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
                manager.networkAddress = GUILayout.TextField(manager.networkAddress, textFieldStyle, GUILayout.Height(Screen.height / 20));
                // only show a port field if we have a port transport
                // we can't have "IP:PORT" in the address field since this only
                // works for IPV4:PORT.
                // for IPV6:PORT it would be misleading since IPV6 contains ":":
                // 2001:0db8:0000:0000:0000:ff00:0042:8329
                // if (Transport.active is PortTransport portTransport)
                // {
                //     // use TryParse in case someone tries to enter non-numeric characters
                //     if (ushort.TryParse(GUILayout.TextField(portTransport.Port.ToString()), out ushort port))
                //         portTransport.Port = port;
                // }

                GUILayout.EndHorizontal();

                // Server Only
// #if UNITY_WEBGL
//                 // cant be a server in webgl build
//                 GUILayout.Box("( WebGL cannot be server )");
// #else
                if (GUILayout.Button("Server Only", buttonStyle, GUILayout.Height(Screen.height / 20)))
                    manager.StartServer();
// #endif
            }
            else
            {
                // Connecting
                GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                GUILayout.Label($"Connecting to {manager.networkAddress}..", labelStyle);
                if (GUILayout.Button("Cancel Connection Attempt", buttonStyle, GUILayout.Height(Screen.height / 20)))
                    manager.StopClient();
            }
        }

        void StatusLabels()
        {
            // host mode
            // display separately because this always confused people:
            //   Server: ...
            //   Client: ...
            if (NetworkServer.active && NetworkClient.active)
            {
                // host mode
                GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                GUILayout.Label($"<b>Host</b>: running on {clientNo}", labelStyle);
            }
            else if (NetworkServer.active)
            {
                // server only
                GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                GUILayout.Label($"<b>Server</b>: running on {clientNo}", labelStyle);
            }
            else if (NetworkClient.isConnected)
            {
                // client only
                GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                GUILayout.Label($"<b>Client</b>: connected to {manager.networkAddress} via {Transport.active}", labelStyle);
            }
        }

        void StopButtons()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                GUILayout.BeginHorizontal();
// #if UNITY_WEBGL
//                 if (GUILayout.Button("Stop Single Player")){
//                     manager.StopHost();
//                     Application.LoadLevel(Application.loadedLevel);
//                 }
// #else
                // stop host if host mode
                if (GUILayout.Button("Stop Host", buttonStyle, GUILayout.Height(Screen.height / 20))){
                    manager.StopHost();
                    // Application.LoadLevel(Application.loadedLevel);
                }

                // stop client if host mode, leaving server up
                // if (GUILayout.Button("Stop Client"))
                //     manager.StopClient();
// #endif
                GUILayout.EndHorizontal();
            }
            else if (NetworkClient.isConnected)
            {
                // stop client if client-only
                if (GUILayout.Button("Stop Client", buttonStyle, GUILayout.Height(Screen.height / 20))){
                    manager.StopClient();
                    // Application.LoadLevel(Application.loadedLevel);
                }
            }
            else if (NetworkServer.active)
            {
                // stop server if server-only
                if (GUILayout.Button("Stop Server", buttonStyle, GUILayout.Height(Screen.height / 20))){
                    manager.StopServer();
                    // Application.LoadLevel(Application.loadedLevel);
                }
            }
        }
    }
}
