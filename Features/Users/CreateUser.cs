using AutoMapper;
using BlazorWebApp.Data;
using BlazorWebApp.Entities;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BlazorWebApp.Features.Users;

public static class CreateUser
{
    public sealed class Command : IRequest<int>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
    
    public sealed class Validator : AbstractValidator<Command>
    {
        private readonly BlazorBlogXDbContext _dbContext;
        public Validator(IStringLocalizer<SharedResource> localizer, BlazorBlogXDbContext dbContext)
        {
            _dbContext = dbContext;
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(256)
                .EmailAddress()
                .Must(BeUniqueEmail)
                .WithMessage(localizer[SharedResource.FieldIsExisted, nameof(Command.Email), nameof(Command.Email)]);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);
        }
        
        private bool BeUniqueEmail(string email)
        {
            bool isEmailExisted = _dbContext
                .Users
                .AsNoTracking()
                .Any(x => x.Email == email);
            
            return !isEmailExisted;
        }
    }

    internal sealed class Handler : IRequestHandler<Command, int>
    {
        private readonly BlazorBlogXDbContext _dbContext;
        private readonly IMapper _mapper;
        public Handler(BlazorBlogXDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }

    internal sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Command, User>();
        }
    }
}

public sealed class MapEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users", async (IMediator mediator, CreateUser.Command command) =>
        {
            var id = await mediator.Send(command);
            return Results.Ok(id);
        });
    }
}