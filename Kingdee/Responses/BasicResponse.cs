﻿// ReSharper disable StringLiteralTypo
using System.Collections.Generic;
using Kingdee.Converters;
using Newtonsoft.Json;
using Shared.JsonConverters;

namespace Kingdee.Responses {
	[JsonConverter(typeof(ObjectWrapperConverter<BasicResponse>), "Result")]
	public class BasicResponse : ResponseBase {
		[JsonProperty("ResponseStatus")]
		public ResponseStatus ResponseStatus { get; set; }
	}

	public class ResponseStatus {
		[JsonProperty("ErrorCode")]
		public string ErrorCode { get; set; }

		[JsonProperty("IsSuccess")]
		[JsonConverter(typeof(BoolConverter))]
		public bool? IsSuccess { get; set; }

		[JsonProperty("Errors")]
		public List<Error> Errors { get; set; }

		[JsonProperty("SuccessEntitys")]
		public List<SuccessEntity> SuccessEntities { get; set; }

		[JsonProperty("SuccessMessages")]
		public List<SuccessMessage> SuccessMessages { get; set; }

		[JsonProperty("MsgCode")]
		public string MessageCode { get; set; }
	}

	public class Error {
		[JsonProperty("FieldName")]
		public string FieldName { get; set; }

		[JsonProperty("Message")]
		public string Message { get; set; }

		[JsonProperty("DIndex")]
		public int DIndex { get; set; }
	}

	public class SuccessEntity {
		[JsonProperty("Id")]
		public string Id { get; set; }

		[JsonProperty("Number")]
		public string Number { get; set; }

		[JsonProperty("DIndex")]
		public int DIndex { get; set; }
	}

	public class SuccessMessage {
		[JsonProperty("FieldName")]
		public string FieldName { get; set; }

		[JsonProperty("Message")]
		public string Message { get; set; }

		[JsonProperty("DIndex")]
		public int DIndex { get; set; }
	}
}