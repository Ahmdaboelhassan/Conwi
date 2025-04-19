using AutoMapper;
using Domain.IRepository;
using MediatR;

namespace Application.Messages.Command;

public abstract class MessageCommandBaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    protected readonly IUnitOfWork _unitOfWork;

    public MessageCommandBaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
