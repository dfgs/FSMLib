using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SyntaxisAnalysis
{
    public struct Token
    {
		public string Class
		{
			get;
		}
		public string Value
		{
			get;
		}

		public Token(string Class,string Value)
		{
			this.Class = Class;this.Value = Value;
		}

    }
}
