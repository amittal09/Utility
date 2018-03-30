namespace Vertica.Utilities_v4.Patterns
{
	public interface ICommand<in TInput, out TReturn>
	{
		TReturn Execute(TInput input);
	}
}