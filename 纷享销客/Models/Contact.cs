﻿using System.ComponentModel.DataAnnotations;
using FXiaoKe.Utilities;
using Newtonsoft.Json;

namespace FXiaoKe.Models {
	/// <summary>
	/// 联系人
	/// </summary>
	[Model("ContactObj")]
	public class Contact {
		/// <summary>
		/// 联系人编码
		/// </summary>
		[JsonProperty("field_b13yj__c")]
		[Required]
		public string Id { get; set; }

		/// <summary>
		/// 姓名
		/// </summary>
		[JsonProperty("name")]
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// 手机1
		/// </summary>
		[JsonProperty("mobile1")]
		[Required]

		public string PhoneNumber { get; set; }

		/// <summary>
		/// 地址
		/// </summary>
		[JsonProperty("add")]
		[Required]

		public string Address { get; set; }
	}
}