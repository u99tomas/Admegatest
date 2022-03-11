using Application.Interfaces.Repositories;
using Application.Mappings;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Companies.Commands
{
    public class AddEditCompanyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Denomination { get; set; }
    }

    internal class AddEditCompanyCommandHandler : IRequestHandler<AddEditCompanyCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditCompanyCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(AddEditCompanyCommand command, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Repository<Company>().Entities.AnyAsync(c => c.CompanyName == command.CompanyName && c.Id != command.Id);

            if (exist)
            {
                return Result<int>.Failure("La empresa ya existe");
            }

            if (command.Id == 0)
            {
                var company = command.ToCompany();

                await _unitOfWork.Repository<Company>().AddAsync(company);
                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success($"Se creo la empresa {company.CompanyName}", company.Id);
            }
            else
            {
                var company = await _unitOfWork.Repository<Company>().GetByIdAsync(command.Id);

                if (company == null)
                {
                    return Result<int>.Failure($"Error: No se ha encontrado la Empresa con Id {command.Id}");
                }

                company.CompanyName = command.CompanyName;
                company.Denomination = command.Denomination;

                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success($"Se actualizo la empresa {company.CompanyName}", company.Id);
            }
        }
    }
}
