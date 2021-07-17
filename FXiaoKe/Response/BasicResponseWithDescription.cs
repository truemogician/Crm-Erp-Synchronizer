﻿using Newtonsoft.Json;

namespace FXiaoKe.Response {
	public class BasicResponseWithDescription : BasicResponse {
		[JsonProperty("errorDescription")]
		public string ErrorDescription { get; set; }
	}
}