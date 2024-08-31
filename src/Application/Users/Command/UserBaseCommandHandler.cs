using Application.Helper;
using Application.IRepository;
using Application.IServices;
using AutoMapper;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.Users.Command;

public abstract class UserBaseCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly UserManager<AppUser> _userManager;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;
    protected readonly IPhotoService _photoService;
    protected readonly ITokenService _tokenService;
    protected readonly JWT _Jwt;


    public UserBaseCommandHandler(UserManager<AppUser> userManager, IOptions<JWT> jwt
        , IMapper mapper, IUnitOfWork unitOfWork, IPhotoService photoService , ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
        _Jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _photoService = photoService;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

}
