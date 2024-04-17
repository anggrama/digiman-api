
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class TokenRequest
    {
        //[Required]
        //public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        //[Required]
        //public string scope { get; set; }
        //[Required]
        //public string client_id { get; set; }
        //[Required]
        //public string client_secret { get; set; }
    }

    public class TokenResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public UserSelect userinfo { get; set; }

        [IgnoreDataMember] // refresh token is returned in http only cookie
        public string refresh_token { get; set; }

    }

    public class RefreshTokenResponse
    {
        [Key]
        [IgnoreDataMember]
        public int Id { get; set; }

        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }

    public class ClientSecret
    {
        //client_id
        public string id { get; set; }
        public string client_secret { get; set; }
    }
}
