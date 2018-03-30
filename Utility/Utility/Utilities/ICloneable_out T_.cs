namespace Vertica.Utilities_v4
{
	public interface ICloneable<out T> : IShallowCloneable<T>, IDeepCloneable<T>
	{

	}
}