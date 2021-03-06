﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Common.Attributes
{
	public class DeserializerAttribute:RuleAttribute
	{
		[XmlIgnore]
		public Type Value
		{
			get;
			set;
		}

		[XmlAttribute]
		public string Name
		{
			get { return Value?.FullName; }
			set { Value = Type.GetType(value); }
		}

		public DeserializerAttribute()
		{
		}
		public DeserializerAttribute(Type Value)
		{
			this.Value = Value;
		}


	}
}
