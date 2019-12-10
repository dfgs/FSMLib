using FSMLib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Common.Attributes
{
	[Serializable,XmlInclude(typeof(IgnoreNodeAttribute)), XmlInclude(typeof(DeserializerAttribute)), XmlInclude(typeof(DeserializeAsAttribute))]
	public abstract class RuleAttribute:IRuleAttribute
	{
	}
}
