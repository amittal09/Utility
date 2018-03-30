namespace Vertica.Utilities_v4.Extensions.Infrastructure
{
	public interface IExtensionPoint<out T> : IExtensionPoint
	{
		T ExtendedValue
		{
			get;
		}
	}
}