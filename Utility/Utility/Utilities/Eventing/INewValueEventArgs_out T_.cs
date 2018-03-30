namespace Vertica.Utilities_v4.Eventing
{
	public interface INewValueEventArgs<out T>
	{
		T NewValue
		{
			get;
		}
	}
}