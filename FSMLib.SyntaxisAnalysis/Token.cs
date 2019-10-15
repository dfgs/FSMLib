using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SyntaxicAnalysis
{
    public struct Token:IEquatable<Token>,IComparable<Token>
    {
		public static int MaxValueLength = 255;
		public static string MaxStringValue = new string(char.MaxValue, MaxValueLength);
		public static string MinStringValue = string.Empty;
		public static Token MinValue = new Token(MinStringValue, MinStringValue);
		public static Token MaxValue = new Token(MaxStringValue, MaxStringValue);

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
			if (Class == null) throw new ArgumentNullException("Class");
			if (Value == null) throw new ArgumentNullException("Value");
			this.Class = Class;this.Value = Value;
		}

		public static bool operator ==(Token obj1, Token obj2)
		{
			return (obj1.Class == obj2.Class && obj1.Value == obj2.Value);
		}
		public static bool operator !=(Token obj1, Token obj2)
		{
			return (obj1.Class != obj2.Class || obj1.Value != obj2.Value);
		}

		public bool Equals(Token other)
		{
			return (Class == other.Class && Value == other.Value);
		}
		public override bool Equals(object obj)
		{
			if (obj is Token other) return Equals(other);
			return false;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 31 + Class.GetHashCode();
				hash = hash * 31 + Class.GetHashCode();
				return hash;
			}
		}

		public override string ToString()
		{
			return $"<{Class},{Value}>";
		}

		private int CompareString(string A,string B)
		{
			if (A.Length==B.Length)
			{
				for(int t=0;t<A.Length;t++)
				{
					if (A[t] == B[t]) continue;
					if (A[t] > B[t]) return 1;
					else return -1;
				}
				return 0;
			}
			if (A.Length > B.Length) return 1;
			else return -1;
		}

		public int CompareTo(Token other)
		{
			if (other.Class==this.Class)
			{
				return CompareString(this.Value, other.Value);
			}
			return CompareString(this.Class, other.Class);
		}



	}
}
