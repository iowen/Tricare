using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TriCare
{
	public class TokenModel
	{

			[JsonProperty("access_token")]
			public string AccessToken { get; set; }

			[JsonProperty("token_type")]
			public string TokenType { get; set; }

			[JsonProperty("expires_in")]
			public int ExpiresIn { get; set; }

			[JsonProperty("userName")]
			public string Username { get; set; }

			[JsonProperty(".issued")]
			public string IssuedAt { get; set; }

			[JsonProperty(".expires")]
			public string ExpiresAt { get; set; }

	}
}

