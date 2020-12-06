using AutoMapper;
using GestaoJogos.CrossCutting.Notification.Services.Contracts;
using GestaoJogos.Domain.Email.Bases;
using GestaoJogos.Domain.Email.Dtos;
using GestaoJogos.Domain.Email.Entities;
using GestaoJogos.Domain.Email.Enums;
using GestaoJogos.Domain.Email.Repositories.Contracts;
using GestaoJogos.Domain.Email.Services.Contracts;
using GestaoJogos.Domain.Email.Validators;
using GestaoJogos.SharedKernel.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GestaoJogos.SharedKernel.Infrastructure.Validators.DoValidate;

namespace GestaoJogos.Domain.Email.Services
{
    public class UserEmailService : EmailBaseService, IUserEmailService
    {
        private readonly INotificationService _notificationService;

        public UserEmailService(
            IUnitOfWork unitOfWork,
            IEmailTemplateRepository repository,
            INotificationService notificationService,
            IMapper mapper
        ) : base(unitOfWork, repository, mapper)
        {
            _notificationService = notificationService;
        }

        public Task SendEmailCreatedUser(EmailUserDto emailUserDto)
        {
            Valid<EnviarEmailUserDtoValidator, EmailUserDto>.Dispatch(emailUserDto);

            var template = TryGetTemplate(EmailTypesEnum.UsuarioCriado, emailUserDto);

            var replacesKeys = new Dictionary<string, string> {
                {"nome", emailUserDto.Nome},
                {"login", emailUserDto.Login},
                {"senha", emailUserDto.Senha ?? ""},
            };

            _notificationService.EnviarEmail(
                emailUserDto.Email,
                template.Assunto,
                template.Texto,
                replacesKeys
            );

            return null;
        }

        public Task SendEmailUpdatedUser(EmailUserDto emailUserDto)
        {
            Valid<EnviarEmailUserDtoValidator, EmailUserDto>.Dispatch(emailUserDto);

            var template = TryGetTemplate(EmailTypesEnum.UsuarioAtualizado, emailUserDto);

            var replacesKeys = new Dictionary<string, string> {
                {"nome", emailUserDto.Nome},
                {"login", emailUserDto.Login},
                {"senha", emailUserDto.Senha ?? ""},
            };

            _notificationService.EnviarEmail(
                emailUserDto.Email,
                template.Assunto,
                template.Texto,
                replacesKeys
            );

            return null;
        }

        private EmailTemplate TryGetTemplate(string identificador, EmailUserDto emailUserDto)
        {
            if (emailUserDto == null)
            {
                throw new Exception("Não foi possível encontrar a configuração de envio de e-mail para esta empresa.");
            }

            var template = GetEmailTemplate(emailUserDto.EmpresaId, identificador);

            if (template == null)
            {
                throw new Exception("Não foi possível encontrar o modelo de e-mail configurado para esta empresa.");
            }

            return template;
        }
    }
}
