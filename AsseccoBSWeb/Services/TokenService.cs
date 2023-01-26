using AsseccoBSWeb.Services.Abstraction;

namespace AsseccoBSWeb.Services
{
    public class TokenService : ITokenService
    {
        private string _accessToken;
        private DateTime _expiredDate = DateTime.MinValue;

        private readonly IOAuthService _oAuthService;

        public TokenService(IOAuthService oAuthService)
        {
            _oAuthService = oAuthService;
        }

        public string GetToken()
        {
            if (DateTime.Now > _expiredDate)
               GetNewToken();
            return _accessToken;
        }

        private void GetNewToken()
        {
            //call to get the key and expired date 

            var token = _oAuthService.GetAccessToken("PortalCloudAPI_F1645EDF0ABE4CBBA9D5135314117619", "sV7fO40gNd3v9x48beVq42zy07Mv6UJB");

            _accessToken = token.AccessToken;
            _expiredDate = DateTime.Now.AddMilliseconds(token.ExpireTime);
        }
    }
}
