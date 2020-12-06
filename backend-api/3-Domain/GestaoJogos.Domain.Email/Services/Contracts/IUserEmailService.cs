using GestaoJogos.Domain.Email.Dtos;
using System.Threading.Tasks;

namespace GestaoJogos.Domain.Email.Services.Contracts
{
    public interface IUserEmailService
    {
        Task SendEmailCreatedUser(EmailUserDto emailUserDto);
        Task SendEmailUpdatedUser(EmailUserDto emailUserDto);
    }
}
