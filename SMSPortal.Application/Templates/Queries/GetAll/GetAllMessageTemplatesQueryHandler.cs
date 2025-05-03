using MediatR;
using SMSPortal.Application.Templates.Dtos;
    using global::SMSPortal.Application.Interfaces.Repositories.MessageTemplates;
    using MediatR;
    using SMSPortal.Application.Interfaces.Repositories.MessageTemplates;
    using SMSPortal.Application.Templates.Queries.GetAll;
    using SMSPortal.Domain.Enitites.MessageTemplates;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    namespace SMSPortal.Application.Templates.Queries.GetAll
    {
        internal class GetAllMessageTemplatesQueryHandler : IRequestHandler<GetAllMessageTemplatesQuery, List<GetAllMessageTemplatesDto>>
        {
            private readonly IMessageTemplateRepo _messageTemplateRepo;

            public GetAllMessageTemplatesQueryHandler(IMessageTemplateRepo messageTemplateRepo)
            {
                _messageTemplateRepo = messageTemplateRepo;
            }

            public async Task<List<GetAllMessageTemplatesDto>> Handle(GetAllMessageTemplatesQuery request, CancellationToken cancellationToken)
            {
                // Retrieve all message templates from the repository
                var templates = await _messageTemplateRepo.GetAllAsync();

                // Map the domain entities to DTOs
                var templateDtos = templates.Select(template => new GetAllMessageTemplatesDto
                {
                    Id = template.Id,
                    Title = template.Title,
                    Content = template.Content
                }).ToList();

                return templateDtos; // Return the list of DTOs
            }
        }
    }


