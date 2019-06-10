﻿using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Serializers
{
	public interface IRuleCollectionSerializer<T>
	{
		void SaveToStream(Stream Stream, IEnumerable<Rule<T>> Rules);
		IEnumerable<Rule<T>> LoadStream(Stream Stream);

	}
}
