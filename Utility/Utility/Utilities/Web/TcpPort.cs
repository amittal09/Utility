using System;

namespace Vertica.Utilities_v4.Web
{
	public abstract class TcpPort
	{
		public const char PortDelimiter = ':';

		private readonly ushort _portNumber;

		public abstract ushort DefaultPort
		{
			get;
		}

		public virtual bool IsDefaultPort
		{
			get
			{
				return this._portNumber == this.DefaultPort;
			}
		}

		public virtual bool IsRegistered
		{
			get
			{
				if (this._portNumber < 1024)
				{
					return false;
				}
				return this._portNumber <= 49151;
			}
		}

		public virtual bool IsWellKnown
		{
			get
			{
				return this._portNumber <= 1023;
			}
		}

		public int PortNumber
		{
			get
			{
				return this._portNumber;
			}
		}

		protected TcpPort()
		{
			this._portNumber = this.DefaultPort;
		}

		protected TcpPort(ushort portNumber)
		{
			this._portNumber = portNumber;
		}
	}
}