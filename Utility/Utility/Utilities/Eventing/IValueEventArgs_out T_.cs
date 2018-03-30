namespace Vertica.Utilities_v4.Eventing
{
	public interface IValueEventArgs<out T>
	{
		T Value
		{
			get;
		}
	}
}