using Application.DTO.Request;
using Application.IRepository;
using Application.Messages.Command.SendMessage;
using AutoMapper;
using Domain.Entity;
using FakeItEasy;
    
namespace Application.Tests.Messages.Command;

public class SendMessageCommandHandlerTest
{
    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly IMapper _fakeMapper;
    private readonly SendMessageCommandHandler _handler;

    public SendMessageCommandHandlerTest()
    {
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _fakeMapper = A.Fake<IMapper>();
        _handler = new SendMessageCommandHandler(_fakeUnitOfWork, _fakeMapper);
    }

}



