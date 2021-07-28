﻿using System;

namespace Shared.Exceptions {
	public class DuplicateException : ExceptionWithDefaultMessage {
		public DuplicateException(string message = null, Exception innerException = null) : base(message, innerException) { }

		public DuplicateException(object duplicateValue, string message = null, Exception innerException = null) : base(message, innerException) => this[nameof(DuplicateValue)] = duplicateValue;

		public object DuplicateValue => Get<object>(nameof(DuplicateValue));
	}
}