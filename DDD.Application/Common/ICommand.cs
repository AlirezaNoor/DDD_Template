namespace DDD.Application.Common;
public interface ICommand { }
public interface ICommand<out TResponse> { }