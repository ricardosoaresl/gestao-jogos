using AutoMapper;
using GestaoJogos.Domain.Email.Bases.Contracts;
using GestaoJogos.Domain.Email.Entities;
using GestaoJogos.Domain.Email.Repositories.Contracts;
using GestaoJogos.SharedKernel.Application.Services;
using GestaoJogos.SharedKernel.Infrastructure.UnitOfWork;
using System;
using System.Linq;

namespace GestaoJogos.Domain.Email.Bases
{
    public abstract class EmailBaseService :
        Service<EmailTemplate, IEmailTemplateRepository>,
        IEmailBaseService
    {
        public EmailBaseService(
            IUnitOfWork unitOfWork,
            IEmailTemplateRepository repository,
            IMapper mapper
        ) : base(unitOfWork, repository, mapper)
        { }

        public EmailTemplate GetEmailTemplate(Guid empresaId, string identificador)
        {
            return _repository.FindBy(t => t.EmpresaId == empresaId && t.Identificador == identificador)
                .FirstOrDefault();
        }
    }
}
