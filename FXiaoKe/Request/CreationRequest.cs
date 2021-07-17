﻿using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using FXiaoKe.Models;
using FXiaoKe.Response;
using FXiaoKe.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Exceptions;

namespace FXiaoKe.Request {
	public class CreationRequest<T> : CreationRequestBase<CreationData<T>> where T : ModelBase { }

	public class CreationRequest<T, TDetail> : CreationRequestBase<CreationData<T, TDetail>> where T : ModelBase where TDetail : ModelBase { }

	[Request("/cgi/crm/v2/data/create", typeof(CreationResponse))]
	public abstract class CreationRequestBase<T> : CrmRequest<T> {
		/// <summary>
		///     是否触发工作流（不传时默认为true, 表示触发），该参数对所有对象均有效
		/// </summary>
		[JsonProperty("triggerWorkFlow")]
		public bool TriggerWorkFlow { get; set; }

		/// <summary>
		///     是否指定创建时间（不传时默认为false,表示忽略参数中的create_time创建时间字段）
		/// </summary>
		[JsonProperty("hasSpecifyTime")]
		public bool SpecifyCreationTime { get; set; }

		/// <summary>
		///     是否指定创建人（不传时默认为false,表示忽略参数中的created_by创建时间字段）
		/// </summary>
		[JsonProperty("hasSpecifyCreatedBy")]
		public bool SpecifyCreator { get; set; }

		/// <summary>
		///     主从对象一起创建时，是否返回从对象id列表，true返回，false不返回，默认不返回
		/// </summary>
		[JsonProperty("includeDetailIds")]
		public bool ReturnSubObjectIds { get; set; }
	}

	public class CreationData<T> where T : ModelBase {
		/// <summary>
		///     主对象数据map(和对象描述中字段一一对应)
		/// </summary>
		[JsonProperty("object_data")]
		[JsonConverter(typeof(CreationDataModelConverter))]
		[Required]
		public T Model { get; set; }
	}

	public class CreationData<T, TDetail> : CreationData<T> where T : ModelBase where TDetail : ModelBase {
		/// <summary>
		///     明细对象数据map(和对象描述中字段一一对应)
		/// </summary>
		[JsonProperty("details")]
		public TDetail Detail { get; set; }
	}

	public class CreationDataModelConverter : JsonConverter<ModelBase> {
		public Type ModelType { get; set; }

		public override void WriteJson(JsonWriter writer, ModelBase value, JsonSerializer serializer) {
			ModelType = value.GetType();
			var builder = new StringBuilder();
			using var stringWriter = new StringWriter(builder);
			serializer.Serialize(stringWriter, value, value.GetType());
			string json = builder.ToString();
			int index = json.IndexOf('{');
			writer.WriteRaw(json[..index]);
			writer.WritePropertyName("dataObjectApiName");
			writer.WriteValue(value.GetType().GetModelName());
			writer.WriteRaw(json[(index + 1)..]);
		}

		public override ModelBase ReadJson(JsonReader reader, Type objectType, ModelBase existingValue, bool hasExistingValue, JsonSerializer serializer) {
			if (ModelType is null)
				throw new NotImplementedException();
			var token = JToken.Load(reader);
			if (token.Type is JTokenType.Null or JTokenType.Undefined)
				return default;
			if (token.Type != JTokenType.Object)
				throw new JTokenTypeException(token, JTokenType.Object);
			if ((token as JObject)!.Property("dataObjectApiName") is { } prop) {
				if (prop.Value.Value<string>() != ModelType.GetModelName())
					throw new JTokenException(prop, $"Model name mismatched: {ModelType.GetModelName()} expected");
				prop.Remove();
			}
			var stringReader = new StringReader(token.ToString());
			return (dynamic)serializer.Deserialize(stringReader, ModelType);
		}
	}
}