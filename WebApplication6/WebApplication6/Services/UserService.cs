namespace WebApplication6.Services;

public interface IUserReader
{
    string GetUserName(int id);
}

public interface IUserWriter
{
    void UpdateUserName(int id, string newName);
}

public interface IUserNotifier
{
    void Notify(int id, string message);
}

public class UserService : IUserReader, IUserWriter, IUserNotifier
{
    private readonly Dictionary<int, string> _users = new()
    {
        { 1, "Админ" },
        { 2, "Пользователь1" }
    };

    public string GetUserName(int id)
    {
        if (_users.TryGetValue(id, out var name))
        {
            return name;
        }
        return "Пользователь не найден";
    }

    public void UpdateUserName(int id, string newName)
    {
        _users[id] = newName;
    }

    public void Notify(int id, string message)
    {
        Console.WriteLine($"[Уведомление пользователю {id}]: {message}");
    }
}