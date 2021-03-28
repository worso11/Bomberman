using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class substituting Mirror's NetworkManagerHUD (using mainly its code)
// Have to be in main asset folder!
public class HUD : MonoBehaviour
{
        Network manager;

        /// <summary>
        /// Whether to show the default control HUD at runtime.
        /// </summary>
        public bool showGUI = true;

        /// <summary>
        /// The horizontal offset in pixels to draw the HUD runtime GUI at.
        /// </summary>
        public int offsetX;

        /// <summary>
        /// The vertical offset in pixels to draw the HUD runtime GUI at.
        /// </summary>
        public int offsetY;

        void Awake()
        {
            manager = GetComponent<Network>();
        }

        void OnGUI()
        {
            if (!showGUI)
                return;

            GUILayout.BeginArea(new Rect(10 + offsetX, 40 + offsetY, 608, 9999));
            if (!NetworkClient.isConnected && !NetworkServer.active)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();
            }

            // client ready
            if (NetworkClient.isConnected && !ClientScene.ready)
            {
                if (GUILayout.Button("Client Ready"))
                {
                    ClientScene.Ready(NetworkClient.connection);

                    if (ClientScene.localPlayer == null)
                    {
                        ClientScene.AddPlayer(NetworkClient.connection);
                    }
                }
            }

            StopButtons();

            GUILayout.EndArea();
        }

        void StartButtons()
        {
            if (!NetworkClient.active)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                // Server + Client
                if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    if (GUILayout.Button("Host (Server + Client)", GUILayout.Width(200), GUILayout.Height(30)))
                    {
                        manager.StartHost();
                    }
                }

                // Server Only
                if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    // cant be a server in webgl build
                    GUILayout.Box("(  WebGL cannot be server  )");
                }
                else
                {
                    if (GUILayout.Button("Server Only", GUILayout.Width(200), GUILayout.Height(30))) manager.StartServer();
                }
                GUILayout.EndVertical();
                
                // Client + IP
                GUILayout.BeginVertical();
                if (GUILayout.Button("Client", GUILayout.Width(200), GUILayout.Height(30)))
                {
                    manager.StartClient();
                }
                manager.networkAddress = GUILayout.TextField(manager.networkAddress, GUILayout.Width(200), GUILayout.Height(30));
                GUILayout.EndVertical();
                
                if (GUILayout.Button("Exit", GUILayout.Width(200), GUILayout.Height(60))) SceneManager.LoadScene("Menu");
                GUILayout.EndHorizontal();
            }
            else
            {
                // Connecting
                GUILayout.Label("Connecting to " + manager.networkAddress + "..");
                if (GUILayout.Button("Cancel Connection Attempt"))
                {
                    manager.StopClient();
                    SceneManager.LoadScene("MultiGame");
                }
            }
        }

        void StatusLabels()
        {
            GUILayout.BeginHorizontal();
            // server / client status message
            if (NetworkServer.active)
            {
                GUILayout.Label("Server: active. Transport: " + Transport.activeTransport);
            }
            if (NetworkClient.isConnected)
            {
                GUILayout.Label("Client: address=" + manager.networkAddress);
            }
            GUILayout.EndHorizontal();
        }

        void StopButtons()
        {
            // stop host if host mode
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Host"))
                {
                    manager.StopHost();
                }
            }
            // stop client if client-only
            else if (NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop Client"))
                {
                    manager.StopClient();
                }
            }
            // stop server if server-only
            else if (NetworkServer.active)
            {
                if (GUILayout.Button("Stop Server"))
                {
                    manager.StopServer();
                }
            }
        }
}
