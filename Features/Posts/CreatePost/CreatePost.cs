using AutoMapper;
using BlazorWebApp.Data;
using BlazorWebApp.Entities;
using Carter;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BlazorWebApp.Features.Posts.CreatePost;

public static class CreatePost
{
    public const string Endpoint = "/api/posts";
    public sealed class Command : IRequest<ErrorOr<int>>
    {
        public string Title { get; set; } = default!;
        
        public string Content { get; set; } = default!;
        
        public int AuthorID { get; set; }
        
        public int CategoryID { get; set; }

        public List<Detail> Details { get; set; } = new();

        public sealed class Detail
        {
            public string Key { get; set; } = default!;
            public string Value { get; set; } = default!;
            public string Type { get; set; } = default!;
        }
    }
    
    public class Validator : AbstractValidator<Command>
    {
        private readonly BlazorBlogXDbContext _dbContext;
        public Validator(IStringLocalizer<SharedResource> localizer, BlazorBlogXDbContext dbContext)
        {
            _dbContext = dbContext;
            
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.Content)
                .NotEmpty();
            
            RuleFor(x => x.AuthorID)
                .NotEmpty()
                .GreaterThan(0)
                .Must(BeAuthorIdExisted)
                .WithMessage(localizer[SharedResource.FieldIsNotExistedInDatabase, nameof(Command.AuthorID), nameof(Command.AuthorID)]);

            RuleFor(x => x.CategoryID)
                .NotEmpty()
                .GreaterThan(0)
                .Must(BeCategoryIdExisted)
                .WithMessage(localizer[SharedResource.FieldIsNotExistedInDatabase, nameof(Command.CategoryID), nameof(Command.CategoryID)]);
            
            RuleForEach(x => x.Details)
                .ChildRules(detail =>
                {
                    detail.RuleFor(x => x.Key)
                        .NotEmpty();

                    detail.RuleFor(x => x.Value)
                        .NotEmpty();

                    detail.RuleFor(x => x.Type)
                        .NotEmpty();
                });
        }
        
        private bool BeAuthorIdExisted(int authorId)
        {
            bool isAuthorIdExisted = _dbContext
                .Users
                .AsNoTracking()
                .Any(x => x.Id == authorId);
            
            return isAuthorIdExisted;
        }
        
        private bool BeCategoryIdExisted(int categoryId)
        {
            bool isCategoryIdExisted = _dbContext
                .Categories
                .AsNoTracking()
                .Any(x => x.Id == categoryId);
            
            return isCategoryIdExisted;
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
            var post = _mapper.Map<Post>(request);
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return post.Id;
        }
    }

    internal sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Command, Post>();
        }
    }
}

public sealed class MapEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(CreatePost.Endpoint, async (IMediator mediator, CreatePost.Command command) =>
        {
            var result = await mediator.Send(command);
            return result.IsError ? Results.BadRequest((object?)result.Errors) : Results.Ok((object?)result.Value);
        });
    }
}