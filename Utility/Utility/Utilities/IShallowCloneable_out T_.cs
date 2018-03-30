namespace Vertica.Utilities_v4
{
	public interface IShallowCloneable<out T>
	{
		T ShallowClone();
	}
}