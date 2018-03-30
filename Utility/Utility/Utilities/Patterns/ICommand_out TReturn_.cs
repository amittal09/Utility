namespace Vertica.Utilities_v4.Patterns
{
	public interface ICommand<out TReturn>
	{
		TReturn Execute();
	}
}