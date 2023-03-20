namespace VerticesEngine.Platforms.Itchio.Profile
{
    /// <summary>
    /// The expected response from Itch with player profile info
    /// </summary>
    /// <code>
    ///{
    ///    "user": {
    ///        "cover_url": "https:\/\/img.itch.zone\/aW1nLzU3ODU2NDAucG5n\/100x100%23\/ZCqZ1b.png",
    ///        "developer": true,
    ///        "id": 1146605,
    ///        "url": "https:\/\/rtroe.itch.io",
    ///        "gamer": true,
    ///        "username": "virtexedgedesign",
    ///        "press_user": false
    ///    },
    ///    "api_key": {
    ///        "issuer": {
    ///            "game_id": 283583
    ///        },
    ///        "expires_at": 1648962638,
    ///        "type": "jwt"
    ///    }
    ///}
    /// </code>
    public class vxItchioProfileResponse
    {
        public vxItchioUserResponse user;
        public vxItchioAPIResponse api_key;
    }
}