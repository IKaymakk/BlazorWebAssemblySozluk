using MediatR;

namespace BlazorSozluk.Common.Events.User;

public class ChangePasswordCommand : IRequest<bool>
{
    public ChangePasswordCommand(Guid? userId, string oldPassword, string newPassword)
    {
        UserId = userId;
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }

    public Guid? UserId { get; set; }
    public string OldPassword{ get; set; }
    public string NewPassword{ get; set; }
}
