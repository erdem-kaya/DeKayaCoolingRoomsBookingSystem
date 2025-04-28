using Domain.Models;

namespace Business.Model;

public class UserResult : ServiceResult
{
    public IEnumerable<UserProfile>? Users { get; set; } = [];
}

public class UserResult<T> : ServiceResult
{
    public T? Result { get; set; }
}
