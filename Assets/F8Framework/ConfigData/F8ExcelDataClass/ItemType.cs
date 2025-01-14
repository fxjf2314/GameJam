/*Auto create
Don't Edit it*/

using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace F8Framework.F8ExcelDataClass
{
	[Serializable]
	public class ItemTypeItem
	{
		[Preserve]
		public int id;
		[Preserve]
		public string name;
		[Preserve]
		public string type;
		[Preserve]
		public string suit;
	}
	
	[Serializable]
	public class ItemType
	{
		public Dictionary<int, ItemTypeItem> Dict = new Dictionary<int, ItemTypeItem>();
	}
}
