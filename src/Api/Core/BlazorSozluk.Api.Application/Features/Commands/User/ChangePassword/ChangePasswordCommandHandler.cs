using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infastructure;
using BlazorSozluk.Common.Infastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ChangePasswordCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserId.HasValue)
            throw new ArgumentNullException(nameof(request.UserId));
      

        var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);
        if (dbUser == null)
            throw new DatabaseValidationException("User not found!");
               

        var encPass = PasswordEncryptor.Encrypt(request.OldPassword);
        if (dbUser.Password != encPass)
            throw new DatabaseValidationException("Incorret old password!");

        dbUser.Password = PasswordEncryptor.Encrypt(request.NewPassword);

        await _userRepository.UpdateAsync(dbUser);
        return true;
    }
}
