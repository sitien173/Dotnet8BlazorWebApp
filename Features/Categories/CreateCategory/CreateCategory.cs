using AutoMapper;
using BlazorWebApp.Data;
using BlazorWebApp.Entities;
using Carter;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BlazorWebApp.Features.Categories.CreateCategory;

public static class CreateCategory
{
    public sealed class Command : IRequest<ErrorOr<int>>
    {
        public string Name { get; set; }
    }
    
    public sealed class Validator : AbstractValidator<Command>
    {
        private readonly BlazorBlogXDbContext _dbContext;
        public Validator(IStringLocalizer<SharedResource> localizer, BlazorBlogXDbContext dbContext)
        {
            _dbContext = dbContext;
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .Must(BeUniqueName)
                .WithMessage(localizer[SharedResource.FieldIsExisted, nameof(Command.Name), nameof(Command.Name)]);
        }

        private bool BeUniqueName(string name)
        {
            bool isNameExisted = _dbContext
                .Categories
                .AsNoTracking()
                .Any(x => x.Name == name);
            
            return !isNameExisted;
        }
    }

    internal sealed class Handler : IRequestHandler<Command, ErrorOr<int>>
    {
        private readonly BlazorBlogXDbContext _dbContext;
        private readonly IMapper _mapper;
        public Handler(BlazorBlogXDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ErrorOr<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }

    internal sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Command, Category>();
        }
    }
}
public sealed class MapEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/categories", async (IMediator mediator, CreateCategory.Command command) =>
        {
            var result = await mediator.Send(command);
            return result.IsError ? Results.BadRequest((object?)result.Errors) : Results.Ok((object?)result.Value);
        });
    }
}