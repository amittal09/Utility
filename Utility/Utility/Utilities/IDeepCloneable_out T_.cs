namespace Vertica.Utilities_v4
{
	public interface IDeepCloneable<out T>
	{
		T DeepClone();
	}
}