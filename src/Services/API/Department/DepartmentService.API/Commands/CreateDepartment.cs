﻿using DepartmentService.Contract.DataTransfer;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities;
using Mapster;
using MediatR;
using OneOf;

namespace DepartmentService.API.Commands;

public class CreateDepartment : IRequest<OneOf<DepartmentDto>>
{
    public CreateDepartment(DepartmentCreateDto model)
    {
        Model = model;
    }

    public DepartmentCreateDto Model { get; }
}

public class CreateDepartmentHandler : IRequestHandler<CreateDepartment, OneOf<DepartmentDto>>
{
    private readonly EnterpriseAssistantDbContext _db;

    public CreateDepartmentHandler(EnterpriseAssistantDbContext db)
    {
        _db = db;
    }

    public async Task<OneOf<DepartmentDto>> Handle(CreateDepartment request, CancellationToken cancellationToken)
    {
        var department = _db.Departments.Add(request.Model.Adapt<Department>()).Entity;
        await _db.SaveChangesAsync(cancellationToken);
        return department.Adapt<DepartmentDto>();
    }
}