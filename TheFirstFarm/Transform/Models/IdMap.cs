﻿using System.ComponentModel.DataAnnotations;

namespace TheFirstFarm.Transform.Models {
	public abstract class IdMap<TF, TK> {
		protected IdMap() { }

		protected IdMap(TF fXiaoKeId, TK kingdeeId) {
			FXiaoKeId = fXiaoKeId;
			KingdeeId = kingdeeId;
		}

		[Key]
		public TF FXiaoKeId { get; set; }

		public TK KingdeeId { get; set; }
	}
}