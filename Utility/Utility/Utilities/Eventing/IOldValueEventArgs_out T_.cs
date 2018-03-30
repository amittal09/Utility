namespace Vertica.Utilities_v4.Eventing
{
	public interface IOldValueEventArgs<out T>
	{
		T OldValue
		{
			get;
		}
	}
}