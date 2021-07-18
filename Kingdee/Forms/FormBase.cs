﻿using Kingdee.Requests;
using Shared.Exceptions;

namespace Kingdee.Forms {
	[Form]
	public abstract class FormBase {
		public object this[Field field] {
			get => field.GetValue(this);
			set => field.SetValue(this, value);
		}
	}
}