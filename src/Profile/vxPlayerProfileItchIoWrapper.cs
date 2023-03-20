using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using VerticesEngine.Profile;
using VerticesEngine.UI.Controls;

namespace VerticesEngine.Platforms.Itchio.Profile
{
    /// <summary>
    /// ItchIO platform wrapper
    /// </summary>
    public sealed class vxPlayerProfileItchIoWrapper : vxIPlayerProfile
    {
        /// <summary>
        /// Itchio API Key Environment Var key
        /// </summary>
        private const string ITCHIO_API_KEY = "ITCHIO_API_KEY";

        /// <summary>
        /// The API token passed through to the ItchIO instance
        /// </summary>
        private string itchioApiToken = string.Empty;

        /// <summary>
        /// The ItchIO API URL
        /// </summary>
        private const string ITCHIO_API_URL = "https://itch.io/api/1/jwt/me";

        /// <summary>
        /// Is the player signed in to the ItchIO API
        /// </summary>
        public bool IsSignedIn
        {
            get { return _isSignedIn; }
        }
        private bool _isSignedIn = false;


        /// <summary>
        /// This event is fired when a user successfully signs in
        /// </summary>
        public event EventHandler OnSignedIn;

        /// <summary>
        /// This event is fired when the Sign in fails for any reason
        /// </summary>
        public event EventHandler OnSignInFailed;


        public string Name
        {
            get { return _displayName; }
        }

        private string _displayName = "PlayerOne";

        public string Id
        {
            get { return _userId; }
        }

        private string _userId = string.Empty;


        public vxPlatformType PlatformType
        {
            get { return vxPlatformType.ItchIO; }
        }

        public Texture2D Avatar
        {
            get { return vxInternalAssets.Textures.DefaultDiffuse; }
        }

        public string PreferredLanguage
        {
            get { return "en"; }
        }

        public Dictionary<object, vxAchievement> Achievements
        {
            get { return _achievements; }
        }


        Dictionary<object, vxAchievement> _achievements = new Dictionary<object, vxAchievement>();

        public void Initialise()
        {
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                if (de.Key.ToString() == "ITCHIO_API_KEY")
                {
                    itchioApiToken = de.Value.ToString();
                }
            }
        }
#pragma warning disable 067, 649

        public void SignIn()
        {
            vxNotificationManager.Show("Signing In ...", Color.Aqua);

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", itchioApiToken);
                    var response = client.GetStringAsync(ITCHIO_API_URL);

                    vxItchioProfileResponse resp = Newtonsoft.Json.JsonConvert.DeserializeObject<vxItchioProfileResponse>(response.Result);
                    _displayName = resp.user.username;
                    _userId = resp.user.id.ToString();
                    _isSignedIn = true;
                    OnSignedIn?.Invoke(null, new EventArgs());
                    
                    vxNotificationManager.Show("Signed into ItchIO as: " + _displayName, new Color(250, 92, 92));
                }
            }
            catch (Exception e)
            {
                vxConsole.WriteException(e);
            }
        }

        public void SignOut()
        {
            vxNotificationManager.Show("Signing Out ...", Color.DeepPink);
        }

        /// <summary>
        /// Get the ItchIO Auth Ticket, which is the API Token passed as a Environment variable to the App.
        /// </summary>
        /// <returns></returns>
        public string GetAuthTicket()
        {
            return itchioApiToken;
        }

        private void WarnNotImplemented()
        {
            vxConsole.WriteLine("Method is not implemented on this platform.");
        }

        public void AddAchievement(object key, vxAchievement achievement)
        {
            _achievements.Add(key, achievement);
        }

        public vxAchievement GetAchievement(object key)
        {
            return _achievements[key];
        }

        public Dictionary<object, vxAchievement> GetAchievements()
        {
            return _achievements;
        }

        public void IncrementAchievement(object key, int increment)
        {
            WarnNotImplemented();
        }

        public void UnlockAchievement(object key)
        {
            try
            {
                _achievements[key].Achieved = true;
                vxNotificationManager.Add(new vxNotification("Achievement Unlocked! : " + key, Color.DeepPink));
            }
            catch
            {
            }
        }

        public void ViewAchievments()
        {
            WarnNotImplemented();
        }


        // Leaderboards
        // **********************************************************
        public void SubmitLeaderboardScore(string id, long score)
        {
            WarnNotImplemented();
        }

        public void ViewLeaderboard(string id)
        {
            WarnNotImplemented();
        }


        public void ShareImage(string path, string extratxt = "")
        {
            WarnNotImplemented();
        }


        public void InitialisePlayerInfo()
        {
            WarnNotImplemented();
        }

        public void Dispose()
        {
            WarnNotImplemented();
        }

        public void ViewAllLeaderboards()
        {
            WarnNotImplemented();
        }

        public void Update() { }

        public void OpenURL(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception e)
            {
                vxConsole.WriteError(e.Message);
            }
        }

        public void OpenStorePage(string url)
        {  try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception e)
            {
                vxConsole.WriteError(e.Message);
            }
        }


        public string[] GetInstalledMods()
        {
            return new string[] { };
        }


        public void Publish(string title, string description, string imgPath, string folderPath, string[] tags,
            ulong idToUpdate = 0, string changelog = "")
        {
        }

        public void SetStatus(string status)
        {
            WarnNotImplemented();
        }

        public void SetStatusKey(string key, string value)
        {
            WarnNotImplemented();
        }

        public void ClearStatus()
        {
            WarnNotImplemented();
        }

        public void SetStat(string key)
        {
            WarnNotImplemented();
        }

        public void SetStat(string key, int value)
        {
            WarnNotImplemented();
        }

        public void GetPlayerIconFromPlatform(string id, Action<bool, Texture2D> callback)
        {
            callback?.Invoke(false, null);
        }
#pragma warning restore 067, 649
    }
}